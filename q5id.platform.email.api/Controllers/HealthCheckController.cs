using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using q5id.platform.email.api.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace q5id.platform.email.api.Controllers
{
    public class HealthCheckController : Controller
    {

        private readonly IAppVersionService _versionService;

        public HealthCheckController(IAppVersionService versionService)
        {
            _versionService = versionService;
        }

        [HttpGet]
        [Route("/api/v1/healthcheck")]
        public IActionResult Index()
        {
            return Ok($"Healthcheck: OK | Version: {_versionService.Version}");
        }
    }
}

