﻿using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AVIV.Core.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        //private readonly ICurrentUserService _currentUserService;
        //private readonly IIdentityService _identityService;

        public PerformanceBehaviour(
            ILogger<TRequest> logger
            /*ICurrentUserService currentUserService*/)

        {
            _timer = new Stopwatch();

            _logger = logger;
            //_currentUserService = currentUserService;
            //_identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;
                var userId = /*_currentUserService.UserId ?? */string.Empty;
                var userName = string.Empty;

                _logger.LogWarning("AVIV API Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                    requestName, elapsedMilliseconds, userId, userName, request);
            }

            return response;
        }
    }
}
