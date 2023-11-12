using Firebase.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using PikAroomFB.Models;
using PikAroomFB.Repository.Account;
using PikAroomFB.Repository.DataConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PikAroomFB.Controllers
{
    public class AccountController : Controller
    {
        private AccountRepository _accountRepository;

        public static string Web_ApiKey = "AIzaSyAMCPbpzgXauvU9DBD3sJuXCN8RPv1AbIM";

        public AccountController()
        {
            _accountRepository = new AccountRepository();
        }
        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(SignUp signUp)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(Web_ApiKey));
                var a = await auth.CreateUserWithEmailAndPasswordAsync(signUp.Email, signUp.Password, signUp.Name);
                ModelState.AddModelError(string.Empty, "Please Verify Your Email then Login!!!");

                //await _accountRepository.SignUp(signUp);
                // ModelState.AddModelError(string.Empty, "Please Verify Your Email then Login!!!");
            }
            catch (Exception ex) 
            { 
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]      
        public ActionResult Login(string returnUrl)
        {


            try
            {
               if(this.Request.IsAuthenticated)
                {
                    return this.RedirectToUser(returnUrl);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
       // public async Task<ActionResult> Login(Models.Login login, string returnUrl, IOwinContext owinContext)
            public async Task<ActionResult> Login(Models.Login login, string returnUrl)
        {
           if(ModelState.IsValid)
            {

                try
                {
                    if(ModelState.IsValid)
                    {
                        var auth = new FirebaseAuthProvider(new FirebaseConfig(Web_ApiKey));
                        var a = await auth.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
                        string token = a.FirebaseToken;
                        var user = a.User;

                        if (token != "")
                        {
                            SignInUser(user.Email, token, false);
                            return RedirectToUser(returnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error username or password is incorrect");
                        }
                    }
                }
                catch
                {

                }



                //IOwinContext owenContext = Request.GetOwinContext();
                //string returnValue = await _accountRepository.Login(login, returnUrl, owinContext);
                //if(!String.IsNullOrEmpty(returnValue) && (returnValue != "Admin" || returnValue != "User"))
                //{
                //    System.Web.HttpContext.Current.Session.Add("Email", login.Email);
                //    if(returnValue=="Admin")
                //    {
                //        System.Web.HttpContext.Current.Session.Add("AccessRight", "Admin");
                //    }
                //    if(returnValue=="User")
                //    {
                //        System.Web.HttpContext.Current.Session.Add("AccessRight", "User");
                //    }
                //    return RedirectToAction("Index", "Home");

                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, returnValue);
                //}
            }
           else
            {
                ModelState.AddModelError(string.Empty, "Invalid login credentials");
            }
            
            return View(login);

        }



        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginAdmin(string returnUrl)
        {


            try
            {
                if (this.Request.IsAuthenticated)
                {
                    return this.RedirectToAdmin(returnUrl);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        // public async Task<ActionResult> Login(Models.Login login, string returnUrl, IOwinContext owinContext)
        public async Task<ActionResult> LoginAdmin(Models.Login login, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                        var auth = new FirebaseAuthProvider(new FirebaseConfig(Web_ApiKey));
                        var a = await auth.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
                        string token = a.FirebaseToken;
                        var user = a.User;

                        if (token != "")
                        {
                            SignInUser(user.Email, token, false);
                            return RedirectToAdmin(returnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error username or password is incorrect");
                        }
                    }
                }
                catch
                {

                }



                //IOwinContext owenContext = Request.GetOwinContext();
                //string returnValue = await _accountRepository.Login(login, returnUrl, owinContext);
                //if(!String.IsNullOrEmpty(returnValue) && (returnValue != "Admin" || returnValue != "User"))
                //{
                //    System.Web.HttpContext.Current.Session.Add("Email", login.Email);
                //    if(returnValue=="Admin")
                //    {
                //        System.Web.HttpContext.Current.Session.Add("AccessRight", "Admin");
                //    }
                //    if(returnValue=="User")
                //    {
                //        System.Web.HttpContext.Current.Session.Add("AccessRight", "User");
                //    }
                //    return RedirectToAction("Index", "Home");

                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, returnValue);
                //}
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login credentials");
            }

            return View(login);

        }
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            System.Web.HttpContext.Current.Session.Remove("AccessRight");
            System.Web.HttpContext.Current.Session.Remove("Email");
            System.Web.HttpContext.Current.Session.Clear();
            return RedirectToAction("Index", "Home", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try 
            { 
                if(Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return RedirectToAction("Index", "Property");
        }
        private ActionResult RedirectToAdmin(string returnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Admin");
        }
        private ActionResult RedirectToUser(string returnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("UserLayout", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(string EmailID)
        {
            await _accountRepository.PasswordResetLink(EmailID);
            ViewBag.Message = "Reset password Link has been sent to : " + EmailID;
            return View();
        }

        private void SignInUser(string email,string token, bool isPersistent)
        {
            var claims = new List<Claim> ();
            try
            {
                claims.Add(new Claim(ClaimTypes.Email, email));
                claims.Add(new Claim(ClaimTypes.Authentication, token));
                var claimIdentities = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authethicationManager = ctx.Authentication;
                authethicationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties() { IsPersistent = isPersistent }, claimIdentities);
            }
            catch(Exception ex)
            {
                throw ex;
            }




        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginUser(string returnUrl)
        {


            try
            {
                if (this.Request.IsAuthenticated)
                {
                    return this.RedirectToUser(returnUrl);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        // public async Task<ActionResult> Login(Models.Login login, string returnUrl, IOwinContext owinContext)
        public async Task<ActionResult> LoginUser(Models.Login login, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                        var auth = new FirebaseAuthProvider(new FirebaseConfig(Web_ApiKey));
                        var a = await auth.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
                        string token = a.FirebaseToken;
                        var user = a.User;

                        if (token != "")
                        {
                            SignInUser(user.Email, token, false);
                            return RedirectToUser(returnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error username or password is incorrect");
                        }
                    }
                }
                catch
                {

                }



                //IOwinContext owenContext = Request.GetOwinContext();
                //string returnValue = await _accountRepository.Login(login, returnUrl, owinContext);
                //if(!String.IsNullOrEmpty(returnValue) && (returnValue != "Admin" || returnValue != "User"))
                //{
                //    System.Web.HttpContext.Current.Session.Add("Email", login.Email);
                //    if(returnValue=="Admin")
                //    {
                //        System.Web.HttpContext.Current.Session.Add("AccessRight", "Admin");
                //    }
                //    if(returnValue=="User")
                //    {
                //        System.Web.HttpContext.Current.Session.Add("AccessRight", "User");
                //    }
                //    return RedirectToAction("Index", "Home");

                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, returnValue);
                //}
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login credentials");
            }

            return View(login);

        }


    }
}