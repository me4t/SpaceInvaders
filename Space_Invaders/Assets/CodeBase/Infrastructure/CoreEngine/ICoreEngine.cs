using CodeBase.Configs;

namespace CodeBase.Infrastructure.CoreEngine
{
	public interface ICoreEngine
	{
		void InitSession(LevelConfig levelConfig);
		void Cleanup();
		void Tick();
		void FixedTick();
	}
}