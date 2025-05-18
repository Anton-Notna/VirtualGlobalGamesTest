namespace Shared.Storage
{
    public interface ISerializableObject<T>
    {
        public void SetData(T data);

        public T GetData();
    }
}