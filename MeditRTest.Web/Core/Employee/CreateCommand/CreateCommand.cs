using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MeditRTest.Web.Core.Employee.CreateNotification;
using MeditRTest.Web.Data;

namespace MeditRTest.Web.Core.Employee.CreateCommand
{
    public class CreateCommand : IRequest<Data.Employee>
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }

    public class Handler : IRequestHandler<CreateCommand, Data.Employee>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        public Handler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext;
            _mediator = mediator;
        }

        public async Task<Data.Employee> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var newEmp = new Data.Employee()
            {
                Name = request.Name,
                Salary = request.Salary
            };
            await _applicationDbContext.Employees.AddAsync(newEmp, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new EmployeeNotification()
            {
                Employee = newEmp,
                ActionType = ActionType.Create
            }, cancellationToken);
            return newEmp;
        }
    }
}