using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MeditRTest.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace MeditRTest.Web.Core.Employee.DeleteCommand
{
    public class DeleteCommand: IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteCommandHandler: AsyncRequestHandler<DeleteCommand>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public DeleteCommandHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        protected override async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var emp = await _applicationDbContext.Employees.FirstOrDefaultAsync(i=>i.Id == request.Id, cancellationToken);
            _applicationDbContext.Employees.Remove(emp);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}