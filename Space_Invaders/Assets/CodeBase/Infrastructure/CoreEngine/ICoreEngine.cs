using CodeBase.Data;
using CodeBase.ECS.Systems.Create;

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