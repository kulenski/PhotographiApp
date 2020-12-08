namespace PhotographiApp.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentInputModel
    {
        [Required]
        public string PhotoId { get; set; }

        [Required]
        [MinLength(5)]
        public string Value { get; set; }
    }
}
