namespace PhotographiApp.Services
{
    using System;
    using System.IO;
    using System.Linq;

    using MetadataExtractor;
    using MetadataExtractor.Formats.Exif;
    using Microsoft.AspNetCore.Http;
    using PhotographiApp.Services.Interfaces;
    using PhotographiApp.Services.Models;

    public class PhotoMetadataService : IPhotoMetadataService
    {
        public PhotoMetadata GetMetadata(IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentException("Input file cannot be null!");
            }

            var metadata = new PhotoMetadata();

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var result = ImageMetadataReader.ReadMetadata(ms);

                // obtain the Exif directories
                var exifIdDirectory = result.OfType<ExifIfd0Directory>().FirstOrDefault();
                var exifSubIfDirectory = result.OfType<ExifSubIfdDirectory>().FirstOrDefault();

                if (exifIdDirectory != null)
                {
                    var camera = exifIdDirectory.GetDescription(ExifIfd0Directory.TagMake) ?? "Unknown";
                    var model = exifIdDirectory.GetDescription(ExifIfd0Directory.TagModel) ?? "Unknown";
                    metadata.Camera = $"{camera} - {model}";

                    var takenDate = exifIdDirectory.GetDescription(ExifIfd0Directory.TagDateTime);
                    DateTime dateTime;

                    if (DateTime.TryParse(takenDate, out dateTime))
                    {
                        metadata.DateTaken = dateTime;
                    }
                }

                if (exifSubIfDirectory != null)
                {
                    var subIfdDescriptor = new ExifSubIfdDescriptor(exifSubIfDirectory);
                    metadata.ExposureTime = subIfdDescriptor.GetExposureTimeDescription();
                    metadata.Aperture = subIfdDescriptor.GetApertureValueDescription();
                    metadata.Flash = subIfdDescriptor.GetFlashDescription();
                    metadata.Iso = subIfdDescriptor.GetIsoEquivalentDescription();
                }
            }

            return metadata;
        }
    }
}
