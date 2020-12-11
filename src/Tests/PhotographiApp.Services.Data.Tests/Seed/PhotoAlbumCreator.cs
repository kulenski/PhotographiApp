namespace PhotographiApp.Services.Data.Tests.Seed
{
    using System;

    using PhotographiApp.Data.Models;

    public class PhotoAlbumCreator
    {
        public static PhotoAlbum Create(Photo photo, Album album)
        {
            return new PhotoAlbum()
            {
                Id = new Random().Next(1, 1000),
                Photo = photo,
                PhotoId = photo.Id,
                Album = album,
                AlbumId = album.Id,
                CreatedOn = DateTime.Now,
            };
        }
    }
}
