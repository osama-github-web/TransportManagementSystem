using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities;
using TMS.Domain.GenericResponse;

namespace IMS.Application.EFCore.Repositories;

public class ApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationUserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<List<ApplicationUser>?> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }
    
    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }
    
    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
    
    public async Task<ApplicationUser?> GetUserByNameAsync(string name)
    {
        return await _userManager.FindByNameAsync(name);
    }

    public async Task<List<string>?> GetUserRolesAsync(ApplicationUser applicationUser)
    {
        var roles = await _userManager.GetRolesAsync(applicationUser);
        return roles.ToList();
    }

    public async Task<TMSResponse?> CreateUserAsync(ApplicationUser applicationUser)
    {
        var _user = await _userManager.FindByEmailAsync(applicationUser.Email);
        if (applicationUser == null || _user != null)
            return new TMSResponse("User Not Found",applicationUser);

        var identityResult = await _userManager.CreateAsync(applicationUser, applicationUser.Password);
        if(!identityResult.Succeeded)
            return new TMSResponse("User Not Created", applicationUser);

        if (!await _roleManager.RoleExistsAsync(applicationUser.Role))
            await _roleManager.CreateAsync(new IdentityRole(applicationUser.Role));

        identityResult = await _userManager.AddToRoleAsync(applicationUser, applicationUser.Role);
        if (!identityResult.Succeeded)
            return new TMSResponse("User Role Not Added", applicationUser);
        return new TMSResponse("User Create Successfully", applicationUser, true);
    }
    
    public async Task<TMSResponse?> UpdateUserAsync(ApplicationUser applicationUser)
    {
        var _user = await _userManager.FindByEmailAsync(applicationUser.Email);
        if (applicationUser == null || _user != null)
            return new TMSResponse("User Not Found", applicationUser);

        _user?.Update(applicationUser);

        var identityResult = await _userManager.UpdateAsync(_user);
        if(!identityResult.Succeeded)
            return new TMSResponse("User Not Updated"+identityResult.Errors.ToString(), applicationUser);
        return new TMSResponse("User Updated Successfully ", applicationUser, true);
    }
    
    public async Task<TMSResponse?> RemoveUserAsync(ApplicationUser applicationUser)
    {
        ApplicationUser _user = null;
        if (!string.IsNullOrWhiteSpace(applicationUser.Email))
            _user = await _userManager.FindByEmailAsync(applicationUser.Email);
        if(_user is null && !string.IsNullOrWhiteSpace(applicationUser.Id))
            _user = await _userManager.FindByIdAsync(applicationUser.Id);
        else if(_user is null && !string.IsNullOrWhiteSpace(applicationUser.UserName))
            _user = await _userManager.FindByNameAsync(applicationUser.UserName);

        if (_user == null)
            return new TMSResponse("User Not Found", applicationUser);

        var identityResult = await _userManager.DeleteAsync(_user);
        if(!identityResult.Succeeded)
            return new TMSResponse("User Not Deleted", applicationUser);
        return new TMSResponse("User Deleted Successfully ", applicationUser,true);
    }
    
    public async Task<TMSResponse?> UpdateUserPasswordAsync(ApplicationUser applicationUser)
    {
        var _user = await _userManager.FindByEmailAsync(applicationUser.Email);
        if (applicationUser == null || _user != null)
            return new TMSResponse("User Not Found", applicationUser);

        var identityResult = await _userManager.RemovePasswordAsync(_user);
        if(!identityResult.Succeeded)
            return new TMSResponse("User Password Not Removed "+identityResult.Errors.ToString(), applicationUser);

        identityResult = await _userManager.AddPasswordAsync(_user,applicationUser.Password);
        if(!identityResult.Succeeded)
            return new TMSResponse("User Password Not Updated", applicationUser);
        return new TMSResponse("User  Password Updated Successfully ", applicationUser, true);
    }
    
    public async Task<TMSResponse?> AddRoleToUserAsync(ApplicationUser applicationUser)
    {
        var _user = await _userManager.FindByEmailAsync(applicationUser.Email);
        if (applicationUser == null || _user != null)
            return new TMSResponse("User Not Found", applicationUser);

        var identityResult = await _userManager.AddToRoleAsync(_user,applicationUser.Role);
        if (!identityResult.Succeeded)
            return new TMSResponse("User Role Not Updated"+identityResult.Errors.ToString(), applicationUser);
        return new TMSResponse("User Role Updated Successfully ", applicationUser, true);
    }

    public async Task<TMSResponse?> RemoveRoleFromUserAsync(ApplicationUser applicationUser)
    {
        var _user = await _userManager.FindByEmailAsync(applicationUser.Email);
        if (applicationUser == null || _user != null)
            return new TMSResponse("User Not Found", applicationUser);

        var identityResult = await _userManager.RemoveFromRoleAsync(_user,applicationUser.Role);
        if (!identityResult.Succeeded)
            return new TMSResponse("User Role Not Removed" + identityResult.Errors.ToString(), applicationUser);
        return new TMSResponse("User Role Removed Successfully ", applicationUser, true);
    }
}
