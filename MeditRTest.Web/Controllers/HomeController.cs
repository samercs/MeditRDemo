using MeditRTest.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MeditRTest.Web.Core;
using UpdatePortal.Service;

namespace MeditRTest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly EmailService _emailService;

        public HomeController(ILogger<HomeController> logger, IMediator mediator, EmailService emailService)
        {
            _logger = logger;
            _mediator = mediator;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new Ping());
            ViewBag.Str = response;
            await _emailService.SendEmail("samer_mail_2006@yahoo.com", "samer", "samer");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
