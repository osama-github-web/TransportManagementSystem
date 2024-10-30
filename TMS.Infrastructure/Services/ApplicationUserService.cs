using IMS.Application.EFCore.Repositories;
using TMS.Domain.Entities;
using TMS.Domain.GenericResponse;

namespace TMS.Infrastructure.Services;

public class ApplicationUserService
{
    private readonly ApplicationUserRepository _applicationUserRepository;

    public ApplicationUserService(ApplicationUserRepository applicationUserRepository)
    {
        this._applicationUserRepository = applicationUserRepository;
    }

    public async Task<List<ApplicationUser>?> GetAllUsersWithRolesAsync()
    {
        var _users = await _applicationUserRepository.GetAllUsersAsync();
        foreach (var user in _users)
        {
            var _roles = await _applicationUserRepository.GetUserRolesAsync(user);
            if (_roles != null)
                user.Role = _roles.FirstOrDefault();
        }
        return _users;
    }
    
    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
        return await _applicationUserRepository.GetUserByIdAsync(userId);
    }
    
    public async Task<TMSResponse?> RemoveUserAsync(ApplicationUser applicationUser)
    {
        return await _applicationUserRepository.RemoveUserAsync(applicationUser);
    }
    
    public async Task<TMSResponse?> CreateUserAsync(ApplicationUser applicationUser)
    {
        return await _applicationUserRepository.CreateUserAsync(applicationUser);
    }

    public async Task<TMSResponse?> UpdateUserAsync(ApplicationUser applicationUser)
    {
        return await _applicationUserRepository.UpdateUserAsync(applicationUser);
    }

    public async Task<TMSResponse?> DeleteUserAsync(ApplicationUser applicationUser)
    {
        return await _applicationUserRepository.RemoveUserAsync(applicationUser);
    }

    public async Task<TMSResponse?> UpdateUserPasswordAsync(ApplicationUser applicationUser)
    {
        return await _applicationUserRepository.UpdateUserPasswordAsync(applicationUser);
    }

    public async Task<TMSResponse?> AddRoleToUserAsync(ApplicationUser applicationUser)
    {
        return await _applicationUserRepository.AddRoleToUserAsync(applicationUser);
    }

    public async Task<TMSResponse?> RemoveRoleFromUserAsync(ApplicationUser applicationUser)
    {
        return await _applicationUserRepository.RemoveRoleFromUserAsync(applicationUser);
    }
}
