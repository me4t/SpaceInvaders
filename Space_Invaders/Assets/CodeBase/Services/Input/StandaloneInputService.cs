using CodeBase.Services.SceneLoader;
using UnityEngine;

namespace CodeBase.Services.Input
{
	public class StandaloneInputService : IInputService
	{
		public float Horizontal => UnityEngine.Input.GetAxis("Horizontal");
		public float Vertical => UnityEngine.Input.GetAxis("Vertical");
		public Vector2 Axis => new Vector2(Horizontal, Vertical);
		public bool IsIceFire => UnityEngine.Input.GetKeyDown(KeyCode.Q);
		public bool IsFireMagic => UnityEngine.Input.GetKeyDown(KeyCode.E);
	}
}