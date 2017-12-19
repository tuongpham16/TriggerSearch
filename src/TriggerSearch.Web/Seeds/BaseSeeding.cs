using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriggerSearch.Data;

namespace TriggerSearch.Web.Seeds
{

    public abstract class BaseSeeding : ISeeding
    {
        protected PermissionContext _context;

        public BaseSeeding(PermissionContext context)
        {
            this._context = context;
        }
        public abstract void Seeding();
    }
}
