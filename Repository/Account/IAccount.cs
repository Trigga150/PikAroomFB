using Microsoft.Owin;
using PikAroomFB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikAroomFB.Repository.Account
{
    internal interface IAccount
    {
        Task SignUp(SignUp signUp);
        Task <string> Login(Models.Login login,  string returnUrl, IOwinContext owinContext);
        
        

        Task PasswordResetLink(string emailID);
        
    }
}
