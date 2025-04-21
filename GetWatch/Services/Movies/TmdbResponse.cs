using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GetWatch.Services.Movies
{
    public class TmdbResponse
    {
        [JsonProperty("results")]
        public List<PopularApiMovie> Results { get; set; } = new List<PopularApiMovie>();
    }
}