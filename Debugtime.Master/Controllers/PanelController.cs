using System.Web.Mvc;
using Debugtime.Common.Infrastructure;
using Debugtime.Common.Model.Input;
using Debugtime.Master.Controllers.Base;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Debugtime.Common.Infrastructure.Master;
using Debugtime.Common.Rest_Services.Concretes;

namespace Debugtime.Master.Controllers
{
    [Authorize]
    public class PanelController : BaseRestController
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var summary = await UserRestService.GetUsageSummaryAsync();

            return View(summary);
        }
        [ActionName("aspnet-mvc-helper")]
        public ActionResult AspNetMvcHelper()
        {
            FinancialDataCollection data = new FinancialDataCollection();
            return View("aspnet-mvc-helper", data);
        }

    }
    public class FinancialDataCollection : List<FinancialDataPoint>
    {
        public FinancialDataCollection()
        {
            this.Add(new FinancialDataPoint { Spending = 20, Budget = 60, Label = "Administration", });
            this.Add(new FinancialDataPoint { Spending = 80, Budget = 40, Label = "Sales", });
            this.Add(new FinancialDataPoint { Spending = 80, Budget = 40, Label = "Marketing", });
            this.Add(new FinancialDataPoint { Spending = 20, Budget = 60, Label = "Development", });
            this.Add(new FinancialDataPoint { Spending = 60, Budget = 20, Label = "Customer Support", });
            this.Add(new FinancialDataPoint { Spending = 20, Budget = 60, Label = "IT", });
        }

    }

    public class FinancialDataPoint
    {
        public string Label { get; set; }
        public double Spending { get; set; }
        public double Budget { get; set; }


        public string ToolTip { get { return String.Format("{0}, Spending {1}, Budget {2}", Label, Spending, Budget); } }

    }


}