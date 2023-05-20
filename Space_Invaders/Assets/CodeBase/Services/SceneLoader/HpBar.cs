using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Services.SceneLoader
{
	public class HpBar : MonoBehaviour
	{
		[SerializeField] private Image bar;
		public void ChangeFill(float value)
		{
			bar.fillAmount = value;
		}
	}
}