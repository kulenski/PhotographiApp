namespace PhotographiApp.Services.Data.Tests.Seed
{
    using System;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Data.Models.Application;

    public class TopicReplyCreator
    {
        public static TopicReply Create(Topic topic, User user)
        {
            return new TopicReply()
            {
                Toplic = topic,
                TopicId = topic.Id,
                User = user,
                UserId = user.Id,
                CreatedOn = DateTime.Now,
            };
        }
    }
}
