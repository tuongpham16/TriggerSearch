using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriggerSearch.Data;
using TriggerSearch.Data.Models;

namespace TriggerSearch.Web.Seeds
{
    public class TypeObjectSeeds : BaseSeeding,  ISeeding
    {
        public TypeObjectSeeds(PermissionContext context):base(context)
        { }

        public override void Seeding()
        {
            if(!_context.TypeObjects.Any())
            {
                _context.TypeObjects.Add(new Data.Models.TypeObject()
                {
                    Name = "Source",
                    Title = "Source",
                    Permits = new List<Permit>()
                    {
                        new Permit(){ Name = "Create", Title = "Tạo mới"},
                        new Permit(){ Name = "Update", Title = "Sửa thông tin"},
                        new Permit(){ Name = "Delete", Title = "Xóa"},
                    }

                });

                _context.TypeObjects.Add(new Data.Models.TypeObject()
                {
                    Name = "Collection",
                    Title = "Collection",
                    Permits = new List<Permit>()
                    {
                        new Permit(){ Name = "Create", Title = "Tạo mới"},
                        new Permit(){ Name = "Update", Title = "Sửa thông tin"},
                        new Permit(){ Name = "Delete", Title = "Xóa"},
                        new Permit(){ Name = "Share", Title = "Chia sẻ"},
                    }

                });

                _context.TypeObjects.Add(new Data.Models.TypeObject()
                {
                    Name = "Item",
                    Title = "Item",
                    Permits = new List<Permit>()
                    {
                        new Permit(){ Name = "Create", Title = "Tạo mới"},
                        new Permit(){ Name = "Update", Title = "Sửa thông tin"},
                        new Permit(){ Name = "Delete", Title = "Xóa"},
                        new Permit(){ Name = "Download", Title = "Tải"},
                        new Permit(){ Name = "View", Title = "Xem thông tin"},
                    }
                });

                _context.SaveChanges();
            }
        }
    }
}
