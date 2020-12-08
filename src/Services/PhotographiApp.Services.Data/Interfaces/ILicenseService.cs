namespace PhotographiApp.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ILicenseService
    {
        ICollection<KeyValuePair<string, string>> GetAll();
    }
}
