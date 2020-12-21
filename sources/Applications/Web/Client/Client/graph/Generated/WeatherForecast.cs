using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace LabTest2.Apps.Web.Client.GraphClient
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class WeatherForecast
        : IWeatherForecast
    {
        public WeatherForecast(
            System.DateTimeOffset date, 
            int temperatureC, 
            int temperatureF, 
            string summary)
        {
            Date = date;
            TemperatureC = temperatureC;
            TemperatureF = temperatureF;
            Summary = summary;
        }

        public System.DateTimeOffset Date { get; }

        public int TemperatureC { get; }

        public int TemperatureF { get; }

        public string Summary { get; }
    }
}
