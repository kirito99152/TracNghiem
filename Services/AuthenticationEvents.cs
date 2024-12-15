using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TracNghiem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TracNghiem.Services
{
    public class AuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly TracNghiemContext _context;
        private readonly ILogger<AuthenticationEvents> _logger;
        public AuthenticationEvents(TracNghiemContext context, ILogger<AuthenticationEvents> logger)
        {
            _context = context;
            _logger = logger;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            // Look for the LastChanged claim.
            var username = (from c in userPrincipal.Claims
                            where c.Type == ClaimTypes.Name
                            select c.Value).FirstOrDefault();
            var password = (from c in userPrincipal.Claims
                            where c.Type == "Password"
                            select c.Value).FirstOrDefault();
            bool check = _context.TbNguoiDungs.Any(t => t.TenDangNhap == username && t.MatKhau == password);
            _logger.LogInformation($"User {username} is checked at {DateTime.Now}.");
            if (check == false)
            {
                _logger.LogInformation($"User {username} is failed to Authenticate at {DateTime.Now}.");
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}