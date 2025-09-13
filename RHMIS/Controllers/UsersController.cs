using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PHMIS.Application.Common.Response;
using PHMIS.Application.Features.Identity.Users.Commands;
using PHMIS.Application.Features.Identity.Users.Queries;
using PHMIS.Application.Identity.Models;
using PHMIS.Controllers.Base;

namespace PHMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<int>> Create(UserCreateDto dto)
        {
            return HandleResultResponse<int>(await Mediator.Send(new CreateUserCommand(dto)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update(int id, UserUpdateDto dto)
        {
            return HandleResultResponse<UserDto>(await Mediator.Send(new UpdateUserCommand(id, dto)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return HandleResultResponse<Unit>(await Mediator.Send(new DeleteUserCommand(id)));
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            return HandleResultResponse<List<UserDto>>(await Mediator.Send(new GetUserListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            return HandleResultResponse<UserDto>(await Mediator.Send(new GetUserByIdQuery(id)));
        }

        [HttpPost("{id}/change-password")]
        public async Task<ActionResult<Unit>> ChangePassword(int id, ChangePasswordDto dto)
        {
            return HandleResultResponse<Unit>(await Mediator.Send(new ChangePasswordCommand(id, dto)));
        }

        [HttpPost("{id}/roles")]
        public async Task<ActionResult<Unit>> AssignRoles(int id, [FromBody] IEnumerable<string> roles)
        {
            return HandleResultResponse<Unit>(await Mediator.Send(new AssignRolesCommand(id, roles)));
        }
    }
}
