using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cerveceria2._0.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria2._0.Controllers.Account
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicationUsersController(ApplicationDbContext context,
                                          UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Administrator/ApplicationUsers
        public IActionResult Index()
        {
            var lstUsers = _userManager.Users.ToList();
            return View(lstUsers);
        }
    }
}
