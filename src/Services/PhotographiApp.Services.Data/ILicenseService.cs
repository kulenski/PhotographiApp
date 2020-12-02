namespace PhotographiApp.Services.Data
{
    using System.Collections.Generic;

    public interface ILicenseService
    {
        ICollection<KeyValuePair<string, string>> GetAll();
    }
}
