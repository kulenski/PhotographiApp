namespace PhotographiApp.Data.Models
{
    using System;

    using PhotographiApp.Data.Common.Models;

    public class License : BaseDeletableModel<string>
    {
        public License()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
