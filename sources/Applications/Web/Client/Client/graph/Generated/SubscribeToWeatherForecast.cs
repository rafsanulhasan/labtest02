using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace LabTest2.Apps.Web.Client.GraphClient
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SubscribeToWeatherForecast
        : ISubscribeToWeatherForecast
    {
        public SubscribeToWeatherForecast(
            global::LabTest2.Apps.Web.Client.GraphClient.IWeatherForecast onGetForecasts)
        {
            OnGetForecasts = onGetForecasts;
        }

        public global::LabTest2.Apps.Web.Client.GraphClient.IWeatherForecast OnGetForecasts { get; }
    }
}
