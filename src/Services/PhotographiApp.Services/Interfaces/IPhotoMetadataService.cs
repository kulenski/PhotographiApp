namespace PhotographiApp.Services.Interfaces
{
    using Microsoft.AspNetCore.Http;
    using PhotographiApp.Services.Models;

    public interface IPhotoMetadataService
    {
        PhotoMetadata GetMetadata(IFormFile file);
    }
}
