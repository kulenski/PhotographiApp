namespace PhotographiApp.Services.Data.Tests.Mock
{
    using Microsoft.AspNetCore.Http;
    using Moq;
    using PhotographiApp.Services.Interfaces;
    using PhotographiApp.Services.Models;

    public class PhotoMetadataServiceMock
    {
        public static Mock<IPhotoMetadataService> Get()
        {
            var service = new Mock<IPhotoMetadataService>();
            service.Setup(x => x.GetMetadata(It.IsAny<IFormFile>())).Returns(new PhotoMetadata() { });
            return service;
        }
    }
}
