namespace PhotographiApp.Web.ViewModels.Photos
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;
    using PhotographiApp.Web.ViewModels.Comments;

    public class PhotoViewModel : IMapFrom<Photo>, IHaveCustomMappings
    {
        public PhotoViewModel()
        {
            this.IsOwnerByCurrentUser = false;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Href { get; set; }

        public string ThumbnailHref { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsCommentAllowed { get; set; }

        public string Camera { get; set; }

        public string Lens { get; set; }

        public string ExposureTime { get; set; }

        public string Aperture { get; set; }

        public string Iso { get; set; }

        public string Flash { get; set; }

        public DateTime? DateTaken { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OwnerId { get; set; }

        public int FavoritesCount { get; set; }

        public bool IsOwnerByCurrentUser { get; set; }

        public IEnumerable<PhotoAlbumViewModel> PhotoAlbums { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Photo, PhotoViewModel>()
                .ForMember(x => x.FavoritesCount, opt =>
                    opt.MapFrom(x => x.Favorites.Count));
        }
    }
}
