namespace PhotographiApp.Web.ViewModels.Administration.Licenses
{
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;

    public class LicenseViewModel : IMapFrom<License>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
