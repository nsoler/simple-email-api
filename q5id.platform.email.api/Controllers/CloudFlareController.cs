using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using q5id.platform.email.api.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace q5id.platform.email.api.Controllers
{
    public class CloudFlareController : Controller
    {

        public CloudFlareController()
        {
        }

        [HttpGet]
        [Route("/.well-known/pki-validation/ca3-c285e16a70ac4b238f983b6e9733b90e.txt")]
        public IActionResult Index()
        {

            return Ok("ca3-973d370c2fb740ed98ada28ce052f048");
        }
    }
}

