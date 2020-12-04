namespace PhotographiApp.Services
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using PhotographiApp.Services.Interfaces;

    public class PhotoStorageService : IPhotoStorageService
    {
        private const string CloudFolder = "photographyApp";
        private const string GetImageUrlFormat = "{0}.jpg";
        private const string Thumb = "thumb";

        private readonly Cloudinary cloudUtility;

        public PhotoStorageService(Cloudinary cloudUtility)
        {
            this.cloudUtility = cloudUtility;
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile imageFile)
        {
            byte[] destinationData;

            using (var ms = new MemoryStream())
            {
                await imageFile.CopyToAsync(ms);
                destinationData = ms.ToArray();
            }

            var fileName = imageFile.FileName;

            using (var ms = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = CloudFolder,
                    File = new FileDescription(fileName, ms),
                };

                var uploadResult = await this.cloudUtility.UploadAsync(uploadParams);

                return uploadResult;
            }
        }

        public string GetImageUrl(string imagePublicId)
        {
            string imageUrl = this.cloudUtility
                                      .Api
                                      .UrlImgUp
                                      .Transform(new Transformation().Quality("auto"))
                                      .BuildUrl(string.Format(GetImageUrlFormat, imagePublicId));

            return imageUrl;
        }

        public string GetThumbnailUrl(string imagePublicId)
        {
            var imageUrl = this.cloudUtility
                                 .Api
                                 .UrlImgUp
                                 .Transform(new Transformation()
                                     .Height(200)
                                     .Width(200)
                                     .Crop(Thumb))
                                 .BuildUrl(string.Format(GetImageUrlFormat, imagePublicId));

            return imageUrl;
        }

        public async Task DeleteImages(params string[] publicIds)
        {
            var delParams = new DelResParams
            {
                PublicIds = publicIds.ToList(),
                Invalidate = true,
            };

            await this.cloudUtility.DeleteResourcesAsync(delParams);
        }
    }
}
