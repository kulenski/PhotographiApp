namespace PhotographiApp.Services.Models
{
    using System;

    public class PhotoMetadata
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
