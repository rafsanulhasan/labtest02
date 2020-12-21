using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace LabTest2.Apps.Web.Client.GraphClient
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetWeatherForecastsOperation
        : IOperation<IGetWeatherForecasts>
    {
        public string Name => "getWeatherForecasts";

        public IDocument Document => Queries.Default;

        public OperationKind Kind => OperationKind.Query;

        public Type ResultType => typeof(IGetWeatherForecasts);

        public IReadOnlyList<VariableValue> GetVariableValues()
        {
            return Array.Empty<VariableValue>();
        }
    }
}
