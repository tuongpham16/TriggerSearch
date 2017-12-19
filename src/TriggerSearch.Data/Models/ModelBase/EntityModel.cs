using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TriggerSearch.Data.Models.Contracts;

namespace TriggerSearch.Data.Models.ModelBase
{
    public abstract class EntityModel : IEntityModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        public Guid? CreaterID { get; set; }
        public DateTime Created { get; set; }
    }
}
