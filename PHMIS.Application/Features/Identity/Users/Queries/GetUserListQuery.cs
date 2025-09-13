using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.Identity.IServices;
using PHMIS.Application.Identity.Models;
using System.Collections.Generic;

namespace PHMIS.Application.Features.Identity.Users.Queries
{
    public record GetUserListQuery() : IRequest<Result<List<UserDto>>>;

    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, Result<List<UserDto>>>
    {
        private readonly IUserService _userService;
        public GetUserListQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public Task<Result<List<UserDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return _userService.GetListAsync();
        }
    }
}
