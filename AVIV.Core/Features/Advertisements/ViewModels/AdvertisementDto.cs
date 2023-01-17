using AVIV.Core.Common.Mappings;
using AVIV.Domain.Entities.Advertisement;

namespace AVIV.Core.Features.Advertisements.ViewModels
{
    public class AdvertisementDto : IMapFrom<Advertisement>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Localisation { get; set; }

        public MeteoDto Meteo { get; set; }
    }
}
