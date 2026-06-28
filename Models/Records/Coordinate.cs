
using Newtonsoft.Json;

namespace Traverse.Models.Records
{
    public record Coordinate 
    {
        [JsonProperty("latitude")]
        public double Latitude {get; init; }
        [JsonProperty("longitude")]
        public double Longitude {get; init; }
    }

}
