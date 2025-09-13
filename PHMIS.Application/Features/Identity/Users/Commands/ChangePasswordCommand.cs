using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.Identity.IServices;
using PHMIS.Application.Identity.Models;

namespace PHMIS.Application.Features.Identity.Users.Commands
{
    public record ChangePasswordCommand(int UserId, ChangePasswordDto Dto) : IRequest<Result<Unit>>;

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<Unit>>
    {
        private readonly IUserService _userService;
        public ChangePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public Task<Result<Unit>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return _userService.ChangePasswordAsync(request.UserId, request.Dto);
        }
    }
}
