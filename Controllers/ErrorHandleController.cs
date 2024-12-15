using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TracNghiem.Controllers
{
    [AllowAnonymous]
    public class ErrorHandleController : Controller
    {
        private readonly ILogger<ErrorHandleController> _logger;

        public ErrorHandleController(ILogger<ErrorHandleController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Forbidden()
        {
            return View();
        }

    }
}