using System.ComponentModel;

namespace CodeBase.Enums
{
	public enum ResourcePath
	{
		[Description("Views/Aliens/AlienSimple")] AlienSimple = 1,
		[Description("Views/Player/PlayerGreen")] PlayerGreen = 20,
		[Description("Views/Bullets/IceBullet")] IceBullet = 40,
		[Description("Views/Bullets/FireBullet")] FireBullet = 41,
		[Description("Views/Bullets/MagicBullet")] MagicBullet = 42,
		[Description("Views/Loot/IceLootBullet")] IceLootBullet = 50,
		[Description("Views/Loot/FireLootBullet")] FireLootBullet = 51,
	}
}