using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.Identity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PHMIS.Application.Identity.IServices
{
    public interface IUserService
    {
        Task<Result<int>> CreateUserAsync(UserCreateDto input);
        Task<Result<UserDto>> UpdateUserAsync(int userId, UserUpdateDto input);
        Task<Result<Unit>> DeleteUserAsync(int userId);
        Task<Result<UserDto>> GetByIdAsync(int userId);
        Task<Result<List<UserDto>>> GetListAsync();
        Task<Result<Unit>> ChangePasswordAsync(int userId, ChangePasswordDto input);
        Task<Result<Unit>> AssignRolesAsync(int userId, IEnumerable<string> roles);
    }
}
