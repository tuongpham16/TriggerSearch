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
        private ISearchService _searchService;
        public GroupController(IGroupService groupService, IUserService userService, ISearchService searchService)
        {
            _groupService = groupService;
            _userService = userService;
            _searchService = searchService;
        }

        public async Task<IActionResult> Add()
        {

            var currentGroup = _groupService
                .All().Where(item => item.ID == Guid.Parse("6e6f56d5-6cfc-41ae-b3ab-6ead1ddddf28")).Include(item => item.GroupUsers)
                .First();
            currentGroup.Title = "admin 2345";
            currentGroup.IsDefault = false;
            currentGroup.IsDeleted = false;

            await _groupService.Update(currentGroup, "Title");

            //Group newGroup = new Group()
            //{
            //    Title = "Administrators 2",
            //    GroupUsers = _userService.All().Select(item => new GroupUser()
            //    {
            //        UserID = item.ID
            //    }).ToList(),
            //};
            //await _groupService.Add(newGroup);
            //await _searchService.IndexAsync(newGroup);
            return null;
        }
    }
}