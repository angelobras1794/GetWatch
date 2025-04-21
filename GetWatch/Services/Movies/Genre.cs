using System;
using System.Collections.Generic;
using System.Linq;

using System.Security.Claims;
using Newtonsoft.Json;

namespace GetWatch.Services.Movies
{
    public class Genre
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        
    }
}