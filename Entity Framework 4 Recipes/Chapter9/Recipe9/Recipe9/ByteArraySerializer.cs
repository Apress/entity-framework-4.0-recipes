using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Runtime.Serialization;

namespace Recipe9
{
    public class ByteArraySerializer
    {
        public static byte[] ToByteArray<T>(T graph)
        {
            var stream = new MemoryStream();
            var serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(stream, graph);
            return stream.ToArray();
        }

        public static T ToObject<T>(byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            stream.Position = 0;
            var serializer = new DataContractSerializer(typeof(T));
            return (T)serializer.ReadObject(stream);
        }
    }
}