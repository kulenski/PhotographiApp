namespace PhotographiApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PhotographiApp.Web.ViewModels.Albums;

    public interface IAlbumsService
    {
        Task CreateAsync(CreateAlbumInputModel model, string userId);

        Task AddPhotoAsync(string albumId, string photoId);

        Task RemovePhotoAsync(string albumId, string photoId);

        Task DeleteAsync(string albumId);

        ICollection<T> GetUserAlbums<T>(string userId);
    }
}
