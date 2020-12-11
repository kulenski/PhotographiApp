namespace PhotographiApp.Services.Data.Tests.Seed
{
    using System;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Data.Models.Application;

    public class PhotoFavoriteCreator
    {
        public static PhotoFavorite Create(Photo photo, User user)
        {
            return new PhotoFavorite()
            {
                Id = new Random().Next(1, 1000),
                Photo = photo,
                User = user,
                PhotoId = photo.Id,
                UserId = user.Id,
                CreatedOn = DateTime.Now,
            };
        }
    }
}
