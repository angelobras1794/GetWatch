using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;

namespace GetWatch.Services.Movies
{
    public class Crew
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("profile_path")]
        public string? ProfilePath { get; set; }
        [JsonProperty("job")]
        public string? Job { get; set; } = string.Empty;
        [JsonProperty("department")]
        public string? Department { get; set; } = string.Empty;
        
    }
}