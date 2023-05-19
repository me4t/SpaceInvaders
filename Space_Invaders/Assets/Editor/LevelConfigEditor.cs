using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Enums;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(LevelConfig))]
	public class LevelConfigEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			LevelConfig levelData = (LevelConfig)target;

			if (GUILayout.Button("Collect"))
			{

				var alienSpawn = FindObjectOfType<AlienSpawn>();
				List<AlienSpawnPoint> alienSpawnPoints = new List<AlienSpawnPoint>();

				var places = FormationExtensions.EvaluatePoints(alienSpawn.data);
				foreach (var place in places)
				{
					var point = new AlienSpawnPoint();
					point.Position = alienSpawn.transform.position + place;
					point.AlienType = AlienType.Simple;
					alienSpawnPoints.Add(point);
				}

				levelData.aliens = alienSpawnPoints;
				var initialPoint = GameObject.FindGameObjectWithTag("InitialPoint");
				levelData.PlayerSpawnPoint = new PlayerSpawnPoint();
				
				levelData.PlayerSpawnPoint.PlayerInitialPoint = initialPoint.transform.position;
				levelData.PlayerSpawnPoint.PlayerType = PlayerType.Green;
			}

			EditorUtility.SetDirty(target);
		}
	}
}
