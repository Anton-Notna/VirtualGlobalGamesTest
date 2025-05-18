using System;

namespace Game.Core
{
    public interface ILevelInfo : IDisposable
    {
        public int AliveEnemies { get; }

        public bool AlivePlayer { get; }
    }
}