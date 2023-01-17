using MediatR;

namespace AVIV.Core.Common.Interfaces
{
    public interface ISensibleRequest : ILoggedRequest<Unit> { }
    public interface ISensibleRequest<TResponse> : IRequest<TResponse> { }
}
