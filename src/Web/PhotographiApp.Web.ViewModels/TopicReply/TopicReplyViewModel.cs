namespace PhotographiApp.Web.ViewModels.TopicReply
{
    using System;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;

    public class TopicReplyViewModel : IMapFrom<TopicReply>
    {
        public string Id { get; set; }

        public string Value { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
