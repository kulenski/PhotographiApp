namespace PhotographiApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PhotographiApp.Data.Common.Repositories;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Models;

    public class AlbumsService : IAlbumsService
    {
        private readonly IDeletableEntityRepository<Album> albumsRespository;

        public AlbumsService(IDeletableEntityRepository<Album> albumsRespository)
        {
            this.albumsRespository = albumsRespository;
        }

        public ICollection<AlbumDto> GetUserAlbums(string userId)
        {
            return this.albumsRespository
                .AllAsNoTracking()
                .Where(album => album.OwnerId == userId)
                .Select(m => new AlbumDto { Id = m.Id, Name = m.Name, Description = m.Description })
                .ToList();
        }
    }
}
