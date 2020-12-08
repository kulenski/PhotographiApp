namespace PhotographiApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PhotographiApp.Data.Common.Repositories;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Interfaces;

    public class LicenseService : ILicenseService
    {
        private readonly IDeletableEntityRepository<License> licensesRepository;

        public LicenseService(IDeletableEntityRepository<License> licensesRepository)
        {
            this.licensesRepository = licensesRepository;
        }

        public ICollection<KeyValuePair<string, string>> GetAll()
        {
            return this.licensesRepository
                .AllAsNoTracking()
                .Select(l => new KeyValuePair<string, string>(l.Id, l.Name))
                .ToList();
        }
    }
}
