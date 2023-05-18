namespace CodeBase.Infrastructure
{
	public interface IPaylodedState<TPayload> : IExitableState
	{
		void Enter(TPayload payload);
	}
}