namespace PhotographiApp.Data.Models
{
    using System;

    using PhotographiApp.Data.Common.Models;
    using PhotographiApp.Data.Models.Application;

    public class TopicReply : BaseModel<string>, IAuditInfo
    {
        public TopicReply()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Value { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string TopicId { get; set; }

        public virtual Topic Toplic { get; set; }
    }
}
