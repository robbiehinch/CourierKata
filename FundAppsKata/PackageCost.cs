using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAppsKata
{
    public enum PackageSize
    {
        Small,
        Medium,
        Large,
        XL,
        Heavy
    }

    public class PackageCost
    {
        public PackageDimensions Dimensions {get;set;}
        public double Cost { get; set; }
        public PackageSize Size { get; set; }
    }
}
