using System;
using System.Collections.Generic;
using System.Text;

namespace Azure_Functions
{
    public static class WeatherConverter
    {
        public static WeatherDTO ConvertToCelsius(WeatherDTO weatherDTO)
        {
            WeatherDTO weatherInCelsius = new WeatherDTO()
            {
                CityName = weatherDTO.CityName,
                Temperature = new TemperatureInfo()
                {
                    CurrentTemperature = Convert(weatherDTO.Temperature.CurrentTemperature),
                    MinTemperature = Convert(weatherDTO.Temperature.MinTemperature),
                    MaxTemperature = Convert(weatherDTO.Temperature.MaxTemperature),
                    Pressure = weatherDTO.Temperature.Pressure,
                    Humidity = weatherDTO.Temperature.Humidity
                }
            };
            return weatherInCelsius;
        }

        private static double Convert(double temperature)
        {
            double temperatureConverted = temperature - 273.15;
            return Math.Round(temperatureConverted, 1);
        }
    }
}
