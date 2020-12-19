
using Fluxor;

using LabTest2.Apps.Web.Shared.DTOs;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

using ReactiveUI;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace LabTest2.Apps.Web.Shared.Store.FetchData
{
	public class FetchDataEffect
		: Effect<FetchDataAction>, IDisposable
	{
		private readonly HttpClient _http;
		private readonly CompositeDisposable _disposables;
		private readonly FetchDataState _initialState;
		private bool _disposedValue;

		public FetchDataEffect(
			IHttpClientFactory factory,
			IState<FetchDataState> state
		)
		{
			_disposables = new CompositeDisposable();
			_http = factory.CreateClient("LabTest2.Apps.Web.ServerAPI-Unrestricted");
			_http.DisposeWith(_disposables);
			_initialState = state.Value;
		}

		protected override async Task HandleAsync(
			FetchDataAction action,
			IDispatcher dispatcher
		)
		{
			var resultAction = new FetchDataResultAction(_initialState.WeatherForecasts, _initialState.IsLoading);
			_ = Observable
				.Create<List<WeatherForecastDTO>>(async observer =>
				{
					try
					{
						var forecasts = await _http
							.GetFromJsonAsync<List<WeatherForecastDTO>>(
								"WeatherForecast"
							)
							.ConfigureAwait(true);
						observer.OnNext(forecasts!);
						observer.OnCompleted();
					}
					catch (AccessTokenNotAvailableException ex)
					{
						observer.OnError(ex);
					}
					catch (Exception ex)
					{
						observer.OnError(ex);
					}
				})
				.Where(forecasts => forecasts is not null && forecasts.Count > 0)
				.Select(forecasts => forecasts.ToImmutableList())
				.Retry(5)
				.Timeout(TimeSpan.FromSeconds(10))
				.Catch<ImmutableList<WeatherForecastDTO>, AccessTokenNotAvailableException>(ex =>
				{
					ex.Redirect();
					return Observable.Return(resultAction.WeatherForecasts.ToImmutableList());
				})
				.Catch<ImmutableList<WeatherForecastDTO>, Exception>(_
					=> Observable.Return(resultAction.WeatherForecasts.ToImmutableList())
				)
				//.SubscribeOn(RxApp.TaskpoolScheduler)
				.ObserveOn(RxApp.MainThreadScheduler)
				.Subscribe(f =>
				{
					resultAction = resultAction with
					{
						WeatherForecasts = f
					};
					dispatcher.Dispatch(
						resultAction
					);
				})
				.DisposeWith(_disposables);

			await Task.CompletedTask.ConfigureAwait(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					_disposables?.Dispose();
				}
				_disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}