using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MeditRTest.Web.Core.Employee.CreateNotification;
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
        private readonly IMediator _mediator;
        public DeleteCommandHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext;
            _mediator = mediator;
        }
        
        protected override async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var emp = await _applicationDbContext.Employees.FirstOrDefaultAsync(i=>i.Id == request.Id, cancellationToken);
            _applicationDbContext.Employees.Remove(emp);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new EmployeeNotification()
            {
                Employee = emp,
                ActionType = ActionType.Delete
            }, cancellationToken);
        }
    }
}