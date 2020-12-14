namespace PhotographiApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using PhotographiApp.Data.Common.Repositories;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Interfaces;
    using PhotographiApp.Services.Interfaces;
    using PhotographiApp.Services.Mapping;
    using PhotographiApp.Services.Models;
    using PhotographiApp.Web.ViewModels.Photos;

    public class PhotoService : IPhotoService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Photo> photoRespository;
        private readonly IPhotoStorageService photoStorageService;
        private readonly IPhotoMetadataService photoMetadataService;
        private readonly IRepository<PhotoAlbum> photoAlbumRepository;

        public PhotoService(
            IDeletableEntityRepository<Photo> photoRespository,
            IPhotoStorageService photoStorageService,
            IPhotoMetadataService photoMetadataService,
            IRepository<PhotoAlbum> photoAlbumRepository)
        {
            this.photoRespository = photoRespository;
            this.photoStorageService = photoStorageService;
            this.photoMetadataService = photoMetadataService;
            this.photoAlbumRepository = photoAlbumRepository;
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

            var extension = Path.GetExtension(model.File.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.ToLower().EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var metadata = this.photoMetadataService.GetMetadata(model.File);
            photo = this.WriteMetadataToPhoto(photo, metadata);

            var uploadResult = await this.photoStorageService.UploadImageAsync(model.File);
            var publicId = uploadResult.PublicId;

            photo.Href = this.photoStorageService.GetImageUrl(publicId);
            photo.ThumbnailHref = this.photoStorageService.GetThumbnailUrl(publicId);
            photo.PublicId = publicId;

            await this.photoRespository.AddAsync(photo);
            await this.photoRespository.SaveChangesAsync();
        }

        public async Task UpdatePhotoAsync(string photoId, string userId, EditPhotoViewModel model)
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

            var photoAlbums = this.photoAlbumRepository.All().Where(x => x.PhotoId == photoId).ToList();
            await this.photoStorageService.DeleteImages(new[] { photo.PublicId });
            foreach (var photoAlbum in photoAlbums)
            {
                this.photoAlbumRepository.Delete(photoAlbum);
            }

            this.photoRespository.Delete(photo);
            await this.photoRespository.SaveChangesAsync();
            await this.photoAlbumRepository.SaveChangesAsync();
        }

        public T GetById<T>(string photoId, string userId)
        {
            var photo = this.photoRespository.AllAsNoTracking().Where(x => x.Id == photoId && (x.OwnerId == userId || x.IsPrivate == false))
            .To<T>().FirstOrDefault();

            return photo;
        }

        public ICollection<T> GetAllByUserId<T>(string userId, string currentUserId)
        {
            if (userId == currentUserId)
            {
                return this.photoRespository.AllAsNoTracking().Where(x => x.OwnerId == userId)
               .To<T>().ToList();
            }
            else
            {
                return this.photoRespository.AllAsNoTracking().Where(x => x.OwnerId == userId && x.IsPrivate == false)
               .To<T>().ToList();
            }
        }

        public ICollection<T> GetLatestPublic<T>()
        {
            var photos = this.photoRespository
                    .AllAsNoTracking()
                    .Where(x => x.IsPrivate == false)
                    .OrderByDescending(x => x.CreatedOn)
                    .Take(20)
                    .To<T>()
                    .ToList();
            return photos;
        }

        private Photo WriteMetadataToPhoto(Photo photo, PhotoMetadata metadata)
        {
            photo.Camera = metadata.Camera;
            photo.Aperture = metadata.Aperture;
            photo.DateTaken = metadata.DateTaken;
            photo.ExposureTime = metadata.ExposureTime;
            photo.Flash = metadata.Flash;
            photo.Iso = metadata.Iso;
            return photo;
        }
    }
}
