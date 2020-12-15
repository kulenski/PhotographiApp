namespace PhotographiApp.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PhotographiApp.Web.ViewModels.Administration.Licenses;

    public interface ILicenseService
    {
        ICollection<KeyValuePair<string, string>> GetAll();

        ICollection<T> GetAll<T>();

        T GetById<T>(string id);

        Task CreateAsync(CreateLicenseViewModel model);

        Task UpdateAsync(EditLicenseViewModel model);

        Task DeleteAsync(string id);
    }
}
