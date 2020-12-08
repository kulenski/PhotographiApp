namespace PhotographiApp.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPhotoAlbumService
    {
        Task AddPhotoAsync(string albumId, string photoId, string userId);

        Task RemovePhotoAsync(string albumId, string photoId, string userId);

        ICollection<T> GetAllUnusedAlbums<T>(string photoId, string userId);

        ICollection<T> GetAllUsedAlbums<T>(string photoId, string userId);

        ICollection<T> GetAllByPhotoId<T>(string photoId);
    }
}
