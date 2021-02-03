using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MeditRTest.Web.Core.Employee.CreateNotification;
using MeditRTest.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace MeditRTest.Web.Core.Employee.EditCommand
{
    public class EditCommand: IRequest<Data.Employee>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }

    public class EditCommandHandler: IRequestHandler<EditCommand, Data.Employee>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        public EditCommandHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext;
            _mediator = mediator;

        }
        public async Task<Data.Employee> Handle(EditCommand request, CancellationToken cancellationToken)
        {
            var emp = await _applicationDbContext.Employees.FirstOrDefaultAsync(i => i.Id == request.Id,
                cancellationToken);
            if (emp != null)
            {
                emp.Name = request.Name;
                emp.Salary = request.Salary;
                _applicationDbContext.Entry(emp).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
            }

            await _mediator.Publish(new EmployeeNotification()
            {
                Employee = emp,
                ActionType = ActionType.Edit
            }, cancellationToken);

            return emp;
        }
    }
}