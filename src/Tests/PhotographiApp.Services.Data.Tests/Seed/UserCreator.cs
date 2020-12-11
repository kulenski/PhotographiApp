namespace PhotographiApp.Services.Data.Tests.Seed
{
    using System;

    using PhotographiApp.Data.Models.Application;

    public class UserCreator
    {
        public static User Create(string username)
        {
            return new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = username,
                NormalizedUserName = username,
            };
        }
    }
}
