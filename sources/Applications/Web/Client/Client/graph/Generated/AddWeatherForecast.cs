using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace LabTest2.Apps.Web.Client.GraphClient
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class AddWeatherForecast
        : IAddWeatherForecast
    {
        public AddWeatherForecast(
            global::LabTest2.Apps.Web.Client.GraphClient.IGetForecastResponse addForecast)
        {
            AddForecast = addForecast;
        }

        public global::LabTest2.Apps.Web.Client.GraphClient.IGetForecastResponse AddForecast { get; }
    }
}
