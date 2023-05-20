using System;
using System.Collections.Generic;
using System.Text;
using CodeBase.Enums;
using CodeBase.UI;
using TMPro;
using UnityEngine;

namespace CodeBase.Services.HudService
{
	public interface IHudService
	{
		void UpdateScore(float current);
		void UpdateRound(int current);
		void UpdateHp(float current,float max);
		void UpdateBullets(Dictionary<BulletType, int> bulletOwnerBullets);
	}

	public class HudService : MonoBehaviour, IHudService
	{
		[SerializeField] private TextMeshProUGUI Score;
		[SerializeField] private TextMeshProUGUI Round;
		[SerializeField] private TextMeshProUGUI Bullets;
		[SerializeField] private HpBar hpBar;

		public void UpdateScore(float current)
		{
			Score.text = $"Score: {current}";
		}

		public void UpdateRound(int current)
		{
			Round.text = $"Round: {current}";
		}

		public void UpdateHp(float current, float max)
		{
			hpBar.ChangeFill(current/max);
		}

		public void UpdateBullets(Dictionary<BulletType, int> bullets)
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append("Bullets : ");
			foreach (var key in bullets.Keys)
				stringBuilder.Append(Environment.NewLine + $"{key.ConvertToString()} : {bullets[key]}");
			
			Bullets.text = stringBuilder.ToString();

		}

	}
}