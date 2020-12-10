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

    public class TopicService : ITopicService
    {
        private readonly IDeletableEntityRepository<Topic> topicRepository;

        public TopicService(
            IDeletableEntityRepository<Topic> topicRepository)
        {
            this.topicRepository = topicRepository;
        }

        public async Task CreateAsync(string userId, string title, string content)
        {
            var entity = new Topic()
            {
                OwnerId = userId,
                Title = title,
                Content = content,
            };

            await this.topicRepository.AddAsync(entity);
            await this.topicRepository.SaveChangesAsync();
        }

        public ICollection<T> GetAllByUser<T>(string userId)
        {
            return this.topicRepository.AllAsNoTracking().Where(x => x.OwnerId == userId).To<T>().ToList();
        }

        public T GetById<T>(string topicId)
        {
            return this.topicRepository.AllAsNoTracking().Where(x => x.Id == topicId).To<T>().FirstOrDefault();
        }

        public ICollection<T> GetLatest<T>()
        {
            return this.topicRepository.AllAsNoTracking().OrderByDescending(x => x.CreatedOn).To<T>().ToList();
        }

        public async Task UpdateAsync(string topicId, string userId, string title, string content)
        {
            var topic = this.topicRepository.All().Where(x => x.Id == topicId && x.OwnerId == userId).FirstOrDefault();
            if (topic == null)
            {
                throw new Exception("Topic does not exist!");
            }

            topic.Title = title;
            topic.Content = content;

            await this.topicRepository.SaveChangesAsync();
        }
    }
}
