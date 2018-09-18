using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Model.View
{
    public class CartPurchaseViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }

        public double TotalToPay
        {
            get
            {
                Discount = Discount > Price ? Price : Discount;
                return Price - Discount;
            }
        }
    }
}
