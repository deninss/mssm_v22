
using Microsoft.AspNetCore.Mvc;
using mssm.Models.AuthModel;
using mssm.Services.IAuth;

namespace mssm.Controllers
{
    public class AuthController : Controller
    {
        private readonly _IAuth _iAuth; 

        public AuthController(_IAuth iAuth)
        {
            _iAuth = iAuth;
        }
        public async Task<IActionResult> Login() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if (await _iAuth.LoginAsync(login)) return Redirect("/");
            return View();
        }
        public async Task<IActionResult> Register() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel register) 
        {
            if (ModelState.IsValid)
            {
                if (await _iAuth.RegisterAsync(register)) return await Login();
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            if (await _iAuth.Logout()) return RedirectToAction("Login", "Auth");
            else return NoContent();
        }
    }
}
