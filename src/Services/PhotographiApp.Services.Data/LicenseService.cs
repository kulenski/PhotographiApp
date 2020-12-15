namespace PhotographiApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PhotographiApp.Data.Common.Repositories;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Interfaces;
    using PhotographiApp.Services.Mapping;
    using PhotographiApp.Web.ViewModels.Administration.Licenses;

    public class LicenseService : ILicenseService
    {
        private readonly IDeletableEntityRepository<License> licensesRepository;

        public LicenseService(IDeletableEntityRepository<License> licensesRepository)
        {
            this.licensesRepository = licensesRepository;
        }

        public async Task CreateAsync(CreateLicenseViewModel model)
        {
            var entity = new License() { Name = model.Name, Description = model.Description };
            await this.licensesRepository.AddAsync(entity);
            await this.licensesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = this.licensesRepository.All().Where(x => x.Id == id).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("License does not exist!");
            }

            this.licensesRepository.Delete(entity);
            await this.licensesRepository.SaveChangesAsync();
        }

        public ICollection<KeyValuePair<string, string>> GetAll()
        {
            return this.licensesRepository
                .AllAsNoTracking()
                .Select(l => new KeyValuePair<string, string>(l.Id, l.Name))
                .ToList();
        }

        public ICollection<T> GetAll<T>()
        {
            return this.licensesRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();
        }

        public T GetById<T>(string id)
        {
            return this.licensesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(EditLicenseViewModel model)
        {
            var entity = this.licensesRepository.All().Where(x => x.Id == model.Id).FirstOrDefault();
            if (entity == null)
            {
                throw new Exception("License does not exist!");
            }

            entity.Name = model.Name;
            entity.Description = model.Description;
            await this.licensesRepository.SaveChangesAsync();
        }
    }
}
