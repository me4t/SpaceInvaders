using System;
using System.Collections.Generic;
using System.Text;
using CodeBase.Enums;
using TMPro;
using UnityEngine;

namespace CodeBase.Services.SceneLoader
{
	public interface IHudService
	{
		void UpdateScore(float current);
		void UpdateRound(int current);
		void UpdateBullets(Dictionary<BulletType, int> bulletOwnerBullets);
	}

	public class HudService : MonoBehaviour, IHudService
	{
		[SerializeField] private TextMeshProUGUI Score;
		[SerializeField] private TextMeshProUGUI Round;
		[SerializeField] private TextMeshProUGUI Bullets;
		private StringBuilder stringBuilder;

		public void UpdateScore(float current)
		{
			Score.text = $"Score: {current}";
		}

		public void UpdateRound(int current)
		{
			Round.text = $"Round: {current}";
		}

		public void UpdateBullets(Dictionary<BulletType, int> bullets)
		{
			stringBuilder = new StringBuilder();
			foreach (var count in bullets.Keys)
			{
				stringBuilder.Append("Bullets : ");
				stringBuilder.Append($"Bullets : {count.ToString()} \nCount : {bullets[count]}");
			}
			Bullets.text = stringBuilder.ToString();

		}

	}
}