namespace PhotographiApp.Services.Data
{
    using System.Threading.Tasks;

    using PhotographiApp.Web.ViewModels.Photos;

    public interface IPhotoService
    {
        Task CreatePhotoAsync(string userId, string groupId, string imagePath, UploadPhotoInputModel model);
    }
}
