namespace PhotographiApp.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITopicService
    {
        Task CreateAsync(string userId, string title, string content);

        Task UpdateAsync(string topicId, string userId, string title, string content);

        T GetById<T>(string topicId);

        ICollection<T> GetAllByUser<T>(string userId);

        ICollection<T> GetLatest<T>();
    }
}
