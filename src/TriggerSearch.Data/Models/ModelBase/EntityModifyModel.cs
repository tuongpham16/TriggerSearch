using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models.Contracts;

namespace TriggerSearch.Data.Models.ModelBase
{
    public class EntityModifyModel : EntityModel, IEntityModel
    {
        public Guid? ModifierID { get; set; }
        public DateTime? Modified { get; set; }
    }
}
