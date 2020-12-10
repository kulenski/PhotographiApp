namespace PhotographiApp.Web.ViewModels.Topic
{
    using System.ComponentModel.DataAnnotations;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;

    public class EditTopicViewModel : IMapFrom<Topic>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string OwnerId { get; set; }
    }
}
