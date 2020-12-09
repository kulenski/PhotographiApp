namespace PhotographiApp.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFavoritesService
    {
        Task ToggleAsync(string photoId, string userId);

        ICollection<T> GetUserFavoritePhotos<T>(string userId);

        bool UserHasFavoritePhoto(string photoId, string userId);
    }
}
