using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TriggerSearch.Data.Models.Contracts;
using TriggerSearch.Data.Models.ModelBase;

namespace TriggerSearch.Data.Models
{
    public class Group : EntityModifyModel, IEntityModel
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsDeleted { get; set; }

        public ICollection<GroupRole>  GroupRoles{ get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
    }
}
