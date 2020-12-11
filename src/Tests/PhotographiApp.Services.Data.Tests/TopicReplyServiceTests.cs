namespace PhotographiApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Tests.Mock;
    using PhotographiApp.Services.Data.Tests.Seed;
    using Xunit;

    public class TopicReplyServiceTests
    {
        [Fact]
        public void Add_ShouldThrowErrorWhenTopicDoesNotExists()
        {
            var user = UserCreator.Create("test");
            var topic = TopicCreator.Create(user);
            var list = new List<Topic>();

            var topicRepo = DeletableEntityRepositoryMock.Get<Topic>(new List<Topic>());
            var topicRepliesRepo = EfRepositoryMock.Get<TopicReply>(new List<TopicReply>());
            var service = new TopicReplyService(topicRepliesRepo.Object, topicRepo.Object);

            Exception ex = Assert.Throws<AggregateException>(() => service.AddAsync(topic.Id, user.Id, string.Empty).Wait());

            Assert.Contains("Topic does not exist", ex.Message);
        }

        [Fact]
        public void Add_ShouldExecuteCorrectly()
        {
            var user = UserCreator.Create("test");
            var topic = TopicCreator.Create(user);
            var list = new List<TopicReply>();

            var topicRepo = DeletableEntityRepositoryMock.Get<Topic>(new List<Topic>() { topic });
            var topicRepliesRepo = EfRepositoryMock.Get<TopicReply>(list);
            var service = new TopicReplyService(topicRepliesRepo.Object, topicRepo.Object);

            service.AddAsync(topic.Id, user.Id, string.Empty).Wait();

            Assert.Single(list);
        }
    }
}
