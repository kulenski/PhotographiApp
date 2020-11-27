namespace PhotographiApp.Data.Models
{
    using PhotographiApp.Data.Common.Models;

    public class PhotoTag : BaseModel<long>
    {
        public long PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

        public long TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
