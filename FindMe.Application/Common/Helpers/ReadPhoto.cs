
using System.Reflection;


namespace FindMe.Application.Common.Helpers
{
    public class ReadPhoto
    {
        public static byte[] ReadEmbeddedPhoto(string fileName)
        {
            var resourceName = $"FindMe.Application.Common.Photos.{fileName}";
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new ArgumentException($"Default photo '{fileName}' not found.");
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
