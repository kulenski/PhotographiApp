namespace PhotographiApp.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PhotographiApp.Data.Models;

    public class LicensesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Licenses.Any())
            {
                return;
            }

            await dbContext.Licenses.AddAsync(new License { Name = "Attribution", Description = "Attribution" });
            await dbContext.Licenses.AddAsync(new License { Name = "Attribution-ShareAlike", Description = "Attribution-ShareAlike" });
            await dbContext.Licenses.AddAsync(new License { Name = "Attribution-NoDerivs", Description = "Attribution-NoDerivs" });
            await dbContext.Licenses.AddAsync(new License { Name = "Attribution-NonCommercial", Description = "Attribution-NonCommercial" });
            await dbContext.Licenses.AddAsync(new License { Name = "Attribution-NonCommercial-ShareAlike", Description = "Attribution-NonCommercial-ShareAlike" });
            await dbContext.Licenses.AddAsync(new License { Name = "Attribution-NonCommercial-NoDerivs", Description = "Attribution-NonCommercial-NoDerivs" });
        }
    }
}
