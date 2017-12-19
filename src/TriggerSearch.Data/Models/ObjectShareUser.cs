using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models.Contracts;
using TriggerSearch.Data.Models.ModelBase;

namespace TriggerSearch.Data.Models
{
     public class ObjectShareUser: EntityModel, IEntityModel
    {
        public Guid TypeObjectID { set; get; }
        public Guid UserID { get; set; }
        public Guid PermitID { get; set; }
    }
}
