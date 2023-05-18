using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.ECS.Systems.LevelCreate
{
	public class AlienSpawn : MonoBehaviour
	{
		public FormationExtensions.FormationData data = new FormationExtensions.FormationData();

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