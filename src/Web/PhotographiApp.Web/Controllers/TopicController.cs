namespace PhotographiApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class TopicController : BaseController
    {
        public IActionResult Show(string id)
        {
            return this.View();
        }
    }
}
