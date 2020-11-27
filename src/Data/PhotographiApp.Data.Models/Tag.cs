namespace PhotographiApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using PhotographiApp.Data.Common.Models;

    public class Tag : BaseDeletableModel<string>
    {
        public Tag()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public virtual ICollection<PhotoTag> Photos { get; set; }
    }
}
