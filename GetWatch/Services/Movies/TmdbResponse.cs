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

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
    }

    public class TmdbGenresResponse
    {
        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}