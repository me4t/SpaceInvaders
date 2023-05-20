using CodeBase.Configs;
using CodeBase.Enums;

namespace CodeBase.Services.StaticData
{
	public interface IStaticDataService
	{
		void Load();
		LevelConfig ForLevelTemplate(int key);
		int TemplateCount { get; }
		AlienConfig ForAlien(AlienType id);
		PlayerConfig ForPlayer(PlayerType spawnPointPlayerType);
		BulletConfig ForBullet(BulletType ice);
		BulletLootConfig ForBulletLoot(BulletType scoreLootType);
		WindowConfig ForWindow(WindowId windowId);
	}

	public interface IPlayerProgressService
	{
		PlayerProgress Progress { get; set; }
	}

	public class PlayerProgressService:IPlayerProgressService
	{
		public PlayerProgress Progress { get; set; }
	}

	public class PlayerProgress
	{
		public int RoundCount = 0;
		public float Score = 0;
	}
}