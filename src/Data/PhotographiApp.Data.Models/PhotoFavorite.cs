namespace PhotographiApp.Data.Models
{
    using PhotographiApp.Data.Common.Models;
    using PhotographiApp.Data.Models.Application;

    public class PhotoFavorite : BaseModel<int>, IAuditInfo
    {
        public string PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
