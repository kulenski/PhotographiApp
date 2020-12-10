namespace PhotographiApp.Web.ViewModels.Topic
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;
    using PhotographiApp.Web.ViewModels.TopicReply;

    public class TopicViewModel : IMapFrom<Topic>
    {
        public TopicViewModel()
        {
            this.IsOwnedByCurrentUser = false;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string OwnerId { get; set; }

        public string OwnerUserName { get; set; }

        public bool IsOwnedByCurrentUser { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public ICollection<TopicReplyViewModel> Replies { get; set; }
    }
}
