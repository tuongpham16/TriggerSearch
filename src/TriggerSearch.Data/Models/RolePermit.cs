using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models.Contracts;
using TriggerSearch.Data.Models.ModelBase;

namespace TriggerSearch.Data.Models
{
    public class RolePermit: EntityModel, IEntityModel
    {
        public Guid RoleID { get; set; }
        public Guid PermitID { get; set; }
    }
}
