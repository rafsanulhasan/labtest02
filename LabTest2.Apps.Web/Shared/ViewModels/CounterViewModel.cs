using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

using Fluxor;

using LabTest2.Apps.Web.Shared.Store.Counter;

using ReactiveUI;

namespace LabTest2.Apps.Web.Shared.ViewModels
{
	public class CounterViewModel
		: ViewModelBase<CounterState>
	{
		private readonly IObservable<bool> _canNotIncrease;
		public ReactiveCommand<Unit, Unit> IncreaseCommand { get; init; }

		private int _count;
		public int Count
		{
			get => _count;
			private set => this.RaiseAndSetIfChanged(ref _count, value);
		}

		private bool _isIncreaseButtonDisabled;
		public bool IsIncreaseButtonDisabled
		{
			get => _isIncreaseButtonDisabled;
			set => this.RaiseAndSetIfChanged(ref _isIncreaseButtonDisabled, value);
		}

		public CounterViewModel(
			IStore store,
			IState<CounterState> state,
			IDispatcher dispatcher
		)
			: base(store, state, dispatcher)
		{
			Count = State.Value.Count;
			_canNotIncrease = this
							.WhenAnyValue(vm => vm.Count)
							.Select(c => c > 5);

			_canNotIncrease
				.Subscribe(
					canNotIncrease => IsIncreaseButtonDisabled = canNotIncrease
				)
				.DisposeWith(Disposables);

			IncreaseCommand = ReactiveCommand.Create(
				() => Increment(),
				_canNotIncrease
			)
			.DisposeWith(Disposables)
			;

			Observable.FromEventPattern<CounterState>(
				eh => State.StateChanged += eh,
				eh => State.StateChanged -= eh
			)
			.Where(handler => handler is not null)
			.Select(handler => handler.EventArgs)
			.Where(args => args is not null)
			.Subscribe(state
				=> Count = state.Count
			)
			.DisposeWith(Disposables);
		}

		public void Increment(int number = 1)
			=> Dispatcher.Dispatch(new CounterIncrementAction(number));
	}
}