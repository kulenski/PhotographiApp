namespace PhotographiApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class TopicReplyController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
