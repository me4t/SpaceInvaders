using UnityEngine;

namespace CodeBase.Services.SceneLoader
{
	public class StandaloneInputService : IInputService
	{
		public float Horizontal => Input.GetAxis("Horizontal");
		public float Vertical => Input.GetAxis("Vertical");
		public Vector2 Axis => new Vector2(Horizontal, Vertical);
		public bool IsIceFire => Input.GetKeyDown(KeyCode.Q);
		public bool IsFireMagic => Input.GetKeyDown(KeyCode.E);
	}
}