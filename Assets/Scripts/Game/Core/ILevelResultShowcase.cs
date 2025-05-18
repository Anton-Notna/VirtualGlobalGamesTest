using Cysharp.Threading.Tasks;
using System.Threading;

namespace Game.Core
{
    public interface ILevelResultShowcase
    {
        public UniTask ShowResult(int levelIndex, bool completed, CancellationToken cancellationToken);
    }
}