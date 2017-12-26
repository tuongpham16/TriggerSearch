using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models.Contracts;
using TriggerSearch.Data.Models.ModelBase;

namespace TriggerSearch.Data.Models
{
    public class GroupRole : EntityModel, IEntityModel
    {
        public Guid GroupID { get; set; }
        public Guid RoleID { get; set; }
        public Role Role { get; set; }
    }
}
