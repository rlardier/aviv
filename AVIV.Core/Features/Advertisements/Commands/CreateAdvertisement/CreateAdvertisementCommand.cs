using AVIV.Core.Common.Interfaces;
using AVIV.Domain.Entities.Advertisement;
using AVIV.Domain.Enums;
using AVIV.SharedKernel.Interface;
using MediatR;

namespace AVIV.Core.Features.Advertisements.Commands.CreateAdvertisement
{
    public class CreateAdvertisementCommand : ILoggedRequest<string>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Localisation { get; set; }
        public PropertyType Type { get; set; }
    }

    public class CreateAdvertisementCommandHandler : IRequestHandler<CreateAdvertisementCommand, string>
    {
        private readonly IRepository<Advertisement> _repository;
        
        public CreateAdvertisementCommandHandler(IRepository<Advertisement> repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var newAdvertisement = new Advertisement(
                    request.Title, 
                    request.Description, 
                    request.Localisation, 
                    request.Type);

            await _repository.AddAsync(newAdvertisement, cancellationToken);

            return newAdvertisement.Id;
        }
    }
}
