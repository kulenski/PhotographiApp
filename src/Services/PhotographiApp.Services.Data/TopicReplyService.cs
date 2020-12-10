namespace PhotographiApp.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PhotographiApp.Data.Common.Repositories;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Interfaces;

    public class TopicReplyService : ITopicReplyService
    {
        private readonly IRepository<TopicReply> topicReplyRepository;
        private readonly IDeletableEntityRepository<Topic> topicRepository;

        public TopicReplyService(
            IRepository<TopicReply> topicReplyRepository,
            IDeletableEntityRepository<Topic> topicRepository)
        {
            this.topicReplyRepository = topicReplyRepository;
            this.topicRepository = topicRepository;
        }

        public async Task AddAsync(string topicId, string userId, string value)
        {
            var topic = this.topicRepository.AllAsNoTracking().Where(x => x.Id == topicId).FirstOrDefault();
            if (topic == null)
            {
                throw new Exception("Topic does not exist!");
            }

            var entity = new TopicReply()
            {
                TopicId = topicId,
                Value = value,
                UserId = userId,
            };

            await this.topicReplyRepository.AddAsync(entity);
            await this.topicReplyRepository.SaveChangesAsync();
        }
    }
}
