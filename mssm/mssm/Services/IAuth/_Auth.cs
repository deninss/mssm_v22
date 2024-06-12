using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mssm.Context;
using mssm.Models.AuthModel;
using mssm.Models.ProjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using mssm.Services.IEmail;
using System;

namespace mssm.Services.IAuth
{
    public class _Auth : _IAuth
    {
        private readonly SignInManager<UserContext> _signInManager;
        private readonly UserManager<UserContext> _userManager;
        private readonly ILogger<_Auth> _logger;

        public _Auth(SignInManager<UserContext> signInManager, UserManager<UserContext> userManager, ILogger<_Auth> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<bool> LoginAsync(LoginModel login)
        {
            var identityUser = await _userManager.FindByEmailAsync(login.Email);
            bool flag = false;
            if (login.RememberMe == "on") flag = true;
            var result = await PasswordSignInAsync(login.Email, login.Password, flag, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return await _userManager.CheckPasswordAsync(identityUser, login.Password);
            }
            return false;
        }
        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await _signInManager.PasswordSignInAsync(user.UserName, password, isPersistent, lockoutOnFailure);
        }
        public async Task<bool> Logout()
        { 
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating project: {ex.Message}");
                throw;
            }
        }

      
        public async Task<bool> RegisterAsync(RegisterModel register)
        {
            var users = Activator.CreateInstance<UserContext>();
            users.UserName = register.Name + " " + register.Surname;
            users.Name = register.Name;
            users.Surname = register.Surname;
            users.Patronymic = register.Patronymic;
            users.Email = register.Email;
            users.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(users, register.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created successfully.");
                if (_userManager.Options.SignIn.RequireConfirmedAccount) return true;
                else
                {
                    await _signInManager.SignInAsync(users, isPersistent: false);
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(users);
                    //var callbackUrl = Url.Action(
                    //    "ConfirmEmail",
                    //    "Account",
                    //    new { userId = users.Id, code = code },
                    //    protocol: HttpContext.Request.Scheme);
                    //EmailService emailService = new EmailService();
                    //await emailService.SendEmailAsync(register.Email, "Confirm your account",
                    //    $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");
                    return true;
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Error in user creation: {error.Code} - {error.Description}");
                }
            }

            return false;
        }
        private UserContext CreateUser()
        {
            try
            {
                return Activator.CreateInstance<UserContext>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(UserContext)}'. " +
                    $"Ensure that '{nameof(UserContext)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

    }
}
