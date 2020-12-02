namespace PhotographiApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PhotographiApp.Web.ViewModels.Photos;

    [Authorize]
    public class PhotoController : BaseController
    {
        [HttpGet]
        public IActionResult Upload()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Upload([FromBody] UploadPhotoInputModel model)
        {
            return null;
        }

        [HttpGet]
        public IActionResult Show()
        {
            return null;
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Edit([FromBody]object model)
        {
            return null;
        }
    }
}
