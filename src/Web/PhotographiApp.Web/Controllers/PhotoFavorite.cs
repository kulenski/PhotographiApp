namespace PhotographiApp.Web.Controllers
{
    using System;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PhotographiApp.Data.Models.Application;
    using PhotographiApp.Services.Data.Interfaces;

    [Authorize]
    public class PhotoFavorite : BaseController
    {
        private readonly IFavoritesService favoritesService;
        private readonly UserManager<User> userManager;

        public PhotoFavorite(
            IFavoritesService favoritesService,
            UserManager<User> userManager)
        {
            this.favoritesService = favoritesService;
            this.userManager = userManager;
        }

        public IActionResult Make(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            try
            {
                this.favoritesService.ToggleAsync(id, userId);
            }
            catch (Exception ex)
            {
                this.ViewData["Error"] = ex.Message;
                return this.View("ValidationError");
            }

            return this.RedirectToAction("Show", "Photo", new { Id = id });
        }
    }
}
