using AVIV.Core.Common.Models.Meteo;

namespace AVIV.Core.Common.Interfaces
{
    public interface IMeteoService {

        public Task<MeteoResult> GetWeatherForCoords(string latitude, string longitude);
    }
}
