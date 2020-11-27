namespace PhotographiApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Data.Common.Models;
    using PhotographiApp.Data.Models.Application;

    public class Topic : BaseDeletableModel<string>, IAuditInfo
    {
        public Topic()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string GroupId { get; set; }

        public virtual Group Group { get; set; }

        public string CreatedByUserId { get; set; }

        public virtual User CreatedByUser { get; set; }

        public virtual ICollection<TopicReply> Replies { get; set; }
    }
}
