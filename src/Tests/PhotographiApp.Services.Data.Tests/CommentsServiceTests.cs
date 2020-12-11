namespace PhotographiApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Tests.Mock;
    using PhotographiApp.Services.Data.Tests.Seed;
    using Xunit;

    public class CommentsServiceTests
    {
        [Fact]
        public void Add_ShouldThrowErrorWhenPhotoDoesNotExist()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, true);
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>());
            var commentsRepo = EfRepositoryMock.Get<Comment>(new List<Comment>());

            var service = new CommentService(photosRepo.Object, commentsRepo.Object);

            Exception ex = Assert.Throws<AggregateException>(() => service.AddAsync(photo.Id, user.Id, "comment").Wait());
            Assert.Contains("Photo does not exists!", ex.Message);
        }

        [Fact]
        public void Add_ShouldThrowErrorWhenCommentsAreNotAllowed()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, false);
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });
            var commentsRepo = EfRepositoryMock.Get<Comment>(new List<Comment>());

            var service = new CommentService(photosRepo.Object, commentsRepo.Object);

            Exception ex = Assert.Throws<AggregateException>(() => service.AddAsync(photo.Id, user.Id, "comment").Wait());
            Assert.Contains("Comments are not allowed for photo!", ex.Message);
        }

        [Fact]
        public void Add_ShouldAddSucessfully()
        {
            var user = UserCreator.Create("test");
            var photo = PhotoCreator.Create(user, false, true);
            var comments = new List<Comment>();
            var photosRepo = DeletableEntityRepositoryMock.Get<Photo>(new List<Photo>() { photo });
            var commentsRepo = EfRepositoryMock.Get<Comment>(comments);

            var service = new CommentService(photosRepo.Object, commentsRepo.Object);

            service.AddAsync(photo.Id, user.Id, "comment").Wait();
            Assert.Single(comments);
        }
    }
}
