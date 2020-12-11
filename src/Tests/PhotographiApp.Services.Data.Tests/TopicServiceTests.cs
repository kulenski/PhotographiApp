namespace PhotographiApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Tests.Mock;
    using PhotographiApp.Services.Data.Tests.Seed;
    using PhotographiApp.Services.Mapping;
    using PhotographiApp.Web.ViewModels;
    using PhotographiApp.Web.ViewModels.Topic;
    using Xunit;

    public class TopicServiceTests
    {
        public TopicServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }

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
            var user = UserCreator.Create("test");
            var visitor = UserCreator.Create("visitor");
            var other = UserCreator.Create("other");
            var list = new List<Topic>() { TopicCreator.Create(user), TopicCreator.Create(visitor) };

            var topicRepo = DeletableEntityRepositoryMock.Get<Topic>(list);
            var service = new TopicService(topicRepo.Object);

            var userTopics = service.GetAllByUser<TopicViewModel>(user.Id);
            var visitorTopics = service.GetAllByUser<TopicViewModel>(visitor.Id);
            var otherTopics = service.GetAllByUser<TopicViewModel>(other.Id);

            Assert.Single(userTopics);
            Assert.Single(visitorTopics);
            Assert.Empty(otherTopics);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectData()
        {
            var user = UserCreator.Create("test");
            var topic = TopicCreator.Create(user);

            var topicRepo = DeletableEntityRepositoryMock.Get<Topic>(new List<Topic>() { topic });
            var service = new TopicService(topicRepo.Object);

            var result = service.GetById<TopicViewModel>(topic.Id);

            Assert.NotNull(result);
            Assert.Equal(topic.Id, result.Id);
        }

        [Fact]
        public void GetLatest_ShouldReturnCorrectData()
        {
            var user = UserCreator.Create("test");
            var list = new List<Topic>()
            {
                TopicCreator.Create(user),
                TopicCreator.Create(user),
            };

            var topicRepo = DeletableEntityRepositoryMock.Get<Topic>(list);
            var service = new TopicService(topicRepo.Object);

            var result = service.GetLatest<TopicViewModel>();

            Assert.Equal(2, result.Count);
        }
    }
}
