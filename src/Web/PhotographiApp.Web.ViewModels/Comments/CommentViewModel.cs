﻿namespace PhotographiApp.Web.ViewModels.Comments
{
    using System;

    using PhotographiApp.Data.Models;
    using PhotographiApp.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public string UserId { get; set; }

        public string Value { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
