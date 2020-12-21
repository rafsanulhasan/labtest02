using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StrawberryShake;

namespace LabTest2.Apps.Web.Client.GraphClient
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class LabTest2Client
        : ILabTest2Client
    {
        private const string _clientName = "LabTest2Client";

        private readonly global::StrawberryShake.IOperationExecutor _executor;
        private readonly global::StrawberryShake.IOperationStreamExecutor _streamExecutor;

        public LabTest2Client(global::StrawberryShake.IOperationExecutorPool executorPool)
        {
            _executor = executorPool.CreateExecutor(_clientName);
            _streamExecutor = executorPool.CreateStreamExecutor(_clientName);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::LabTest2.Apps.Web.Client.GraphClient.IGetWeatherForecasts>> GetWeatherForecastsAsync(
            global::System.Threading.CancellationToken cancellationToken = default)
        {

            return _executor.ExecuteAsync(
                new GetWeatherForecastsOperation(),
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::LabTest2.Apps.Web.Client.GraphClient.IGetWeatherForecasts>> GetWeatherForecastsAsync(
            GetWeatherForecastsOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::LabTest2.Apps.Web.Client.GraphClient.IAddWeatherForecast>> AddWeatherForecastAsync(
            global::StrawberryShake.Optional<global::LabTest2.Apps.Web.Client.GraphClient.WeatherForecastDTOInput> forecast = default,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (forecast.HasValue && forecast.Value is null)
            {
                throw new ArgumentNullException(nameof(forecast));
            }

            return _executor.ExecuteAsync(
                new AddWeatherForecastOperation { Forecast = forecast },
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<global::LabTest2.Apps.Web.Client.GraphClient.IAddWeatherForecast>> AddWeatherForecastAsync(
            AddWeatherForecastOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _executor.ExecuteAsync(operation, cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::LabTest2.Apps.Web.Client.GraphClient.ISubscribeToWeatherForecast>> SubscribeToWeatherForecastAsync(
            global::System.Threading.CancellationToken cancellationToken = default)
        {

            return _streamExecutor.ExecuteAsync(
                new SubscribeToWeatherForecastOperation(),
                cancellationToken);
        }

        public global::System.Threading.Tasks.Task<global::StrawberryShake.IResponseStream<global::LabTest2.Apps.Web.Client.GraphClient.ISubscribeToWeatherForecast>> SubscribeToWeatherForecastAsync(
            SubscribeToWeatherForecastOperation operation,
            global::System.Threading.CancellationToken cancellationToken = default)
        {
            if (operation is null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            return _streamExecutor.ExecuteAsync(operation, cancellationToken);
        }
    }
}
