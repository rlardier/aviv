using AVIV.Core.Common.Exceptions;
using AVIV.Core.Common.Interfaces;
using AVIV.Core.Features.Advertisements.Specifications;
using AVIV.Domain.Entities.Advertisement;
using AVIV.Domain.Enums;
using AVIV.SharedKernel.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace AVIV.Core.Features.Advertisements.Commands.UpdateStatus
{
    public class UpdateAdvertisementStatusCommand : ILoggedRequest<Unit>
    {
        public string Id { get; set; }
        public AdvertisementStatus Status { get; set; }
    }

    public class UpdateAdvertisementStatusCommandHandler : IRequestHandler<UpdateAdvertisementStatusCommand, Unit>
    {
        private readonly IRepository<Advertisement> _repository;

        public UpdateAdvertisementStatusCommandHandler(IRepository<Advertisement> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateAdvertisementStatusCommand request, CancellationToken cancellationToken)
        {
            var spec = new AllAdvertisementByIdSpecification(request.Id);
            var advertisement = await _repository.AsQueryable(spec).FirstOrDefaultAsync();

            if (advertisement == null)
                throw new ForbiddenAccessException();

            advertisement.UpdateStatus(request.Status);

            await _repository.UpdateAsync(advertisement, cancellationToken);

            return Unit.Value;
        }
    }
}
