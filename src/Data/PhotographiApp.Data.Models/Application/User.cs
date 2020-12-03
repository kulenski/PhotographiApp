// ReSharper disable VirtualMemberCallInConstructor
namespace PhotographiApp.Data.Models.Application
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using PhotographiApp.Data.Common.Models;

    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        // Application specific navigation properties
        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<PhotoFavorite> FavoritePhotos { get; set; }
    }
}
