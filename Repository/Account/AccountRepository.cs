using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Newtonsoft.Json;
using PikAroomFB.Models;
using PikAroomFB.Repository.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PikAroomFB.Repository.Account
{
    public class AccountRepository : IAccount, IDisposable
    {
        private FirebaseConnect _connect;
        private Firebase.Auth.IFirebaseAuthProvider _authProvider;
        private IFirebaseClient _firebaseClient;
        public AccountRepository()
        {
            _connect = new FirebaseConnect();
            _authProvider = _connect.authProvider;
            _firebaseClient = _connect.firebaseClient;
        }
        public async Task<string> Login(Models.Login login, string returnUrl, IOwinContext owinContext)
        {
            bool isAdmin = false;
            var fbAuthenticationResponse = await _authProvider.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
            string token = fbAuthenticationResponse.FirebaseToken;
            var user = fbAuthenticationResponse.User;

            if (!string.IsNullOrEmpty(token))
            {
                var claims = new List<Claim>();
                try
                {
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.Authentication, token));
                    var claimIdentities = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    isAdmin = this.isAdmin(login);
                    var ctx = owinContext;
                    var authenticationManager = ctx.Authentication;
                    authenticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties() { IsPersistent = false }, claimIdentities);
                    if (isAdmin == false)
                    {
                        return "User";

                    }
                    else
                    {
                        return "Admin";
                    }
                }
                catch 
                {
                    return "Authentication Loging Failed!!!";
                }


                
            }
            else 
            {
                return "Token Generation Failled!!!";
            }
        }
        private bool isAdmin(Models.Login login)
        {
            FirebaseResponse firebaseResponse = _firebaseClient.Get("AccessRight");
            dynamic accessRightData = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);
            bool isAdmin = false;
            if (accessRightData != null)
            {
                foreach(var accessRightEmail in accessRightData)
                {
                    if(login.Email == accessRightEmail.First.Value.ToString())
                    {
                        isAdmin = true;
                    }
                }
            }
            return isAdmin;
        }
        public async Task SignUp(SignUp signUp)
        {
            await _authProvider.CreateUserWithEmailAndPasswordAsync(signUp.Email, signUp.Password, signUp.Password, true);
        }

        public async Task PasswordResetLink(string emailID)
        {
            await _authProvider.SendPasswordResetEmailAsync(emailID);
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}