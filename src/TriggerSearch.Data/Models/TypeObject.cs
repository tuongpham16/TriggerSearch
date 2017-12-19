using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models.Contracts;
using TriggerSearch.Data.Models.ModelBase;

namespace TriggerSearch.Data.Models
{
    public class TypeObject: EntityModel, IEntityModel
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public ICollection<Permit> Permits{ get; set; }
        public ICollection<ObjectShareUser> ObjectShareUsers { get; set; }
    }
}
