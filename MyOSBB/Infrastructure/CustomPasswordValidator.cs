using Microsoft.AspNetCore.Identity;
using MyOSBB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOSBB.Infrastructure
{
    public class CustomPasswordValidator : PasswordValidator<ApplicationUser>
    {

        public override async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
        {

            IdentityResult result = await base.ValidateAsync(manager, user, password);

            List<IdentityError> errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

            //if (String.IsNullOrEmpty(password) || password.Length < RequiredLength)
            //{
            //    errors.Add(new IdentityError
            //    {
            //        Description = String.Format("Минимальная длина пароля равна {0}", RequiredLength)
            //    });
            //}

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainsUserName",
                    Description = "Password cannot contain username"
                });
            }

            if (password.Contains("12345"))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainsSequence",
                    Description = "Password cannot contain numeric sequence"
                });
            }

            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}
