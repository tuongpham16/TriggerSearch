using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models.Contracts;
using TriggerSearch.Data.Models.ModelBase;

namespace TriggerSearch.Data.Models
{
    public class User : EntityModifyModel, IEntityModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarURL { get; set; }
        public bool? IsLock { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsDeleted { get; set; }

        public ICollection<GroupUser> GroupUsers { get; set; }
        public ICollection<ObjectShareUser> ObjectShareUsers { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

    }
}
