namespace CodeBase.Infrastructure.GameStateMachine.States
{
	public interface IState : IExitableState
	{
		void Enter();
	}
}