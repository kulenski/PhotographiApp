namespace PhotographiApp.Services.Data
{
    using System.Collections.Generic;

    using PhotographiApp.Services.Data.Models;

    public interface IAlbumsService
    {
        ICollection<AlbumDto> GetUserAlbums(string userId);
    }
}
