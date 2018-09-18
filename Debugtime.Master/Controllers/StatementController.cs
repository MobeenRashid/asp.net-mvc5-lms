using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Debugtime.Common.Persistence;

namespace Debugtime.Master.Controllers
{
    public class StatementController : Controller
    {
        private ApplicationDbContext _context;
        // GET: Transactions
        public ActionResult Index()
        {
            _context = new ApplicationDbContext();
            var transactions = _context.Transactions.Include(t => t.Buyer.UserProfile).Include(t=>t.Course).Include(t => t.Statement).ToList();
            return View(transactions);
        }

        public ActionResult Invoice()
        {
            return View();
        }
    }
}