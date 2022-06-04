namespace FundAppsKata
{
    public static class CostCalculator
    {
        private static Tuple<double, PackageSize> CalculateCharge(double basePrice, int weight, int limit, PackageSize standardSize)
        {
            var overweightAmount = weight - limit;
            var fullPrice = basePrice + (overweightAmount > 0 ? 2 * overweightAmount : 0);

            var heavyOverweightAmount = weight - 50;
            var overWeightPrice = 50.0 + (heavyOverweightAmount > 0 ? heavyOverweightAmount : 0);

            if (overWeightPrice < fullPrice)
                return Tuple.Create(overWeightPrice, PackageSize.Heavy);

            return Tuple.Create(fullPrice, standardSize);
        }

        private static Tuple<double, PackageSize> CalculateParcelCost(PackageDimensions packageDimensions)
        {
            var minDimension = Math.Min(Math.Min(packageDimensions.HeightCm, packageDimensions.WidthCm), packageDimensions.DepthCm);
            if (minDimension <= 0)
                throw new ArgumentException($"Package dimensions incorrectly entered, small dimension is {minDimension} - {packageDimensions}");

            var maxDimension = Math.Max(Math.Max(packageDimensions.HeightCm, packageDimensions.WidthCm), packageDimensions.DepthCm);
            if (maxDimension < 10)
            {
                return CalculateCharge(3.0, packageDimensions.WeightKg, 1, PackageSize.Small);
            }
            if (maxDimension < 50)
            {
                return CalculateCharge(8.0, packageDimensions.WeightKg, 3, PackageSize.Medium);
            }
            if (maxDimension < 100)
            {
                return CalculateCharge(15.0, packageDimensions.WeightKg, 6, PackageSize.Large);
            }

            return CalculateCharge(25.0, packageDimensions.WeightKg, 10, PackageSize.XL);
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