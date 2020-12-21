using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StrawberryShake;

namespace LabTest2.Apps.Web.Client.GraphClient
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial interface ILabTest2Client
    {
        Task<IOperationResult<global::LabTest2.Apps.Web.Client.GraphClient.IGetWeatherForecasts>> GetWeatherForecastsAsync(
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::LabTest2.Apps.Web.Client.GraphClient.IGetWeatherForecasts>> GetWeatherForecastsAsync(
            GetWeatherForecastsOperation operation,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::LabTest2.Apps.Web.Client.GraphClient.IAddWeatherForecast>> AddWeatherForecastAsync(
            Optional<global::LabTest2.Apps.Web.Client.GraphClient.WeatherForecastDTOInput> forecast = default,
            CancellationToken cancellationToken = default);

        Task<IOperationResult<global::LabTest2.Apps.Web.Client.GraphClient.IAddWeatherForecast>> AddWeatherForecastAsync(
            AddWeatherForecastOperation operation,
            CancellationToken cancellationToken = default);

        global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::LabTest2.Apps.Web.Client.GraphClient.ISubscribeToWeatherForecast>> SubscribeToWeatherForecastAsync(
            CancellationToken cancellationToken = default);

        global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::LabTest2.Apps.Web.Client.GraphClient.ISubscribeToWeatherForecast>> SubscribeToWeatherForecastAsync(
            SubscribeToWeatherForecastOperation operation,
            CancellationToken cancellationToken = default);
    }
}
