using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using q5id.platform.email.models;
using q5id.platform.email.dal.Interfaces;
using q5id.platform.email.dal.Entities;
using SerilogTimings;


namespace q5id.platform.email.api.Controllers
{
    
    [Route("/api/v1/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository _emailRepository;
        private readonly ILogger<EmailController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="emailRepository"></param>
        public EmailController(ILogger<EmailController> logger, IEmailRepository emailRepository)
        {
            _logger = logger;
            _emailRepository = emailRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Email email)
        {
            string correlationId = Guid.NewGuid().ToString();
            Response.Headers.Add("X-CorrelationId-X", correlationId);
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid email address format ${email}", new Exception("WARNING"));
                return BadRequest(406);
            }

            // Telemetry Information.
            using (Operation.Time("{Telemetry} {Method} | {CorrelationId} ", "Telemetry", nameof(Create), correlationId))
            {
                try
                {
                    EmailEntity emailEntity = new EmailEntity() {
                          EmailAddress = email.EmailAddress.ToLower()
                        , UpdateDateTime = email.UpdateDateTime
                        , IsConsumer = email.IsConsumer
                        , IsInvestor = email.IsInvestor
                        , IsBusiness = email.IsBusiness
                    };
                        
                    await _emailRepository.Create(emailEntity);

                    _logger.LogDebug("=========> After create Email to Database ${email} | {correlationId}", email.EmailAddress, correlationId);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.ToString().Contains("duplicate key"))
                    {
                        // Dupliacted Email Address | send 200 to Mobile device.
                        _logger.LogWarning("Duplicate email address ${email}", new Exception("WARNING"));
                        return Ok(200);
                    }
                    else
                    {
                        // General Error. Most likely due to Connection to SQL Server
                        _logger.LogError("Error creating email ${email}", ex);
                        return BadRequest(500);
                    }
                }
                // Email Saved Successfully
                return Ok(200);
            }
        }
    }
}
