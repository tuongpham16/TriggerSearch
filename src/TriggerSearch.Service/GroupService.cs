using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Contract.Services;
using TriggerSearch.Core;
using TriggerSearch.Data;
using TriggerSearch.Data.Models;
using TriggerSearch.Search;

namespace TriggerSearch.Service
{
    public class GroupService : BaseService<Group>,  IGroupService
    {
        public GroupService(IUnitOfWork unitOfWork):base(unitOfWork)
        {
           
        }
        
    }
}
