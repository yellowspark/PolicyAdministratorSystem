using AuthorizationMicroservice.DTO;
using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Interface;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthorizationController : ControllerBase
    {
        private IMemberService _service;

        public AuthorizationController(IMemberService service)
        {
            _service = service;
        }


        [HttpPost("Login")]
        public ActionResult Login([FromBody] MemberLoginDTO member)
        {
            if (member == null)
            {
                return BadRequest("Member details cannot be empty");
            }
            else
            {
                AgentDetails AuthenticatedUser = _service.AuthenticateUser(member);
                if (AuthenticatedUser != null)
                {
                    TokenUserDTO token = _service.CreateJwt(AuthenticatedUser);
                    return Ok(token);
                }
                else
                    return NotFound("Invalid Credentials!!!");
            }
        }
    }
}
