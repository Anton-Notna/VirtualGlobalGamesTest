using Cysharp.Threading.Tasks;
using Shared.Storage;
using System.Threading;

namespace Game.Core
{
    public class GameScenario
    {
        private readonly ILevelBuilder _builder;
        private readonly ILevelResultShowcase _showcase;
        private readonly DataSaveLoad _data;

        public GameScenario(ILevelBuilder builder, ILevelResultShowcase showcase, DataSaveLoad data)
        {
            _builder = builder;
            _showcase = showcase;
            _data = data;
        }

        public async UniTask ProcessGame(CancellationToken cancellationToken)
        {
            int levelIndex = 0;
            while (true)
            {
                bool success = await ProcessLevel(levelIndex, cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                    return;

                await _showcase.ShowResult(levelIndex, success, cancellationToken);

                if (success)
                {
                    levelIndex++;
                }
                else
                {
                    levelIndex = 0;
                    _data.Reset();
                }
            }
        }

        private async UniTask<bool> ProcessLevel(int level, CancellationToken cancellationToken)
        {
            ILevelInfo info = _builder.Build(level);
            _data.Load();

            while (info.AlivePlayer && info.AliveEnemies > 0 && cancellationToken.IsCancellationRequested == false)
                await UniTask.Yield();

            if (cancellationToken.IsCancellationRequested)
                return false;

            bool success = info.AlivePlayer;

            _data.Save();
            info.Dispose();
            _builder.Cleanup();

            return success;
        }
    }
}