using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TracNghiem.Models;
using TracNghiem.PageModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace TracNghiem.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly TracNghiemContext _context;
        public AccountController(ILogger<AccountController> logger, TracNghiemContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            return View(new LoginModel(){ReturnUrl = ReturnUrl, rememberMe = "false"});
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model) {
            _logger.LogInformation(ModelState.IsValid.ToString());
            // foreach (var state in ModelState) {
            //     foreach (var error in state.Value.Errors) {
            //         _logger.LogWarning(error.ErrorMessage);
            //     }
            // }
            if (ModelState.IsValid) {
                bool remember = model.rememberMe == "true";
                var user = await _context.TbNguoiDungs.Include(user => user.IdVaiTroNavigation).Where(t => t.TenDangNhap == model.username && t.MatKhau == model.password).FirstOrDefaultAsync();
                // _logger.LogInformation($"{model.username}, {model.password}");
                if (user == null)
                {
                    bool check = _context.TbNguoiDungs.Any(item => item.TenDangNhap == model.username);
                    if (check) ModelState.AddModelError("password", "Mật khẩu sai.");
                    else ModelState.AddModelError("username", "Tài khoản này không tồn tại");
                    _logger.LogInformation("dwadwa");
                    return View(model);
                }
                await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.TenDangNhap),
                    new Claim("Password", user.MatKhau),
                    new Claim("FullName", ""+user.HoTen),
                    new Claim(ClaimTypes.Role, user.IdVaiTroNavigation.VaiTro),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = remember,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity), 
                    authProperties);

                _logger.LogInformation("User {Email} logged in at {Time}.", 
                    user.TenDangNhap, DateTime.Now);

                return LocalRedirect(Url.GetLocalUrl(model.ReturnUrl));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("username,password,confirm_password")] RegisterModel model) {
            var check = _context.TbNguoiDungs.Any(t => t.TenDangNhap == model.username);
            if (check == true) ModelState.AddModelError("username", "Đã tồn tại");
            if (model.password != model.confirm_password) ModelState.AddModelError("confirm_password", "Nhập lại mật khẩu giống mật khẩu trước đó");
            if (ModelState.IsValid) {
                var user = new TbNguoiDung(){
                    TenDangNhap = model.username,
                    MatKhau = model.password,
                    IdVaiTro = 1,
                };
                _context.TbNguoiDungs.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}