using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriggerSearch.Data;
using TriggerSearch.Data.Models;

namespace TriggerSearch.Web.Seeds
{
    public class UserSeeds : BaseSeeding, ISeeding
    {
        public UserSeeds(PermissionContext context):base(context)
        {
        }
        public override void Seeding()
        {
            if(!_context.Users.Any())
            {
                _context.Add(new User()
                {
                    FirstName = "Administrator",
                    Email = "admin@demo.com",
                    UserName = "administrator",
                    UserRoles = _context.Roles.ToList().Select(item => new UserRole()
                    {
                        RoleID = item.ID
                    }).ToList()

                });
                _context.SaveChanges();
            }

        }
    }
}
