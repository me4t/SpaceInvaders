namespace CodeBase.Infrastructure.GameStateMachine.States
{
	public interface IPaylodedState<TPayload> : IExitableState
	{
		void Enter(TPayload payload);
	}
}