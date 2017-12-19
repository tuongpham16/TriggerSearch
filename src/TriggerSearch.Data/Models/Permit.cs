using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models.Contracts;
using TriggerSearch.Data.Models.ModelBase;

namespace TriggerSearch.Data.Models
{
    public class Permit : EntityModel, IEntityModel
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

        public Guid TypeObjectID { get; set; }

        public ICollection<RolePermit> RolePermits { get; set; }
        public ICollection<ObjectShareUser> ObjectShareUsers { get; set; }

    }
}
