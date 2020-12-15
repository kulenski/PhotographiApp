namespace PhotographiApp.Web.Areas.Administration.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using PhotographiApp.Services.Data.Interfaces;
    using PhotographiApp.Web.ViewModels.Administration.Licenses;

    public class LicensesController : AdministrationController
    {
        private readonly ILicenseService licenseService;

        public LicensesController(ILicenseService licenseService)
        {
            this.licenseService = licenseService;
        }

        public IActionResult Index()
        {
            var model = this.licenseService.GetAll<LicenseViewModel>();
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View(new CreateLicenseViewModel() { });
        }

        [HttpPost]
        public IActionResult Create(CreateLicenseViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                this.licenseService.CreateAsync(model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Licenses");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = this.licenseService.GetById<EditLicenseViewModel>(id);
            if (model == null)
            {
                this.ViewData["Error"] = "Such license does not exist!";
                return this.View("ValidationError");
            }

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditLicenseViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                this.licenseService.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Licenses");
        }

        public IActionResult Delete(string id)
        {
            try
            {
                this.licenseService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                this.ViewData["Error"] = ex.Message;
                return this.View("ValidationError");
            }

            return this.RedirectToAction("Index", "Licenses");
        }
    }
}
