namespace PhotographiApp.Web.ViewModels.Administration.Licenses
{
    using System.ComponentModel.DataAnnotations;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;

    public class EditLicenseViewModel : IMapFrom<License>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
