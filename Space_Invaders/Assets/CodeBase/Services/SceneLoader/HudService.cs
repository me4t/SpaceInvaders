using TMPro;
using UnityEngine;

namespace CodeBase.Services.SceneLoader
{
	public interface IHudService
	{
		void UpdateScore(float current);
	}

	public class HudService : MonoBehaviour, IHudService
	{
		[SerializeField] private TextMeshProUGUI Score;

		public void UpdateScore(float current)
		{
			Score.text = $"Score: {current}";
		}
	}
}