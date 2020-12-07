namespace PhotographiApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PhotographiApp.Web.ViewModels.Albums;

    public interface IAlbumsService
    {
        Task CreateAsync(CreateAlbumInputModel model, string userId);

        Task UpdateAsync(string albumId, string userId, EditAlbumInputModel model);

        Task AddPhotoAsync(string albumId, string photoId);

        Task RemovePhotoAsync(string albumId, string photoId);

        Task DeleteAsync(string albumId, string userId);

        ICollection<T> GetUserAlbums<T>(string userId, string currentUserId);

        T GetById<T>(string albumId, string userId);

        ICollection<T> GetAlbumPhotos<T>(string albumId, string userId);
    }
}
