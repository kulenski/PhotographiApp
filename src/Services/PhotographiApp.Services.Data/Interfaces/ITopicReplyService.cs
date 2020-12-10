namespace PhotographiApp.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface ITopicReplyService
    {
        Task AddAsync(string topicId, string userId, string value);
    }
}
