using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MeditRTest.Web.Data;

namespace MeditRTest.Web.Core.Behaviors
{
    public class DatabaseLoginBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public DatabaseLoginBehavior(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var log = new Log
            {
                Message = $"New Request {typeof(TRequest).Name}"
            };
            await _applicationDbContext.Logs.AddAsync(log, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var response = await next();

            log = new Log
            {
                Message = $"New Response {typeof(TResponse).Name}"
            };
            await _applicationDbContext.Logs.AddAsync(log, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return response;
        }
    }
}