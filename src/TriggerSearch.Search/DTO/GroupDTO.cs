using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models;

namespace TriggerSearch.Search.DTO
{
    public class GroupDTO
    {
        public Guid ID { get; set; }
        public string Title { get; set; }

        public GroupDTO FromEntity(Group group)
        {
            return new GroupDTO()
            {
                ID = group.ID,
                Title = group.Title
            };
        }


        public static implicit operator GroupDTO(Group group)
        {
            return new GroupDTO()
            {
                ID = group.ID,
                Title = group.Title
            };
        }
    }
}
