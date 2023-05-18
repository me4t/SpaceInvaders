namespace CodeBase.Infrastructure
{
	public interface IGameStateMachine 
	{
		void Enter<TState>() where TState : class, IState;
		void Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>;
	}
}