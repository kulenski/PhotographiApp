namespace PhotographiApp.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using PhotographiApp.Data.Common.Repositories;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Web.ViewModels.Photos;

    public class PhotoService : IPhotoService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Photo> photoRespository;
        private readonly IRepository<PhotoMetadata> photoMetadataRepository;

        public PhotoService(
            IDeletableEntityRepository<Photo> photoRespository,
            IRepository<PhotoMetadata> photoMetadataRepository)
        {
            this.photoRespository = photoRespository;
            this.photoMetadataRepository = photoMetadataRepository;
        }

        public async Task CreatePhotoAsync(string userId, string groupId, string imagePath, UploadPhotoInputModel model)
        {
            var photo = new Photo
            {
                Title = model.Title,
                Description = model.Description,
                LicenseId = model.LicenseId,
                IsCommentAllowed = model.IsCommentAllowed,
                IsPrivate = model.IsPrivate,
            };

            var metadata = new PhotoMetadata();

            photo.Metadata = metadata;

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

            await this.photoMetadataRepository.AddAsync(metadata);
            await this.photoRespository.AddAsync(photo);
            await this.photoRespository.SaveChangesAsync();
        }
    }
}
