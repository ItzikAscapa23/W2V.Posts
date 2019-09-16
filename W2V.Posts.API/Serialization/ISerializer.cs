namespace W2V.Posts.API.Serialization
{
    public interface ISerializer
    {
        byte[] SerializeByteArray<T>(T obj);
        string SerializeString<T>(T obj);
        T Deserialize<T>(byte[] arr);
        T Deserialize<T>(string objArr);
    }
}
