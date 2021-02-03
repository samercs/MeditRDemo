using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MeditRTest.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace MeditRTest.Web.Core.Employee.GetById
{
    public class GetById: IRequest<Data.Employee>
    {
        public int Id { get; set; }
    }

    public class GetByIdHandler: IRequestHandler<GetById, Data.Employee>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GetByIdHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Data.Employee> Handle(GetById request, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Employees.FirstOrDefaultAsync(i => i.Id == request.Id,
                cancellationToken);
        }
    }
}