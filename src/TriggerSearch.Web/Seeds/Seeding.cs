using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriggerSearch.Data;

namespace TriggerSearch.Web.Seeds
{
    public class Seeding
    {
        private PermissionContext _context;
        public Seeding(PermissionContext context)
        {
            _context = context;
        }

        public void Init()
        {
            TypeObjectSeeds typeObjectSeeds = new TypeObjectSeeds(_context);
            typeObjectSeeds.Seeding();

            RoleSeeds roleSeeds = new RoleSeeds(_context);
            roleSeeds.Seeding();

            UserSeeds userSeeds = new UserSeeds(_context);
            userSeeds.Seeding();

            GroupSeeds groupSeeds = new GroupSeeds(_context);
            groupSeeds.Seeding();
        }

    }
}
