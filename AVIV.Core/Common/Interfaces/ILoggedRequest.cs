using MediatR;

namespace AVIV.Core.Common.Interfaces
{
    public interface ILoggedRequest : ILoggedRequest<Unit> { }
    public interface ILoggedRequest<TResponse> : IRequest<TResponse> { }
}
