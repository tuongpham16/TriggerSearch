using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models.Contracts;
using TriggerSearch.Data.Models.ModelBase;

namespace TriggerSearch.Data.Models
{
    public class GroupUser : EntityModel, IEntityModel
    {
        public Guid GroupID { get; set; }
        public Guid UserID { get; set; }
    }
}
