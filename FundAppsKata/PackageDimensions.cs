using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAppsKata
{
    public class PackageDimensions
    {
        public int HeightCm { get; set; }
        public int WidthCm { get; set; }
        public int DepthCm { get; set; }
        public int WeightKg { get; set; }

        public override string ToString()
        {
            return $"Height: {HeightCm}, Width: {WidthCm}, Depth: {DepthCm}";
        }
    }
}
