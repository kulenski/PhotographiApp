namespace PhotographiApp.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PhotographiApp.Data.Common.Repositories;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Data.Interfaces;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Photo> photoRepository;
        private readonly IRepository<Comment> commentRepository;

        public CommentService(
            IDeletableEntityRepository<Photo> photoRepository,
            IRepository<Comment> commentRepository)
        {
            this.photoRepository = photoRepository;
            this.commentRepository = commentRepository;
        }

        public async Task AddAsync(string photoId, string userId, string comment)
        {
            var photo = this.photoRepository.AllAsNoTracking().Where(x => x.Id == photoId).FirstOrDefault();
            if (photo == null)
            {
                throw new Exception("Photo does not exists!");
            }

            if (photo.IsCommentAllowed == false)
            {
                throw new Exception("Comments are not allowed for photo!");
            }

            var entity = new Comment()
            {
                PhotoId = photoId,
                UserId = userId,
                Value = comment,
            };

            await this.commentRepository.AddAsync(entity);
            await this.commentRepository.SaveChangesAsync();
        }
    }
}
