namespace PhotographiApp.Services.Interfaces
{
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public interface IPhotoStorageService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile imageFile);

        string GetImageUrl(string imagePublicId);

        string GetThumbnailUrl(string imagePublicId);

        Task DeleteImages(params string[] publicIds);
    }
}
