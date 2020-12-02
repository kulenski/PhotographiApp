namespace PhotographiApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class AlbumController : BaseController
    {
        public IActionResult Show(string id)
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            return null;
        }

        [HttpPost]
        public IActionResult Edit([FromBody] object model)
        {
            return null;
        }
    }
}
