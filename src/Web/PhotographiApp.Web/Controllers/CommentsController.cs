namespace PhotographiApp.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PhotographiApp.Data.Models.Application;
    using PhotographiApp.Services.Data.Interfaces;
    using PhotographiApp.Web.ViewModels.Comments;
    using PhotographiApp.Web.ViewModels.Photos;

    [Authorize]
    public class CommentsController : BaseController
    {
        private readonly ICommentService commentService;
        private readonly IPhotoService photoService;
        private readonly UserManager<User> userManager;

        public CommentsController(
            ICommentService commentService,
            IPhotoService photoService,
            UserManager<User> userManager)
        {
            this.commentService = commentService;
            this.photoService = photoService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Add(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var photo = this.photoService.GetById<PhotoViewModel>(id, userId);
            if (photo == null || photo.IsCommentAllowed == false)
            {
                this.ViewData["Error"] = "Photo not found or comments not allowed!";
                return this.View("ValidationError");
            }

            var model = new CommentInputModel()
            {
                PhotoId = photo.Id,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.userManager.GetUserId(this.User);

            try
            {
                await this.commentService.AddAsync(model.PhotoId, userId, model.Value);
                return this.RedirectToAction("Show", "Photo", new { Id = model.PhotoId });
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }
        }
    }
}
