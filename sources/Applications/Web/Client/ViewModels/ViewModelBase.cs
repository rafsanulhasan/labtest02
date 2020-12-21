using System;
using System.Reactive.Disposables;

using Fluxor;

using LabTest2.Apps.Web.Client.Store;

using ReactiveUI;

namespace LabTest2.Apps.Web.Client.ViewModels
{
	public abstract class ViewModelBase<TState>
		: ReactiveObject, IDisposable
		where TState : StateBase
	{
		private bool _disposedValue;
		protected readonly CompositeDisposable Disposables;

		public IStore Store { get; protected set; }

		public IState<TState> State { get; protected set; }

		public IDispatcher Dispatcher { get; protected set; }

		protected ViewModelBase(
			IStore store,
			IState<TState> state,
			IDispatcher dispatcher
		)
		{
			Store = store;
			State = state;
			Dispatcher = dispatcher;
			Disposables = new CompositeDisposable();
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					Disposables?.Dispose();
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
