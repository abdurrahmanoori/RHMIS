using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MediatR;
using PHMIS.Application.Common.Response;
using PHMIS.Application.Identity.IServices;
using PHMIS.Application.Identity.Models;
using PHMIS.Domain.Entities.Identity.Entity;

namespace PHMIS.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result<int>> CreateUserAsync(UserCreateDto input)
        {
            var user = new ApplicationUser
            {
                UserName = input.UserName,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                FirstName = input.FirstName,
                LastName = input.LastName,
                HospitalId = input.HospitalId,
                EmailConfirmed = false
            };

            var createResult = await _userManager.CreateAsync(user, input.Password);
            if (!createResult.Succeeded)
            {
                return Result<int>.WithErrors(createResult.Errors.Select(e => new ValidationError
                {
                    Code = e.Code,
                    Description = e.Description
                }).ToList());
            }

            if (input.Roles != null)
            {
                foreach (var role in input.Roles.Distinct())
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        var roleResult = await _roleManager.CreateAsync(new ApplicationRole { Name = role });
                        if (!roleResult.Succeeded)
                        {
                            return Result<int>.WithErrors(roleResult.Errors.Select(e => new ValidationError
                            {
                                Code = e.Code,
                                Description = e.Description
                            }).ToList());
                        }
                    }
                }
                var addToRoles = await _userManager.AddToRolesAsync(user, input.Roles.Distinct());
                if (!addToRoles.Succeeded)
                {
                    return Result<int>.WithErrors(addToRoles.Errors.Select(e => new ValidationError
                    {
                        Code = e.Code,
                        Description = e.Description
                    }).ToList());
                }
            }

            return Result<int>.SuccessResult(user.Id, "User created successfully");
        }

        public async Task<Result<UserDto>> UpdateUserAsync(int userId, UserUpdateDto input)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return Result<UserDto>.NotFoundResult(userId);
            }

            if (!string.IsNullOrWhiteSpace(input.Email)) user.Email = input.Email;
            if (!string.IsNullOrWhiteSpace(input.PhoneNumber)) user.PhoneNumber = input.PhoneNumber;
            if (!string.IsNullOrWhiteSpace(input.FirstName)) user.FirstName = input.FirstName;
            if (!string.IsNullOrWhiteSpace(input.LastName)) user.LastName = input.LastName;
            if (input.EmailConfirmed.HasValue) user.EmailConfirmed = input.EmailConfirmed.Value;
            if (input.PhoneNumberConfirmed.HasValue) user.PhoneNumberConfirmed = input.PhoneNumberConfirmed.Value;
            if (input.HospitalId.HasValue) user.HospitalId = input.HospitalId.Value;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return Result<UserDto>.WithErrors(updateResult.Errors.Select(e => new ValidationError
                {
                    Code = e.Code,
                    Description = e.Description
                }).ToList());
            }

            if (input.Roles != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                var toRemove = currentRoles.Except(input.Roles).ToArray();
                var toAdd = input.Roles.Except(currentRoles).ToArray();
                if (toRemove.Length > 0)
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, toRemove);
                    if (!removeResult.Succeeded)
                        return Result<UserDto>.WithErrors(removeResult.Errors.Select(e => new ValidationError { Code = e.Code, Description = e.Description }).ToList());
                }
                if (toAdd.Length > 0)
                {
                    foreach (var role in toAdd)
                    {
                        if (!await _roleManager.RoleExistsAsync(role))
                            await _roleManager.CreateAsync(new ApplicationRole { Name = role });
                    }
                    var addResult = await _userManager.AddToRolesAsync(user, toAdd);
                    if (!addResult.Succeeded)
                        return Result<UserDto>.WithErrors(addResult.Errors.Select(e => new ValidationError { Code = e.Code, Description = e.Description }).ToList());
                }
            }

            return Result<UserDto>.SuccessResult(ToDto(user), "User updated successfully");
        }

        public async Task<Result<Unit>> DeleteUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return Result<Unit>.FailureResult("User not found");

            var res = await _userManager.DeleteAsync(user);
            if (!res.Succeeded)
            {
                return Result<Unit>.WithErrors(res.Errors.Select(e => new ValidationError { Description = e.Description }).ToList());
            }
            return Result<Unit>.SuccessResult(Unit.Value, "User deleted successfully");
        }

        public async Task<Result<UserDto>> GetByIdAsync(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return Result<UserDto>.NotFoundResult(userId);
            return Result<UserDto>.SuccessResult(ToDto(user));
        }

        public async Task<Result<List<UserDto>>> GetListAsync()
        {
            var users = await _userManager.Users.AsNoTracking().ToListAsync();
            var dtos = users.Select(ToDto).ToList();
            return Result<List<UserDto>>.SuccessResult(dtos);
        }

        public async Task<Result<Unit>> ChangePasswordAsync(int userId, ChangePasswordDto input)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return Result<Unit>.FailureResult("User not found");
            var res = await _userManager.ChangePasswordAsync(user, input.CurrentPassword, input.NewPassword);
            if (!res.Succeeded)
            {
                return Result<Unit>.WithErrors(res.Errors.Select(e => new ValidationError { Description = e.Description }).ToList());
            }
            return Result<Unit>.SuccessResult(Unit.Value, "Password changed");
        }

        public async Task<Result<Unit>> AssignRolesAsync(int userId, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return Result<Unit>.FailureResult("User not found");

            var current = await _userManager.GetRolesAsync(user);
            var toRemove = current.Except(roles).ToArray();
            var toAdd = roles.Except(current).ToArray();

            if (toRemove.Length > 0)
            {
                var removeRes = await _userManager.RemoveFromRolesAsync(user, toRemove);
                if (!removeRes.Succeeded)
                    return Result<Unit>.WithErrors(removeRes.Errors.Select(e => new ValidationError { Description = e.Description }).ToList());
            }
            if (toAdd.Length > 0)
            {
                foreach (var role in toAdd)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        var createRole = await _roleManager.CreateAsync(new ApplicationRole { Name = role });
                        if (!createRole.Succeeded)
                            return Result<Unit>.WithErrors(createRole.Errors.Select(e => new ValidationError { Description = e.Description }).ToList());
                    }
                }
                var addRes = await _userManager.AddToRolesAsync(user, toAdd);
                if (!addRes.Succeeded)
                    return Result<Unit>.WithErrors(addRes.Errors.Select(e => new ValidationError { Description = e.Description }).ToList());
            }

            return Result<Unit>.SuccessResult(Unit.Value, "Roles updated");
        }

        private static UserDto ToDto(ApplicationUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                HospitalId = user.HospitalId
            };
        }
    }
}
