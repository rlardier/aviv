using AVIV.Core.Common.Interfaces;
using AVIV.Core.Common.Models.Meteo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AVIV.Core.Common.Services
{
    public class MeteoService : IMeteoService
    {
        private const string APIUrlKey = "MeteoAPI:Url";
        private readonly HttpClient _httpClient;
        private readonly ILogger<MeteoService> _logger;

        public MeteoService(
            IConfiguration config,
            ILogger<MeteoService> logger)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(config[APIUrlKey])
            };
            _logger = logger;
        }

        public async Task<MeteoResult> GetWeatherForCoords(string latitude, string longitude)
        {
            MeteoResult result = null;

            try
            {
                using (var response = await _httpClient.GetAsync($"forecast?latitude={latitude}&longitude={longitude}&current_weather=true"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<MeteoResult>(apiResponse);
                }
            }
            catch (Exception ex)
            {
                // TODO : check comportement à avoir.
                _logger.LogError("Error while getting meteo", ex);
            }

            return result;
        }
    }
}
