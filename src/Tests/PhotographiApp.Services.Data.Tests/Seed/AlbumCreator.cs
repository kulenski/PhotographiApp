namespace PhotographiApp.Services.Data.Tests.Seed
{
    using System;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Data.Models.Application;

    public class AlbumCreator
    {
        public static Album Create(bool isPrivate, User owner)
        {
            return new Album()
            {
                IsPrivate = isPrivate,
                IsDeleted = false,
                CreatedOn = DateTime.Now,
                OwnerId = owner.Id,
                Owner = owner,
            };
        }
    }
}
