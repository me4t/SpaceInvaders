using UnityEngine;

namespace CodeBase.Services.Input
{
	public interface IInputService
	{
		float Horizontal { get; }
		float Vertical { get; }
		Vector2 Axis { get;}
		bool IsIceFire { get;}
		bool IsFireMagic { get;}
	}
}