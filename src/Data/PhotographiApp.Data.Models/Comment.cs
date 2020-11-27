namespace PhotographiApp.Data.Models
{
    using System;

    using PhotographiApp.Data.Common.Models;
    using PhotographiApp.Data.Models.Application;

    public class Comment : BaseModel<string>, IAuditInfo
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Value { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string PhotoId { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
