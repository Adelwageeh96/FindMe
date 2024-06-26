using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMe.Application.Common.Helpers
{
    public static class EmbeddingVectorConverter
    {
        public static byte[] ToByteArray(List<float> embedding)
        {
            var byteArray = new byte[embedding.Count * sizeof(float)];
            Buffer.BlockCopy(embedding.ToArray(), 0, byteArray, 0, byteArray.Length);
            return byteArray;
        }

        public static List<float> ToFloatList(byte[] byteArray)
        {
            var floatArray = new float[byteArray.Length / sizeof(float)];
            Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
            return floatArray.ToList();
        }
    }

}
