namespace PhotographiApp.Data.Models
{
    using PhotographiApp.Data.Common.Models;
    using PhotographiApp.Data.Models.Application;

    public class GroupMembership : BaseModel<int>, IAuditInfo
    {
        public string GroupId { get; set; }

        public virtual Group Group { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public bool IsApproved { get; set; }
    }
}
