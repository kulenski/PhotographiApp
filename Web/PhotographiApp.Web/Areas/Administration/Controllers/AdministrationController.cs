namespace PhotographiApp.Web.Areas.Administration.Controllers
{
    using PhotographiApp.Common;
    using PhotographiApp.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
