namespace PhotographiApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Data.Common.Models;
    using PhotographiApp.Data.Models.Application;

    public class Group : BaseDeletableModel<string>, IAuditInfo
    {
        public Group()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }

        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<GroupMembership> Memberships { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}
