namespace PhotographiApp.Data.Models
{
    using PhotographiApp.Data.Common.Models;

    public class PhotoTag : BaseModel<int>
    {
        public string PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

        public string TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
