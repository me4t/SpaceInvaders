using CodeBase.Enums;
using TMPro;
using UnityEngine;

namespace CodeBase.Services.SceneLoader
{
	public interface IHudService
	{
		void UpdateScore(float current);
		void UpdateRound(int current);
		void UpdateBullets(int current, BulletType bulletType);
	}

	public class HudService : MonoBehaviour, IHudService
	{
		[SerializeField] private TextMeshProUGUI Score;
		[SerializeField] private TextMeshProUGUI Round;
		[SerializeField] private TextMeshProUGUI Bullets;

		public void UpdateScore(float current)
		{
			Score.text = $"Score: {current}";
		}

		public void UpdateRound(int current)
		{
			Round.text = $"Round: {current}";
		}

		public void UpdateBullets(int current, BulletType bulletType)
		{
			Bullets.text = $"Bullets : {bulletType.ConvertToString()} \nCount : {current}";
		}
	}
}