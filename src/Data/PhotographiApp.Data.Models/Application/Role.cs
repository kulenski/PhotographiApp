// ReSharper disable VirtualMemberCallInConstructor
namespace PhotographiApp.Data.Models.Application
{
    using System;

    using Microsoft.AspNetCore.Identity;
    using PhotographiApp.Data.Common.Models;

    public class Role : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public Role()
            : this(null)
        {
        }

        public Role(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
