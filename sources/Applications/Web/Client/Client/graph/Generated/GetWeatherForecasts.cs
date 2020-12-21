using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace LabTest2.Apps.Web.Client.GraphClient
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetWeatherForecasts
        : IGetWeatherForecasts
    {
        public GetWeatherForecasts(
            global::LabTest2.Apps.Web.Client.GraphClient.IGetForecastResponse forecasts)
        {
            Forecasts = forecasts;
        }

        public global::LabTest2.Apps.Web.Client.GraphClient.IGetForecastResponse Forecasts { get; }
    }
}
