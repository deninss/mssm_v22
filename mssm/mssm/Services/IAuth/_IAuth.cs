 
using Microsoft.AspNetCore.Mvc;
using mssm.Models.AuthModel;

namespace mssm.Services.IAuth
{
    public interface _IAuth
    {
        Task<bool> LoginAsync(LoginModel login);
        Task<bool> RegisterAsync(RegisterModel register);
        Task<bool> Logout();
    }
}
