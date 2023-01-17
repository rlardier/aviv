using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using AVIV.SharedKernel.Interface;
using System.Threading;
using System.Threading.Tasks;
using AVIV.Core.Common.Interfaces;

namespace AVIV.Core.Common.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ILoggedRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        //private readonly ICurrentUserService _currentUserService;
        //private readonly ITenantInfo _tenantInfo;

        /*private readonly IIdentityService _identityService;*/

        public LoggingBehaviour(
            ILogger<LoggingBehaviour<TRequest, TResponse>> logger
            //ICurrentUserService currentUserService,
            //ITenantInfo tenantInfo
             //IIdentityService identityService
            )
        {
            _logger = logger;
            //_currentUserService = currentUserService;
            /*_identityService = identityService;*/
            //_tenantInfo = tenantInfo;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId =  /*_currentUserService.UserId ?? */ string.Empty;
            string userName = string.Empty;


            _logger.LogInformation("Begin AVIV mediator request: {RequestType} ({@Request}) - For : {@UserId} {@UserName} on Tenant : {@Tenant}",
                requestName, request, userId, userName, "_tenantInfo.Name");

            var response = await next();

            var responseName = typeof(TResponse).Name;
            _logger.LogInformation("Success AVIV mediator request. Return type : {ResponseType} - For : {@UserId} {@UserName} on Tenant : {@Tenant}",
                responseName, userId, userName, "_tenantInfo.Name");

            return response;
        }
    }
}
