using CodeBase.Data;
using CodeBase.Enums;
using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.MonoBehaviourView
{
	public class AlienSpawn : MonoBehaviour
	{
		public FormationData data = new FormationData();

		private void OnDrawGizmos()
		{
			if (Application.isPlaying) return;

			for (int i = 0; i < data.Amount; i++)
			{
				var formation = FormationExtensions.EvaluatePoints(data);
				Gizmos.color = data.GizmoColor;

				foreach (var pos in formation)
				{
					Gizmos.DrawCube(transform.position + pos + new Vector3(0, data.UnitGizmoSize.y * 0.5f, 0),
						data.UnitGizmoSize);
				}
			}
		}
	}
}