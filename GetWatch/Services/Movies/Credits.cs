using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;

namespace GetWatch.Services.Movies
{
    public class Credits
    {
        [JsonProperty("cast")]
        public List<Cast> Cast { get; set; } = new List<Cast>();

        [JsonProperty("crew")]

        public List<Crew> Crew { get; set; } = new List<Crew>();
    }
}