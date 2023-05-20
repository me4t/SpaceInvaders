using CodeBase.ECS.Systems.LevelCreate;

namespace CodeBase.Infrastructure.CoreEngine
{
	public interface ICoreEngine
	{
		void InitSession(LevelData levelConfig);
		void Cleanup();
		void Tick();
		bool GameOver { get; }
	}
}