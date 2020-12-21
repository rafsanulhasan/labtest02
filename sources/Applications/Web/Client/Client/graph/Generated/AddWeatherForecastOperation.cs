using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace LabTest2.Apps.Web.Client.GraphClient
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class AddWeatherForecastOperation
        : IOperation<IAddWeatherForecast>
    {
        public string Name => "addWeatherForecast";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Mutation;

        public Type ResultType => typeof(IAddWeatherForecast);

        public Optional<global::LabTest2.Apps.Web.Client.GraphClient.WeatherForecastDTOInput> Forecast { get; set; }

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            var variables = new List<VariableValue>();

            if (Forecast.HasValue)
            {
                variables.Add(new VariableValue("forecast", "WeatherForecastDTOInput", Forecast.Value));
            }

            return variables;
        }
    }
}
