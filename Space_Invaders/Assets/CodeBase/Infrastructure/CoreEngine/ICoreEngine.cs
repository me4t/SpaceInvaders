using CodeBase.Data;

namespace CodeBase.Infrastructure.CoreEngine
{
	public interface ICoreEngine
	{
		void InitSession(LevelData levelConfig);
		void Tick();
		bool GameOver { get; }
	}
}