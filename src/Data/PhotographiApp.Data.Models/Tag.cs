namespace PhotographiApp.Data.Models
{
    using PhotographiApp.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
