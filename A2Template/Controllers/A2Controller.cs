//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using A2Template.Models;
using A2Template.Data;
using A2Template.Dtos;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Globalization;

namespace A2Template.Controllers
{
    [Route("webapi")]
    [ApiController]
    public class A2Controller : Controller
    {
        private readonly IA2Repo _repository;

        public A2Controller(IA2Repo repository)
        {
            _repository = repository;
        }

        [HttpPost("Register")]
        public ActionResult<string> Register(User user)
        {
            if (!_repository.UserExists(user.UserName))
            {
                _repository.Register(user);
                return Ok("User Successfully registered");
            } 
            else
            {
                return Ok($"UserName {user.UserName} is not available");
            }
           
        }

        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("PurchaseSign/{id}")]
        public ActionResult PurchaseSign(string id)
        {
            Sign sign = _repository.GetSign(id);
            if (sign != null)
            {
                PurchaseOutput signOutput = new PurchaseOutput {UserName = User.FindFirst(ClaimTypes.Name)?.Value, SignID = sign.Id};
                return Ok(signOutput);
            } 
            else
            {
                return BadRequest($"Sign {id} not found");
            }
        }

        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "OrganizerOnly")]
        [HttpPost("AddEvent")]
        public ActionResult AddEvent(EventInput inputEvent)
        {
            if (!DateTime.TryParseExact(
                inputEvent.Start,
                "yyyyMMdd'T'HHmmss'Z'",
                null,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out _)
                &&
                !DateTime.TryParseExact(
                inputEvent.End,
                "yyyyMMdd'T'HHmmss'Z'",
                null,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out _)
                )
            {
                return BadRequest("The format of Start and End should be yyyyMMddTHHmmssZ.");
            }
            else if (!DateTime.TryParseExact(
                inputEvent.Start,
                "yyyyMMdd'T'HHmmss'Z'",
                null,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out _))
            {
                return BadRequest("The format of Start should be yyyyMMddTHHmmssZ.");
            }
            else if (!DateTime.TryParseExact(
                inputEvent.End,
                "yyyyMMdd'T'HHmmss'Z'",
                null,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out _))
            {
                return BadRequest("The format of End should be yyyyMMddTHHmmssZ.");
            }
            else
            {
                Event userInputEvent = new Event {
                    Start = inputEvent.Start, 
                    End = inputEvent.End, 
                    Summary = inputEvent.Summary,
                    Description = inputEvent.Description,
                    Location = inputEvent.Location};
                _repository.AddEvent(userInputEvent);
                return Ok("Success");
            }
        }

        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "UserOrOrganizer")]
        [HttpGet("EventCount")]
        public ActionResult EventCount()
        {
            return Ok(_repository.GetEventCount());
        }

        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "UserOrOrganizer")]
        [HttpGet("Event/{id}")]
        public ActionResult Event(int id)
        {
            Event retrivedEvent = _repository.GetEvent(id);
            if (retrivedEvent != null)
            {
                // do stuff
                Response.Headers.Add("Content-Type", "text/calendar");
                return Ok(retrivedEvent);
            } 
            else
            {
                return BadRequest($"Event {id} does not exist");
            }
        }
    }
}
