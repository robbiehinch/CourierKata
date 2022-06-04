namespace FundAppsKata
{
    public static class CostCalculator
    {
        private static Tuple<double, PackageSize> CalculateParcelCost(PackageDimensions packageDimensions)
        {
            var minDimension = Math.Min(Math.Min(packageDimensions.HeightCm, packageDimensions.WidthCm), packageDimensions.DepthCm);
            if (minDimension <= 0)
                throw new ArgumentException($"Package dimensions incorrectly entered, small dimension is {minDimension} - {packageDimensions}");

            var maxDimension = Math.Max(Math.Max(packageDimensions.HeightCm, packageDimensions.WidthCm), packageDimensions.DepthCm);
            if (maxDimension < 10)
                return Tuple.Create(3.0, PackageSize.Small);
            if (maxDimension < 50)
                return Tuple.Create(8.0, PackageSize.Medium);
            if (maxDimension < 100)
                return Tuple.Create(15.0, PackageSize.Large);
            
            return Tuple.Create(25.0, PackageSize.XL);
        }

        private static PackageCost Calculate(PackageDimensions packageDimensions)
        {
            var parcelCost = CalculateParcelCost(packageDimensions);
            return new PackageCost
            {
                Cost = parcelCost.Item1,
                Dimensions = packageDimensions,
                Size = parcelCost.Item2
            };
        }

        public static OrderCost Calculate(IEnumerable<PackageDimensions> packages)
        {
            var packageCosts = packages
                .Select(_ => Calculate(_))
                .ToList();

            var total = packageCosts.Sum(_ => _.Cost);
            var speedyShipping = total * 2;
            return new OrderCost
            {
                Packages = packageCosts,
                Total = total,
                SpeedyShippingTotal = speedyShipping
            };
        }
    }
}