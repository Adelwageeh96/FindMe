using FindMe.Shared;
using Microsoft.AspNetCore.Http;


namespace FindMe.Application.Interfaces.Services
{
    public interface IHumanDetectionService
    {
        public Task<HumanDetectionResponse> VerifyHumanAsync(IFormFile imageFile);
    }
}
