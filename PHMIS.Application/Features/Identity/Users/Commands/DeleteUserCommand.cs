using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.Identity.IServices;

namespace PHMIS.Application.Features.Identity.Users.Commands
{
    public record DeleteUserCommand(int UserId) : IRequest<Result<Unit>>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Unit>>
    {
        private readonly IUserService _userService;
        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public Task<Result<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return _userService.DeleteUserAsync(request.UserId);
        }
    }
}
