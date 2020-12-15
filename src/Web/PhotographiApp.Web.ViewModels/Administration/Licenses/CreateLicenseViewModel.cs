namespace PhotographiApp.Web.ViewModels.Administration.Licenses
{
    using System.ComponentModel.DataAnnotations;

    public class CreateLicenseViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
