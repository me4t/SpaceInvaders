using System.Collections.Generic;
using System.Linq;
using CodeBase.Configs;
using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class StaticDataService : IStaticDataService
	{
		private Dictionary<AlienType, AlienConfig> aliens;
		private const string AlienConfigsPath = "Static Data/Islands";

		public void Load()
		{
			aliens = Resources
				.LoadAll<AlienConfig>(AlienConfigsPath)
				.ToDictionary(x => x.Type, x => x);
		}
	}

	public interface IStaticDataService 
	{
		void Load();
	}
}