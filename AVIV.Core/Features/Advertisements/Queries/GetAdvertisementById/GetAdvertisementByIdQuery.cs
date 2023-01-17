using Ardalis.GuardClauses;
using AutoMapper;
using AVIV.Core.Common.Interfaces;
using AVIV.Core.Common.Mappings;
using AVIV.Core.Features.Advertisements.Specifications;
using AVIV.Core.Features.Advertisements.ViewModels;
using AVIV.Domain.Entities.Advertisement;
using AVIV.SharedKernel.Interface;
using MediatR;

namespace AVIV.Core.Features.Advertisements.Queries.GetAdvertisementById
{
    public class GetAdvertisementByIdQuery : ILoggedRequest<AdvertisementDto>
    {
        public string Id { get; set; }
    }

    public class GetAdvertisementByIdQueryHandler : IRequestHandler<GetAdvertisementByIdQuery, AdvertisementDto>
    {
        private readonly IRepository<Advertisement> _repository;
        private readonly IMapper _mapper;
        private readonly IMeteoService _meteoService;

        public GetAdvertisementByIdQueryHandler(
            IRepository<Advertisement> repository,
            IMapper mapper,
            IMeteoService meteoService
        )
        {
            _repository = repository;
            _mapper = mapper;
            _meteoService = meteoService;
        }

        public async Task<AdvertisementDto> Handle(GetAdvertisementByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new AdvertisementPublishedByIdSpecification(request.Id);
            var advertisement = await _repository
                .AsQueryable(spec)
                .ProjectToFirstOrDefaultAsync<AdvertisementDto>(_mapper.ConfigurationProvider);

            if (advertisement == null)
                throw new Common.Exceptions.NotFoundException(request.Id);

            var meteo = await _meteoService.GetWeatherForCoords("48.85", "2.35");
            advertisement.Meteo = _mapper.Map<MeteoDto>(meteo.CurrentWeather);

            return advertisement;
        }
    }
}
