using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.Identity.IServices;
using PHMIS.Application.Identity.Models;

namespace PHMIS.Application.Features.Identity.Users.Commands
{
    public record CreateUserCommand(UserCreateDto Dto) : IRequest<Result<int>>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
    {
        private readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return _userService.CreateUserAsync(request.Dto);
        }
    }
}
