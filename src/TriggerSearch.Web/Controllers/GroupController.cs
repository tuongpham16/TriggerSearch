using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TriggerSearch.Contract.Services;
using TriggerSearch.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace TriggerSearch.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Group")]
    public class GroupController : Controller
    {
        private IGroupService _groupService;
        private IUserService _userService;
        public GroupController(IGroupService groupService, IUserService userService)
        {
            _groupService = groupService;
            _userService = userService;
        }

        public async Task<IActionResult> Add()
        {

            var currentGroup = _groupService
                .All().Include(item => item.GroupUsers)
                .Include(item => item.GroupRoles).First();
            currentGroup.Title = "admin";
            currentGroup.IsDefault = false;
            currentGroup.IsDeleted = false;
            currentGroup.GroupRoles.Add(new GroupRole()
            {
                RoleID = currentGroup.GroupRoles.ToList()[0].RoleID
            });
            await _groupService.Update(currentGroup,"Title");
            //await _groupService.Add(new Group()
            //    {
            //        Title = "Administrators 2",
            //        GroupUsers = _userService.All().Select(item => new GroupUser()
            //        {
            //            UserID = item.ID
            //        }).ToList(),
            //    });
            return null;
        }
    }
}