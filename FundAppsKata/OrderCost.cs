using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAppsKata
{
    public class OrderCost
    {
        public List<PackageCost> Packages {get;set;}
        public double Total { get; set; }
        public double SpeedyShippingTotal { get; set; }
        public double MultiParcelTotal { get; set; }
    }
}
