namespace Game.Core
{
    public interface IGameData
    {
        public void Save();

        public void Load();

        public void Reset();
    }
}