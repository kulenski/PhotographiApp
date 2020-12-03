namespace PhotographiApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using PhotographiApp.Data.Common.Repositories;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;
    using PhotographiApp.Web.ViewModels.Photos;

    public class PhotoService : IPhotoService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Photo> photoRespository;

        public PhotoService(IDeletableEntityRepository<Photo> photoRespository)
        {
            this.photoRespository = photoRespository;
        }

        public async Task CreatePhotoAsync(string userId, string imagePath, CreatePhotoInputModel model)
        {
            var photo = new Photo
            {
                Title = model.Title,
                Description = model.Description,
                LicenseId = model.LicenseId,
                IsCommentAllowed = model.IsCommentAllowed,
                IsPrivate = model.IsPrivate,
                OwnerId = userId,
            };

            Directory.CreateDirectory($"{imagePath}/photos/");

            var extension = Path.GetExtension(model.File.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.ToLower().EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var physicalPath = $"{imagePath}/photos/{photo.Id}.{extension}";
            photo.Href = $"/photos/{photo.Id}.{extension}";

            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.File.CopyToAsync(fileStream);

            await this.photoRespository.AddAsync(photo);
            await this.photoRespository.SaveChangesAsync();
        }

        public async Task UpdatePhotoAsync(string photoId, string userId, UpdatePhotoInputModel model)
        {
            var photo = this.photoRespository.All().Where(x => x.Id == photoId && x.OwnerId == userId).FirstOrDefault();

            if (photo == null)
            {
                throw new Exception("Such photo does not exists!");
            }

            photo.Title = model.Title;
            photo.Description = model.Description;
            photo.LicenseId = model.LicenseId;
            photo.IsCommentAllowed = model.IsCommentAllowed;
            photo.IsPrivate = model.IsPrivate;

            await this.photoRespository.SaveChangesAsync();
        }

        public async Task DeletePhotoAsync(string photoId, string userId)
        {
            var photo = this.photoRespository.All().Where(x => x.Id == photoId && x.OwnerId == userId).FirstOrDefault();

            if (photo == null)
            {
                throw new Exception("Such photo does not exists!");
            }

            this.photoRespository.Delete(photo);
            await this.photoRespository.SaveChangesAsync();
        }

        public T GetById<T>(string photoId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                var photo = this.photoRespository.AllAsNoTracking().Where(x => x.Id == photoId && x.IsPrivate == false)
                .To<T>().FirstOrDefault();

                return photo;
            }
            else
            {
                var photo = this.photoRespository.AllAsNoTracking().Where(x => x.Id == photoId && x.OwnerId == userId)
                .To<T>().FirstOrDefault();

                return photo;
            }
        }

        public ICollection<T> GetAllByUserId<T>(string userId)
        {
            var photos = this.photoRespository.AllAsNoTracking().Where(x => x.OwnerId == userId)
               .To<T>().ToList();

            return photos;
        }
    }
}
