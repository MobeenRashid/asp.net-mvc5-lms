using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Debugtime.Common.Persistence;
using System.Data.Entity;

namespace Debugtime.Master.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext _context;
        // GET: Transactions
        public ActionResult Index()
        {
            _context = new ApplicationDbContext();
            var transactions = _context.Transactions.Include(t => t.Buyer.UserProfile).Include(t => t.Statement).ToList();
            return View(transactions);
        }
    }
}