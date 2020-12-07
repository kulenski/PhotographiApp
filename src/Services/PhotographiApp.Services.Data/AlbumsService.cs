namespace PhotographiApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PhotographiApp.Data.Common.Repositories;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;
    using PhotographiApp.Web.ViewModels.Albums;

    public class AlbumsService : IAlbumsService
    {
        private readonly IDeletableEntityRepository<Album> albumsRespository;
        private readonly IRepository<PhotoAlbum> photoAlbumRepository;

        public AlbumsService(
            IDeletableEntityRepository<Album> albumsRespository,
            IRepository<PhotoAlbum> photoAlbumRepository)
        {
            this.albumsRespository = albumsRespository;
            this.photoAlbumRepository = photoAlbumRepository;
        }

        public async Task CreateAsync(CreateAlbumInputModel model, string userId)
        {
            var album = new Album
            {
                Name = model.Name,
                Description = model.Description,
                IsPrivate = model.IsPrivate,
                OwnerId = userId,
            };

            await this.albumsRespository.AddAsync(album);
            await this.albumsRespository.SaveChangesAsync();
        }

        public async Task UpdateAsync(string albumId, string userId, EditAlbumInputModel model)
        {
            var album = this.albumsRespository.All().Where(x => x.Id == albumId && x.OwnerId == userId).FirstOrDefault();
            if (album == null)
            {
                throw new Exception("Album does not exist!");
            }

            album.Name = model.Name;
            album.Description = model.Description;
            album.IsPrivate = model.IsPrivate;

            await this.albumsRespository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string albumId, string userId)
        {
            var album = this.albumsRespository.All().Where(x => x.Id == albumId && x.OwnerId == userId).FirstOrDefault();

            if (album == null)
            {
                throw new Exception("Album does not exist!");
            }

            var albumPhotos = this.photoAlbumRepository.All().Where(x => x.AlbumId == albumId).ToList();
            if (albumPhotos.Count > 0)
            {
                throw new Exception("Album contains photos!");
            }

            this.albumsRespository.Delete(album);
            await this.albumsRespository.SaveChangesAsync();
        }

        public ICollection<T> GetUserAlbums<T>(string userId, string currentUserId)
        {
            if (userId == currentUserId)
            {
                return this.albumsRespository
               .AllAsNoTracking()
               .Where(album => album.OwnerId == userId)
               .To<T>().ToList();
            }
            else
            {
                return this.albumsRespository
               .AllAsNoTracking()
               .Where(album => album.OwnerId == userId && album.IsPrivate == false)
               .To<T>().ToList();
            }
        }

        public T GetById<T>(string albumId, string userId)
        {
            var album = this.albumsRespository.All()
                .Where(x => (x.Id == albumId && (x.OwnerId == userId || x.IsPrivate == false)))
                .To<T>().FirstOrDefault();

            if (album == null)
            {
                throw new Exception("Album does not exist!");
            }

            return album;
        }

        public ICollection<T> GetAlbumPhotos<T>(string albumId, string userId)
        {
            var album = this.albumsRespository.All()
                .Where(x => (x.Id == albumId && (x.OwnerId == userId || x.IsPrivate == false)))
                .FirstOrDefault();

            bool isAlbumOwnedByCurrentUser = userId == album.OwnerId;

            if (album == null)
            {
                throw new Exception("Album does not exist!");
            }

            if (isAlbumOwnedByCurrentUser)
            {
                return this.photoAlbumRepository
                .AllAsNoTracking()
                .Where(x => x.AlbumId == albumId)
                .To<T>()
                .ToList();
            }
            else
            {
                return this.photoAlbumRepository
                .AllAsNoTracking()
                .Where(x => x.AlbumId == albumId && x.Photo.IsPrivate == false)
                .To<T>()
                .ToList();
            }
        }
    }
}
