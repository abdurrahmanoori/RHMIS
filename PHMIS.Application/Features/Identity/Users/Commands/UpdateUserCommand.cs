using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.Identity.IServices;
using PHMIS.Application.Identity.Models;

namespace PHMIS.Application.Features.Identity.Users.Commands
{
    public record UpdateUserCommand(int UserId, UserUpdateDto Dto) : IRequest<Result<UserDto>>;

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserDto>>
    {
        private readonly IUserService _userService;
        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return _userService.UpdateUserAsync(request.UserId, request.Dto);
        }
    }
}
