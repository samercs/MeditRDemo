using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MeditRTest.Web.Core.Employee.CreateCommand;
using MeditRTest.Web.Core.Employee.CreateNotification;
using MeditRTest.Web.Core.Employee.DeleteCommand;
using MeditRTest.Web.Core.Employee.EditCommand;
using MeditRTest.Web.Core.Employee.GetAllQuery;
using MeditRTest.Web.Core.Employee.GetById;
using MeditRTest.Web.Data;

namespace MeditRTest.Web.Controllers
{
    public class EmployeeController : ApplicationBaseController
    {
        public EmployeeController(IMediator mediator, ApplicationDbContext context) : base(mediator, context)
        {
        }

        public async Task<IActionResult> Index()
        {
            var allEmps = await Mediator.Send(new GetAllQuery());
            return View(allEmps);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name, decimal salary)
        {
            var result = await Mediator.Send(new CreateCommand()
            {
                Name = name,
                Salary = salary
            });
            ViewBag.Success = $"Employee {result.Id} has been added successfully.";
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var emp = await Mediator.Send(new GetById()
            {
                Id = id
            });
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, string name, decimal salary)
        {
            var emp = await Mediator.Send(new GetById()
            {
                Id = id
            });
            if (emp == null)
            {
                return NotFound();
            }

            await Mediator.Send(new EditCommand()
            {
                Id = id,
                Name = name,
                Salary = salary
            });
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var emp = await Mediator.Send(new GetById()
            {
                Id = id
            });
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var emp = await Mediator.Send(new GetById()
            {
                Id = id
            });
            if (emp == null)
            {
                return NotFound();
            }

            await Mediator.Send(new DeleteCommand()
            {
                Id = id
            });
            return RedirectToAction(nameof(Index));
        }
    }
}
