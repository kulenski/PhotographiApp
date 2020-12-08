namespace PhotographiApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PhotographiApp.Data.Common.Repositories;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Interfaces;
    using PhotographiApp.Services.Mapping;

    public class PhotoAlbumService : IPhotoAlbumService
    {
        private readonly IDeletableEntityRepository<Photo> photoRepository;
        private readonly IDeletableEntityRepository<Album> albumsRespository;
        private readonly IRepository<PhotoAlbum> photoAlbumRepository;

        public PhotoAlbumService(
            IDeletableEntityRepository<Photo> photoRepository,
            IDeletableEntityRepository<Album> albumsRespository,
            IRepository<PhotoAlbum> photoAlbumRepository)
        {
            this.photoRepository = photoRepository;
            this.albumsRespository = albumsRespository;
            this.photoAlbumRepository = photoAlbumRepository;
        }

        public async Task AddPhotoAsync(string albumId, string photoId, string userId)
        {
            var album = this.albumsRespository.All().Where(a => a.Id == albumId && a.OwnerId == userId).FirstOrDefault();

            if (album == null)
            {
                throw new Exception("Album does not exist!");
            }

            var photo = this.photoRepository.AllAsNoTracking().Where(p => p.Id == photoId && p.OwnerId == userId).FirstOrDefault();

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
                AlbumId = album.Id,
                PhotoId = photo.Id,
            };

            await this.photoAlbumRepository.AddAsync(newPhotoAlbum);
            await this.photoAlbumRepository.SaveChangesAsync();
        }

        public async Task RemovePhotoAsync(string albumId, string photoId, string userId)
        {
            var album = this.albumsRespository.All().Where(a => a.Id == albumId && a.OwnerId == userId).FirstOrDefault();

            if (album == null)
            {
                throw new Exception("Album does not exist!");
            }

            var photo = this.photoRepository.AllAsNoTracking().Where(p => p.Id == photoId && p.OwnerId == userId).FirstOrDefault();

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

        public ICollection<T> GetAllUnusedAlbums<T>(string photoId, string userId)
        {
            var usedAlbumIds = this.photoAlbumRepository.AllAsNoTracking().Where(x => x.PhotoId == photoId && x.Photo.OwnerId == userId).Select(x => x.AlbumId).ToList();
            var query = this.albumsRespository.AllAsNoTracking().Where(x => x.OwnerId == userId);
            if (usedAlbumIds.Count > 0)
            {
                foreach (var albumId in usedAlbumIds)
                {
                    query = query.Where(x => x.Id != albumId);
                }
            }

            return query.To<T>().ToList();
        }

        public ICollection<T> GetAllUsedAlbums<T>(string photoId, string userId)
        {
            var usedAlbumIds = this.photoAlbumRepository.AllAsNoTracking().Where(x => x.PhotoId == photoId && x.Photo.OwnerId == userId).Select(x => x.AlbumId).ToList();
            var albums = this.albumsRespository.AllAsNoTracking().Where(x => x.OwnerId == userId).ToList();
            var result = new List<T>();

            if (usedAlbumIds.Count > 0)
            {
                var foundAlbums = new List<Album>();
                foreach (var albumId in usedAlbumIds)
                {
                    var foundAlbum = albums.Where(x => x.Id == albumId).FirstOrDefault();
                    if (foundAlbum != null)
                    {
                        foundAlbums.Add(foundAlbum);
                    }
                }

                return foundAlbums.AsQueryable().To<T>().ToList();
            }

            return result;
        }

        public ICollection<T> GetAllByPhotoId<T>(string photoId)
        {
            var albums = this.photoAlbumRepository.AllAsNoTracking().Where(x => x.PhotoId == photoId).To<T>().ToList();
            return albums;
        }
    }
}
