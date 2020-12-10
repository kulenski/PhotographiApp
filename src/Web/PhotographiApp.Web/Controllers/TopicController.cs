namespace PhotographiApp.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PhotographiApp.Data.Models.Application;
    using PhotographiApp.Services.Data.Interfaces;
    using PhotographiApp.Web.ViewModels.Topic;

    [Authorize]
    public class TopicController : BaseController
    {
        private readonly ITopicService topicService;
        private readonly UserManager<User> userManager;

        public TopicController(
            ITopicService topicService,
            UserManager<User> userManager)
        {
            this.topicService = topicService;
            this.userManager = userManager;
        }

        public IActionResult Show(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var viewModel = this.topicService.GetById<TopicViewModel>(id);
            if (viewModel == null)
            {
                this.ViewData["Error"] = "Topic does not exist!";
                return this.View("ValidationError");
            }

            if (userId == viewModel.OwnerId)
            {
                viewModel.IsOwnedByCurrentUser = true;
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTopicViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.userManager.GetUserId(this.User);

            try
            {
                await this.topicService.CreateAsync(userId, model.Title, model.Content);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            return this.View("CreateSuccessful");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var model = this.topicService.GetById<EditTopicViewModel>(id);
            if (model == null)
            {
                this.ViewData["Error"] = "Topic not found!";
                return this.View("ValidationError");
            }

            if (model.OwnerId != userId)
            {
                this.ViewData["Error"] = "You cannot edit topics that are owned by other users!";
                return this.View("ValidationError");
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTopicViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                await this.topicService.UpdateAsync(model.Id, user.Id, model.Title, model.Content);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            return this.RedirectToAction("Show", "Topic", new { Id = model.Id });
        }
    }
}
