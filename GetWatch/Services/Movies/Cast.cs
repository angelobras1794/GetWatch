using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;

namespace GetWatch.Services.Movies
{
    public class Cast
    {

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("profile_path")]
        public string? ProfilePath { get; set; }

        [JsonProperty("character")]
        public string? Character { get; set; } = string.Empty;

        [JsonProperty("known_for_department")]
        public string? KnownForDepartment { get; set; } = string.Empty;
        
    }
}