namespace Shared.Storage
{
    public interface IStorage 
    {
        public bool Load<T>(string key, out T data);

        public void Save<T>(string key, T data);
    }
}