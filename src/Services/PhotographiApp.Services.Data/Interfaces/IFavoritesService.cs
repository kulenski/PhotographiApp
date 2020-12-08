namespace PhotographiApp.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface IFavoritesService
    {
        Task ToggleAsync(string photoId, string userId);

        bool UserHasFavoritePhoto(string photoId, string userId);
    }
}
