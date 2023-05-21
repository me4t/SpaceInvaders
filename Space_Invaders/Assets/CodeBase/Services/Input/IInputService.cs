namespace CodeBase.Services.Input
{
	public interface IInputService
	{
		float Horizontal { get; }
		float Vertical { get; }
		bool IsIceFire { get;}
		bool IsFireMagic { get;}
	}
}