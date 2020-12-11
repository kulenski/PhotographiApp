namespace PhotographiApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Tests.Mock;
    using PhotographiApp.Services.Data.Tests.Seed;
    using Xunit;

    public class TopicServiceTests
    {
        [Fact]
        public void Create_ShouldExecuteCorrectly()
        {
            var user = UserCreator.Create("test");
            var list = new List<Topic>();

            var topicRepo = DeletableEntityRepositoryMock.Get<Topic>(list);
            var service = new TopicService(topicRepo.Object);

            service.CreateAsync(user.Id, string.Empty, string.Empty).Wait();

            Assert.Single(list);
        }

        [Fact]
        public void Update_ShouldThrowExceptionWhenTopicDoesNotExists()
        {
            var user = UserCreator.Create("test");
            var topic = TopicCreator.Create(user);
            var list = new List<Topic>();

            var topicRepo = DeletableEntityRepositoryMock.Get<Topic>(list);
            var service = new TopicService(topicRepo.Object);

            Exception ex = Assert.Throws<AggregateException>(() => service.UpdateAsync(topic.Id, user.Id, string.Empty, string.Empty).Wait());

            Assert.Contains("Topic does not exist", ex.Message);
        }

        [Fact]
        public void Update_ShouldThrowExceptionWhenUserIsNotOwner()
        {
            var user = UserCreator.Create("test");
            var visitor = UserCreator.Create("visitor");
            var topic = TopicCreator.Create(user);
            var list = new List<Topic>() { topic };

            var topicRepo = DeletableEntityRepositoryMock.Get<Topic>(list);
            var service = new TopicService(topicRepo.Object);

            Exception ex = Assert.Throws<AggregateException>(() => service.UpdateAsync(topic.Id, visitor.Id, string.Empty, string.Empty).Wait());

            Assert.Contains("Topic does not exist", ex.Message);
        }

        [Fact]
        public void GetAllByUser_ShouldReturnCorrectData()
        {
            // TODO: To be implemented when figure out how to fix automapper error.
        }

        [Fact]
        public void GetById_ShouldReturnCorrectData()
        {
            // TODO: To be implemented when figure out how to fix automapper error.
        }

        [Fact]
        public void GetLatest_ShouldReturnCorrectData()
        {
            // TODO: To be implemented when figure out how to fix automapper error.
        }
    }
}
