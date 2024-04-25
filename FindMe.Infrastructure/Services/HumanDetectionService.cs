using FindMe.Application.Interfaces.Services;
using FindMe.Shared;
using Microsoft.AspNetCore.Http;
using OpenCvSharp;
using System.Reflection;

namespace FindMe.Infrastructure.Services
{
    public class HumanDetectionService : IHumanDetectionService
    {


        public async Task<HumanDetectionResponse> VerifyHumanAsync(IFormFile imageFile)
        {
            await Task.CompletedTask;
            try
            {
                // Get the assembly where the embedded resource is located
                var assembly = Assembly.GetAssembly(typeof(HumanDetectionService));

                // Construct the full resource name based on the folder and file name
                var resourceName = $"FindMe.Infrastructure.AIModels.haarcascade_frontalface_default.xml";

                // Read the XML file contents
                using var stream = assembly.GetManifestResourceStream(resourceName);
                if (stream == null)
                {
                    return new HumanDetectionResponse { Message = "Haar cascade XML file not found." };
                }

                // Read the XML file contents
                using var reader = new StreamReader(stream);
                var xmlContent = reader.ReadToEnd();

                // Create CascadeClassifier from XML content
                using var cascade = new CascadeClassifier();
                cascade.Load(xmlContent);

                byte[] imageData;
                using (var memoryStream = new MemoryStream())
                {
                    imageFile.CopyTo(memoryStream);
                    imageData = memoryStream.ToArray();
                }

                // Read and process the image file
                using var image = Cv2.ImDecode(imageData, ImreadModes.Color);


                // Detect faces in the image
                var detectedFaces = cascade.DetectMultiScale(image, 1.1, 3,OpenCvSharp.HaarDetectionTypes.ScaleImage, new Size(30, 30));

                // If exactly one face is detected, crop the face and return it as an array of bytes
                if (detectedFaces.Length == 1)
                {
                    // Get the first detected face
                    var faceRect = detectedFaces.First();

                    // Crop the detected face from the original image
                    var croppedImage = new Mat(image, faceRect);

                    // Convert the cropped image to a byte array
                    var croppedBytes = croppedImage.ToBytes();

                    // Return the cropped image bytes
                    return new HumanDetectionResponse { IsSuccess=true, Data = croppedBytes };
                }
                else
                {
                    // Zero or multiple faces detected
                    return new HumanDetectionResponse { };
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., invalid image format, file not found, etc.)
                return new HumanDetectionResponse { Message = ex.Message };
            }
        }
    }
}
