using Newtonsoft.Json;

namespace Azure_Functions
{
    public class TemperatureInfo
    {
        [JsonProperty(PropertyName = "temp")]
        public double CurrentTemperature { get; set; }
        [JsonProperty(PropertyName = "pressure")]
        public double Pressure { get; set; }
        [JsonProperty(PropertyName = "humidity")]
        public double Humidity { get; set; }
        [JsonProperty(PropertyName = "temp_min")]
        public double MinTemperature { get; set; }
        [JsonProperty(PropertyName = "temp_max")]
        public double MaxTemperature { get; set; }
    }
}