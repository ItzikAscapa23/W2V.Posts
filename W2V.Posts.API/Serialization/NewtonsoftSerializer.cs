using System;
using System.Text;
using Newtonsoft.Json;

namespace W2V.Posts.API.Serialization
{
    public class NewtonsoftSerializer : ISerializer
    {
        /// <summary>
        /// Converting byte[] to string using utf8 encoding.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public T Deserialize<T>(byte[] arr)
        {
            try
            {
                //TODO: add overload to pass specific encoder.
                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(arr), new JsonSerializerSettings()
                    { TypeNameHandling = TypeNameHandling.Auto });
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public T Deserialize<T>(string objStr)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(objStr, new JsonSerializerSettings()
                    { TypeNameHandling = TypeNameHandling.Auto });
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        public byte[] SerializeByteArray<T>(T obj)
        {
            try
            {
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
                    { TypeNameHandling = TypeNameHandling.Auto }));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string SerializeString<T>(T obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
                    { TypeNameHandling = TypeNameHandling.Auto });
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
