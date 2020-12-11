namespace PhotographiApp.Services.Data.Tests.Mock
{
    using System;
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using PhotographiApp.Services.Interfaces;

    public class PhotoStorageServiceMock
    {
        public static Mock<IPhotoStorageService> Get()
        {
            var service = new Mock<IPhotoStorageService>();
            service.Setup(x => x.UploadImageAsync(It.IsAny<IFormFile>())).Returns(Task.FromResult(new ImageUploadResult() { PublicId = Guid.NewGuid().ToString() }));
            return service;
        }
    }
}
