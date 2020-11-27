namespace PhotographiApp.Data.Models
{
    using System;

    using PhotographiApp.Data.Common.Models;

    public class PhotoMetadata : BaseModel<int>
    {
        public string Camera { get; set; }

        public string Lens { get; set; }

        public string ExposureTime { get; set; }

        public string Aperture { get; set; }

        public string Iso { get; set; }

        public string Flash { get; set; }

        public DateTime? DateTaken { get; set; }
    }
}
