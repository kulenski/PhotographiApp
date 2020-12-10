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
    using PhotographiApp.Web.ViewModels.TopicReply;

    [Authorize]
    public class TopicReplyController : BaseController
    {
        private readonly ITopicReplyService topicReplyService;
        private readonly ITopicService topicService;
        private readonly UserManager<User> userManager;

        public TopicReplyController(
            ITopicReplyService topicReplyService,
            ITopicService topicService,
            UserManager<User> userManager)
        {
            this.topicReplyService = topicReplyService;
            this.topicService = topicService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Add(string id)
        {
            var model = this.topicService.GetById<TopicViewModel>(id);
            if (model == null)
            {
                this.ViewData["Error"] = "Topic does not exist!";
                return this.View("ValidationError");
            }

            return this.View(new AddTopicReplyViewModel() { TopicId = id });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTopicReplyViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.userManager.GetUserId(this.User);

            try
            {
                await this.topicReplyService.AddAsync(model.TopicId, userId, model.Value);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            return this.RedirectToAction("Show", "Topic", new { Id = model.TopicId });
        }
    }
}
