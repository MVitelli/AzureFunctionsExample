using Newtonsoft.Json;

namespace Azure_Functions
{
    public class WeatherDTO
    {
        [JsonProperty( PropertyName = "name")]
        public string CityName { get; set; }
        [JsonProperty(PropertyName = "main")]
        public TemperatureInfo Temperature { get; set; }
    }
}