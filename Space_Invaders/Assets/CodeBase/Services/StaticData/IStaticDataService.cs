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
	}
}