using AVIV.Core.Common.Mappings;
using AVIV.Core.Common.Models.Meteo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVIV.Core.Features.Advertisements.ViewModels
{
    public class MeteoDto : IMapFrom<CurrentWeather>
    {
        public float Temperature { get; set; }
        public float WindSpeed { get; set; }
    }
}
