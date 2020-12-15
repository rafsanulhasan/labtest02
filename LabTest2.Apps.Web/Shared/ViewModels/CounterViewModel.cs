using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

using ReactiveUI;

namespace LabTest2.Apps.Web.Shared.ViewModels
{
	public class CounterViewModel
		: ReactiveObject, IDisposable
	{
		private readonly IObservable<bool> _canNotIncrease;
		private readonly CompositeDisposable _disposables;
		private bool _disposedValue;
		private int _count;
		private bool _isIncreaseButtonDisabled;

		public ReactiveCommand<Unit, Unit> IncreaseCommand { get; init; }

		public int Count
		{
			get => _count;
			private set => this.RaiseAndSetIfChanged(ref _count, value);
		}
		public bool IsIncreaseButtonDisabled
		{
			get => _isIncreaseButtonDisabled;
			set => this.RaiseAndSetIfChanged(ref _isIncreaseButtonDisabled, value);
		}

		public CounterViewModel()
		{
			_disposables = new CompositeDisposable();

			_canNotIncrease = this
							.WhenAnyValue(vm => vm.Count)
							.Select(c => c > 5);

			_canNotIncrease
				.Subscribe(v => IsIncreaseButtonDisabled = v)
				.DisposeWith(_disposables);

			IncreaseCommand = ReactiveCommand.Create(
				() => Increment(),
				_canNotIncrease
			)
			.DisposeWith(_disposables)
			;

		}

		public void Decrement(int number = 1)
			=> Count -= number;

		public void Increment(int number = 1)
			 => Count += number;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					_disposables?.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null

				_disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~CounterViewModel()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
