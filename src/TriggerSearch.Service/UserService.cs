using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Contract.Services;
using TriggerSearch.Core;
using TriggerSearch.Data;
using TriggerSearch.Data.Models;

namespace TriggerSearch.Service
{
    public class UserService : BaseService<User>,  IUserService
    {
        public UserService(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }
    }
}
