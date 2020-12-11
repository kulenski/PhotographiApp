namespace PhotographiApp.Services.Data.Tests.Seed
{
    using System;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Data.Models.Application;

    public class TopicCreator
    {
        public static Topic Create(User user)
        {
            return new Topic()
            {
                Owner = user,
                OwnerId = user.Id,
                CreatedOn = DateTime.Now,
                IsDeleted = false,
            };
        }
    }
}
