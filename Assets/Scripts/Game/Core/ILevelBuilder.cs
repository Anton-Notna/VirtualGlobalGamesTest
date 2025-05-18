namespace Game.Core
{
    public interface ILevelBuilder
    {
        public ILevelInfo Build(int levelIndex);

        public void Cleanup();
    }
}