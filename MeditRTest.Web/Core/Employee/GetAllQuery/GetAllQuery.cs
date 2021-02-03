using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MeditRTest.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace MeditRTest.Web.Core.Employee.GetAllQuery
{
    public class GetAllQuery: IRequest<IList<Data.Employee>>
    {
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IList<Data.Employee>>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GetAllQueryHandler(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public async Task<IList<Data.Employee>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var allEmps = await _applicationDbContext.Employees.ToListAsync(cancellationToken);
            return allEmps;
        }
    }
}