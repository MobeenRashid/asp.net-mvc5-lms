using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DebugTime.Domain.Model;

namespace Debugtime.Master.Models.View
{
    public class MasterDashboardViewModel
    {
        public MasterDashboardViewModel()
        {
            Transactions = new HashSet<Transaction>();
        }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}