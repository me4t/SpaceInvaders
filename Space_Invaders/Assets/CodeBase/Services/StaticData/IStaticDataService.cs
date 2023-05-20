using CodeBase.Configs;
using CodeBase.ECS.Systems.Factories;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Enums;

namespace CodeBase.Services.StaticData
{
	public interface IStaticDataService
	{
		void Load();
		LevelData ForLevelTemplate(int key);
		int TemplateCount { get; }
		AlienCreateData ForAlien(AlienType id);
		PlayerData ForPlayer(PlayerType id);
		BulletCreateData ForBullet(BulletType id);
		BulletLootData ForBulletLoot(BulletType id);
		WindowData ForWindow(WindowId id);
	}
}