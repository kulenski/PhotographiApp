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

    public class FavoritesService : IFavoritesService
    {
        private readonly IRepository<PhotoFavorite> favoritesRepository;
        private readonly IDeletableEntityRepository<Photo> photoRepository;

        public FavoritesService(
            IRepository<PhotoFavorite> favoritesRepository,
            IDeletableEntityRepository<Photo> photoRepository)
        {
            this.favoritesRepository = favoritesRepository;
            this.photoRepository = photoRepository;
        }

        public ICollection<T> GetUserFavoritePhotos<T>(string userId)
        {
            return this.favoritesRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == userId && x.Photo.IsPrivate == false)
                .Select(x => x.Photo)
                .To<T>()
                .ToList();
        }

        public async Task ToggleAsync(string photoId, string userId)
        {
            var photo = this.photoRepository.All().Where(x => x.Id == photoId).FirstOrDefault();
            if (photo == null)
            {
                throw new Exception("Photo does not exist!");
            }

            var favorite = this.favoritesRepository.All().Where(x => x.PhotoId == photoId && x.UserId == userId).FirstOrDefault();

            if (favorite == null)
            {
                if (photo.OwnerId != userId)
                {
                    var entity = new PhotoFavorite() { Photo = photo, UserId = userId };
                    await this.favoritesRepository.AddAsync(entity);
                    await this.favoritesRepository.SaveChangesAsync();
                }
            }
            else
            {
                this.favoritesRepository.Delete(favorite);
                await this.favoritesRepository.SaveChangesAsync();
            }
        }

        public bool UserHasFavoritePhoto(string photoId, string userId)
        {
            return this.favoritesRepository.AllAsNoTracking().Where(x => x.PhotoId == photoId && x.UserId == userId).Any();
        }
    }
}
