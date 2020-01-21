using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SimpleHashing;

using Banking.Data;
using Banking.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Banking.Controllers
{
    public class LoginController : Controller
    {
        private readonly BankingContext _context;

        public IActionResult Index() => View();

        public LoginController(BankingContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string userId, string password)
        {
            var login = await _context.Login.FindAsync(userId);
            if (login == null || !PBKDF2.Verify(login.PasswordHash, password))
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                return View(new Login { UserID = userId });
            }
            HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.Customer.CustomerID);
            HttpContext.Session.SetString(nameof(Customer.Name), login.Customer.Name);

            return RedirectToAction("Index", "Customer");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }
}
