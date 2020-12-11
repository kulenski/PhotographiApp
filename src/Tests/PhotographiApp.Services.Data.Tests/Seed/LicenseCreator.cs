namespace PhotographiApp.Services.Data.Tests.Seed
{
    using System;

    using PhotographiApp.Data.Models;

    public class LicenseCreator
    {
        public static License Create()
        {
            return new License()
            {
                CreatedOn = DateTime.Now,
            };
        }
    }
}
