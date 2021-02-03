using System.Threading;
using System.Threading.Tasks;
using MediatR;
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

        public Handler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
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
            return newEmp;
        }
    }
}