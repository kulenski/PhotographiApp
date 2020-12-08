namespace PhotographiApp.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task AddAsync(string photoId, string userId, string comment);
    }
}
