using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriggerSearch.Data;
using TriggerSearch.Data.Models;

namespace TriggerSearch.Web.Seeds
{
    public class GroupSeeds : BaseSeeding, ISeeding
    {
        
        public GroupSeeds(PermissionContext context):base(context)
        {
        }

        public override void Seeding()
        {
            if(!_context.Groups.Any())
            {
                _context.Add(new Group()
                {
                    Title = "Administrators",
                    GroupUsers = _context.Users.Select(item => new GroupUser()
                    {
                        UserID = item.ID
                    }).ToList(),
                    GroupRoles = _context.Roles.Select(item => new GroupRole()
                    {
                        RoleID = item.ID
                    }).ToList()
                });

                _context.SaveChanges();
            }
        }

    }
}
