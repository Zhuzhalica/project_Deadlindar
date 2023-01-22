using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ValueObjects;
using WebAPI.Server.Services;

namespace WebAPI.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly ILogger<GroupController> logger;
        private readonly IGroupService groupService;

        public GroupController(ILogger<GroupController> logger, IGroupService service)
        {
            this.logger = logger;
            this.groupService = service;
        }

        [HttpGet("GetAll")]
        public IEnumerable<Group> GetAll()
        {
            logger.LogInformation(MyLogEvents.GetItem, "Get all groups");
            return groupService.GetAll();
        }

        [HttpGet("GetNamesByLogin")]
        public IEnumerable<string> GetGroupByName(string login)
        {
            logger.LogInformation(MyLogEvents.GetItem, $"Get by login. Login: {login}");
            return groupService.GetNamesByLogin(login);
        }

        [HttpGet("GetByName")]
        public Group? GetGroupByName(string login, string groupName)
        {
            var group = groupService.GetGroupByName(login, groupName);
            if (group != null)
            {
                logger.LogInformation(MyLogEvents.GetItem, "Get by name");
                return group;
            }

            logger.LogInformation(MyLogEvents.GetItemNotFound,
                $"Get by name failed. Login: {login}, Name: {groupName}");
            return default;
        }

        [HttpPost("Create")]
        public ActionResult<Group> Create(string login, Group _group)
        {
            if (groupService.Create(login, _group))
            {
                logger.LogInformation(MyLogEvents.InsertItem,
                    $"Add new group. Login: {login}, Group: {_group.Name}");
                return Ok();
            }

            logger.LogWarning(MyLogEvents.GetItemNotFound,
                $"Failed attempt create a group. Login: {login}, Group: {_group.Name}");
            return BadRequest();
        }

        [HttpDelete("Delete")]
        public ActionResult<Group> Delete(string login, string groupName)
        {
            if (groupService.Delete(login, groupName))
            {
                logger.LogInformation(MyLogEvents.DeleteItem, $"Group delete");
                return Ok();
            }

            logger.LogWarning(MyLogEvents.GetItemNotFound,
                $"Failed attempt to delete a group. Login: {login}, Group: {groupName}");
            return BadRequest(groupName);
        }

        [HttpPost("AddUser")]
        public ActionResult<Group> AddUser(string memberLogin, string groupName, string newLogin, GroupRole role)
        {
            if (groupService.AddUser(memberLogin, groupName, newLogin, role))
            {
                logger.LogInformation(MyLogEvents.InsertItem,
                    $"Add new user. Adder: {memberLogin}, New login: {newLogin}, Group: {groupName}");
                return Ok();
            }

            logger.LogWarning(MyLogEvents.GetItemNotFound,
                $"Failed attempt add a user. Adder: {memberLogin}, New login: {newLogin}, Group: {groupName}");
            return BadRequest();
        }

        [HttpDelete("RemoveUser")]
        public ActionResult<Group> RemoveUser(string memberLogin, string groupName, string removeLogin)
        {
            if (groupService.RemoveUser(memberLogin, groupName, removeLogin))
            {
                logger.LogInformation(MyLogEvents.DeleteItem,
                    $"Remove user. Remover: {memberLogin}, Remove login: {removeLogin}, Group: {groupName}");
                return Ok();
            }

            logger.LogWarning(MyLogEvents.GetItemNotFound,
                $"Failed attempt to delete a user. Remover: {memberLogin}, Remove login: {removeLogin}, Group: {groupName}");
            return BadRequest(groupName);
        }

        [HttpPost("AddEvent")]
        public ActionResult<Group> AddEvent(string memberLogin, string groupName, Event _event)
        {
            if (groupService.AddEvent(memberLogin, groupName, _event))
            {
                logger.LogInformation(MyLogEvents.InsertItem,
                    $"Add new event. Adder: {memberLogin}, New event: {_event.Title}, Group: {groupName}");
                return Ok();
            }

            logger.LogWarning(MyLogEvents.GetItemNotFound,
                $"Failed attempt add a event. Adder: {memberLogin}, New event: {_event.Title}, Group: {groupName}");
            return BadRequest();
        }

        [HttpPut("ChangeEvent")]
        public ActionResult<Group> ChangeEvent(string memberLogin, string groupName, Event _event)
        {
            if (groupService.ChangeEvent(memberLogin, groupName, _event))
            {
                logger.LogInformation(MyLogEvents.InsertItem,
                    $"Change event. Login: {memberLogin}, Event: {_event.Title}, Group: {groupName}");
                return Ok();
            }

            logger.LogWarning(MyLogEvents.GetItemNotFound,
                $"Failed attempt change event. Login: {memberLogin}, Event: {_event.Title}, Group: {groupName}");
            return BadRequest();
        }

        [HttpDelete("RemoveEvent")]
        public ActionResult<Group> RemoveEvent(string memberLogin, string groupName, Event _event)
        {
            if (groupService.RemoveEvent(memberLogin, groupName, _event))
            {
                logger.LogInformation(MyLogEvents.DeleteItem,
                    $"Remove event. Remover: {memberLogin}, remove event: {_event.Title}, Group: {groupName}");
                return Ok();
            }

            logger.LogWarning(MyLogEvents.GetItemNotFound,
                $"Failed attempt to delete a event. Remover: {memberLogin}, remove event: {_event.Title}, Group: {groupName}");
            return BadRequest(groupName);
        }

        [HttpPut("ChangeRole")]
        public ActionResult<Group> ChangeRole(string memberLogin, string groupName, string changeRoleLogin,
            GroupRole role)
        {
            if (groupService.ChangeRole(memberLogin, groupName, changeRoleLogin, role))
            {
                logger.LogInformation(MyLogEvents.InsertItem,
                    $"Change role. Login: {memberLogin}, Change user {changeRoleLogin}, Group: {groupName}");
                return Ok();
            }

            logger.LogWarning(MyLogEvents.GetItemNotFound,
                $"Failed attempt change role. Login: {memberLogin}, Change user {changeRoleLogin}, Group: {groupName}");
            return BadRequest();
        }
    }
}