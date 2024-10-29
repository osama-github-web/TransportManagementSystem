using Microsoft.AspNetCore.Identity;
using TMS.Domain.Entities;

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

    public async Task<ApplicationUser?> CreateUserAsync(ApplicationUser applicationUser)
    {
        var _user = await _userManager.FindByEmailAsync(applicationUser.Email);
        if (applicationUser == null || _user != null)
            return null;

        var identityResult = await _userManager.CreateAsync(applicationUser, applicationUser.Password);
        if(!identityResult.Succeeded) 
            return null;

        if (!await _roleManager.RoleExistsAsync(applicationUser.Role))
            await _roleManager.CreateAsync(new IdentityRole(applicationUser.Role));

        identityResult = await _userManager.AddToRoleAsync(applicationUser, applicationUser.Role);
        if (identityResult.Succeeded)
            return null;
        return applicationUser;
    }
    
    public async Task<ApplicationUser?> UpdateUserAsync(ApplicationUser applicationUser)
    {
        var _user = await _userManager.FindByEmailAsync(applicationUser.Email);
        if (applicationUser == null || _user != null)
            return null;
        
        _user?.Update(applicationUser);

        var identityResult = await _userManager.UpdateAsync(_user);
        if(!identityResult.Succeeded) 
            return null;
        return applicationUser;
    }
    
    public async Task<ApplicationUser?> DeleteUserAsync(ApplicationUser applicationUser)
    {
        var _user = await _userManager.FindByEmailAsync(applicationUser.Email);
        if (applicationUser == null || _user != null)
            return null;

        var identityResult = await _userManager.DeleteAsync(_user);
        if(!identityResult.Succeeded) 
            return null;
        return applicationUser;
    }
    
    public async Task<ApplicationUser?> UpdateUserPasswordAsync(ApplicationUser applicationUser)
    {
        var _user = await _userManager.FindByEmailAsync(applicationUser.Email);
        if (applicationUser == null || _user != null)
            return null;

        var identityResult = await _userManager.RemovePasswordAsync(_user);
        if(!identityResult.Succeeded) 
            return null;
        
        identityResult = await _userManager.AddPasswordAsync(_user,applicationUser.Password);
        if(!identityResult.Succeeded) 
            return null;
        return applicationUser;
    }
    
    public async Task<ApplicationUser?> AddRoleToUserAsync(ApplicationUser applicationUser)
    {
        var _user = await _userManager.FindByEmailAsync(applicationUser.Email);
        if (applicationUser == null || _user != null)
            return null;
        
        var identityResult = await _userManager.AddToRoleAsync(_user,applicationUser.Role);
        if(!identityResult.Succeeded) 
            return null;
        return applicationUser;
    }
    
    public async Task<ApplicationUser?> RemoveRoleFromUserAsync(ApplicationUser applicationUser)
    {
        var _user = await _userManager.FindByEmailAsync(applicationUser.Email);
        if (applicationUser == null || _user != null)
            return null;
        
        var identityResult = await _userManager.RemoveFromRoleAsync(_user,applicationUser.Role);
        if(!identityResult.Succeeded) 
            return null;
        return applicationUser;
    }
}
