using TMPro;
using UnityEngine;

namespace CodeBase.Services.SceneLoader
{
	public interface IHudService
	{
		void UpdateScore(float current);
		void UpdateRound(int current);
	}

	public class HudService : MonoBehaviour, IHudService
	{
		[SerializeField] private TextMeshProUGUI Score;
		[SerializeField] private TextMeshProUGUI Round;

		public void UpdateScore(float current)
		{
			Score.text = $"Score: {current}";
		}

		public void UpdateRound(int current)
		{
			Round.text = $"Round: {current}";
		}
	}
}