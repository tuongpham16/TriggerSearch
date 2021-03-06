﻿using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models.Contracts;
using TriggerSearch.Data.Models.ModelBase;

namespace TriggerSearch.Data.Models
{
    public class UserRole : EntityModel, IEntityModel
    {
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
    }
}
