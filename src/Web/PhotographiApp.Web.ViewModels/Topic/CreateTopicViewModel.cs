namespace PhotographiApp.Web.ViewModels.Topic
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTopicViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
