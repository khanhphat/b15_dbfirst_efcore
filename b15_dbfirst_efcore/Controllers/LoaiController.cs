using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using b15_dbfirst_efcore.Models;
using Microsoft.AspNetCore.Mvc;

namespace b15_dbfirst_efcore.Controllers
{
    public class LoaiController : Controller
    {
        private readonly MyeStoreContext _context;
        public LoaiController(MyeStoreContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}