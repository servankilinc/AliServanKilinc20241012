using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService _accountService;

        public HomeController(IAccountService accountService)
        { 
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            Claim? nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (nameId == null) return RedirectToAction("Login", "Auth");

            Guid userId = Guid.Parse(nameId.Value);
            var accountList =  await _accountService.GetUserAccountsWithLastTransfersAsync(userId);
            return View(accountList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
