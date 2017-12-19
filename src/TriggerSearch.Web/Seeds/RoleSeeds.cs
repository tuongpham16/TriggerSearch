using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriggerSearch.Data;
using TriggerSearch.Data.Models;

namespace TriggerSearch.Web.Seeds
{
    public class RoleSeeds : BaseSeeding, ISeeding
    {
        public RoleSeeds(PermissionContext context):base(context)
        {

        }

        public override void Seeding()
        {
            if(!_context.Roles.Any())
            {
                var typeObjects = _context.TypeObjects.ToList();

                _context.Roles.Add(new Data.Models.Role()
                {
                    Title = "Administrator",
                    RolePermits = typeObjects.SelectMany(item => item.Permits).Select(item => new RolePermit()
                    {
                        PermitID = item.ID
                    }).ToList()
                });
                _context.SaveChanges();
            }
        }
    }
}
