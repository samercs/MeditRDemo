using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MeditRTest.Web.Data;

namespace MeditRTest.Web.Controllers
{
    public class ApplicationBaseController : Controller
    {
        protected IMediator Mediator;
        protected ApplicationDbContext Context;


        public ApplicationBaseController(IMediator mediator, ApplicationDbContext context)
        {
            Mediator = mediator;
            Context = context;
        }
        
    }
}
