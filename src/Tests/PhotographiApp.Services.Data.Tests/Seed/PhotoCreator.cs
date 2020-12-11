namespace PhotographiApp.Services.Data.Tests.Seed
{
    using System;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Data.Models.Application;

    public class PhotoCreator
    {
        public static Photo Create(User owner, bool isPrivate, bool isCommentingAllowed)
        {
            return new Photo()
            {
                IsDeleted = false,
                IsCommentAllowed = isCommentingAllowed,
                CreatedOn = DateTime.Now,
                IsPrivate = isPrivate,
                Owner = owner,
                OwnerId = owner.Id,
            };
        }
    }
}
