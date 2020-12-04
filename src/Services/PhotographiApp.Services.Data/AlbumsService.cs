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
        private readonly IDeletableEntityRepository<Photo> photoRepository;
        private readonly IRepository<PhotoAlbum> photoAlbumRepository;

        public AlbumsService(
            IDeletableEntityRepository<Album> albumsRespository,
            IDeletableEntityRepository<Photo> photoRepository,
            IRepository<PhotoAlbum> photoAlbumRepository)
        {
            this.albumsRespository = albumsRespository;
            this.photoRepository = photoRepository;
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

        public async Task AddPhotoAsync(string albumId, string photoId)
        {
            var album = this.albumsRespository.All().Where(a => a.Id == albumId).FirstOrDefault();

            if (album == null)
            {
                throw new Exception("Album does not exist!");
            }

            var photo = this.photoRepository.AllAsNoTracking().Where(p => p.Id == photoId).FirstOrDefault();

            if (photo == null)
            {
                throw new Exception("Photo does not exist!");
            }

            var photoAlbum = this.photoAlbumRepository.All().Where(pa => pa.PhotoId == photoId && pa.AlbumId == albumId).FirstOrDefault();

            if (photoAlbum != null)
            {
                throw new Exception("Photo is already added to the album!");
            }

            var newPhotoAlbum = new PhotoAlbum
            {
                Album = album,
                Photo = photo,
            };

            await this.photoAlbumRepository.AddAsync(newPhotoAlbum);
            await this.photoAlbumRepository.SaveChangesAsync();
        }

        public async Task RemovePhotoAsync(string albumId, string photoId)
        {
            var album = this.albumsRespository.All().Where(a => a.Id == albumId).FirstOrDefault();

            if (album == null)
            {
                throw new Exception("Album does not exist!");
            }

            var photo = this.photoRepository.AllAsNoTracking().Where(p => p.Id == photoId).FirstOrDefault();

            if (photo == null)
            {
                throw new Exception("Photo does not exist!");
            }

            var photoAlbum = this.photoAlbumRepository.All().Where(pa => pa.PhotoId == photoId && pa.AlbumId == albumId).FirstOrDefault();

            if (photoAlbum == null)
            {
                throw new Exception("Such mapping between photo and album does not exist!");
            }

            this.photoAlbumRepository.Delete(photoAlbum);
            await this.photoAlbumRepository.SaveChangesAsync();
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

        public ICollection<T> GetUserAlbums<T>(string userId)
        {
            return this.albumsRespository
                .AllAsNoTracking()
                .Where(album => album.OwnerId == userId)
                .To<T>().ToList();
        }

        public T GetById<T>(string albumId, string userId)
        {
            var album = this.albumsRespository.All().Where(x => x.Id == albumId && x.OwnerId == userId).To<T>().FirstOrDefault();

            if (album == null)
            {
                throw new Exception("Album does not exist!");
            }

            return album;
        }
    }
}
