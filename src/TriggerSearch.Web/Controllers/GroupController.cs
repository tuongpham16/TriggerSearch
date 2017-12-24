using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TriggerSearch.Contract.Services;
using TriggerSearch.Data.Models;
using Microsoft.EntityFrameworkCore;
using TriggerSearch.Search;

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

            //var currentGroup = _groupService
            //    .All().Where(item => item.ID == Guid.Parse("bddfd36e-9298-4d7c-9c72-f45a857bd465")).Include(item => item.GroupUsers)
            //    .First();
            //currentGroup.Title = "admin 23";
            //currentGroup.IsDefault = false;
            //currentGroup.IsDeleted = false;



            Group currentGroup = new Group()
            {
                Title = "Administrators 2",
                GroupUsers = _userService.All().Select(item => new GroupUser()
                {
                    UserID = item.ID
                }).ToList(),
            };
            await _groupService.Add(currentGroup);

            //await _userService.Add(newUser);
            await _groupService.Update(currentGroup);
            //await _groupService.Delete(currentGroup);
            return Json(currentGroup);
        }
    }
}