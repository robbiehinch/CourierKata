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

        private static HashSet<PackageCost> CheapestPackages(IEnumerable<PackageCost> packageCost, int discountSize)
        {
            var sorted = packageCost
                .OrderBy(_ => _.Cost)
                .ToList();
            var discounts = sorted.Count / discountSize;
            return sorted
                .Take(discounts)
                .ToHashSet();
        }

        private static double CalculateDiscount(List<PackageCost> packages)
        {
            var packagesBySize = packages
                .ToLookup(_ => _.Size);
            var smallPackages = packagesBySize[PackageSize.Small];
            var smallDiscountPackages = CheapestPackages(smallPackages, 4);

            var mediumPackages = packagesBySize[PackageSize.Medium];
            var mediumDiscountPackages = CheapestPackages(mediumPackages, 3);

            var remainingDiscountPackages = packages
                .Where(_ => !mediumDiscountPackages.Contains(_) && !smallDiscountPackages.Contains(_))
                .ToList();

            var fifthParcelDiscountPackages = CheapestPackages(remainingDiscountPackages, 5);

            return smallDiscountPackages.Sum(_ => _.Cost)
                + mediumDiscountPackages.Sum(_ => _.Cost)
                + fifthParcelDiscountPackages.Sum(_ => _.Cost);
        }

        public static OrderCost Calculate(IEnumerable<PackageDimensions> packages)
        {
            var packageCosts = packages
                .Select(_ => Calculate(_))
                .ToList();

            var total = packageCosts.Sum(_ => _.Cost);
            var discount = CalculateDiscount(packageCosts);
            var discountTotal = total - discount;
            var speedyShipping = discountTotal * 2;
            return new OrderCost
            {
                Packages = packageCosts,
                Total = total,
                MultiParcelTotal = discountTotal,
                SpeedyShippingTotal = speedyShipping
            };
        }
    }
}