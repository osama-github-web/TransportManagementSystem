using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    [NotMapped]
    public string? Role {  get; set; }
    [NotMapped]
    public string? Password {  get; set; }

    public ApplicationUser Update(ApplicationUser user) 
    { 
        if(!string.IsNullOrEmpty(user.UserName))
            this.UserName = user.UserName;
        if(!string.IsNullOrEmpty(user.Email))
            this.Email = user.Email;
        if(!string.IsNullOrEmpty(user.PhoneNumber))
            this.PhoneNumber = user.PhoneNumber;
        return this;
    }
}
