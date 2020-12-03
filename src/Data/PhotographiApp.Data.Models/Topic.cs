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
            this.Replies = new HashSet<TopicReply>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<TopicReply> Replies { get; set; }
    }
}
