namespace PhotographiApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PhotographiApp.Web.ViewModels.Photos;

    public interface IPhotoService
    {
        Task CreatePhotoAsync(string userId, string imagePath, CreatePhotoInputModel model);

        Task UpdatePhotoAsync(string photoId, string userId, EditPhotoViewModel model);

        Task DeletePhotoAsync(string photoId, string userId);

        T GetById<T>(string photoId, string userId);

        ICollection<T> GetAllByUserId<T>(string userId);

        ICollection<T> GetLatestPublic<T>();
    }
}
