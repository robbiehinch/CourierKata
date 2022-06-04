using Microsoft.VisualStudio.TestTools.UnitTesting;
using FundAppsKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAppsKata.Tests
{
    [TestClass()]
    public class CostCalculatorTests
    {
        [TestMethod()]
        public void Calculate1by1by1()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=1
                }
            });

            var singleResult = result.Packages.Single();

            //Check package not modified
            Assert.AreEqual(1, singleResult.Dimensions.HeightCm);
            Assert.AreEqual(1, singleResult.Dimensions.WidthCm);
            Assert.AreEqual(1, singleResult.Dimensions.DepthCm);

            //Check package calculation
            Assert.AreEqual(PackageSize.Small, singleResult.Size);
            Assert.AreEqual(3, singleResult.Cost);

            //Check Total Calculation
            Assert.AreEqual(3, result.Total);
        }

        [TestMethod()]
        public void CalculateMediumParcel()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=10
                }
            });

            var singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Medium, singleResult.Size);
            Assert.AreEqual(8, singleResult.Cost);

            //Check Total Calculation
            Assert.AreEqual(8, result.Total);
        }

        [TestMethod()]
        public void CalculateMediumParcelAllDimensions()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=10,
                    WidthCm=1,
                    DepthCm=1
                }
            });

            Assert.AreEqual(8, result.Total);
            
            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=10,
                    DepthCm=1
                }
            });

            Assert.AreEqual(8, result.Total);
        }

        [TestMethod()]
        public void CalculateLargeParcel()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=50
                }
            });

            var singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Large, singleResult.Size);
            Assert.AreEqual(15, singleResult.Cost);

            //Check Total Calculation
            Assert.AreEqual(15, result.Total);
        }

        [TestMethod()]
        public void CalculateXlParcel()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=100
                }
            });

            var singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.XL, singleResult.Size);
            Assert.AreEqual(25, singleResult.Cost);

            //Check Total Calculation
            Assert.AreEqual(25, result.Total);
        }

        [TestMethod()]
        public void CalculateTotalXlAndSmallParcelsTotal()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=9
                },
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=100
                }
            });

            Assert.AreEqual(2, result.Packages.Count);

            //Check Total Calculation
            Assert.AreEqual(25 + 3, result.Total);
            Assert.AreEqual((25 + 3) * 2, result.SpeedyShippingTotal);
        }

        [TestMethod()]
        public void CheckNegativeDimensionThrows()
        {
            var negativeDimensionPackage = new PackageDimensions
            {
                HeightCm = -1,
                WidthCm = 1,
                DepthCm = 9
            };

            Assert.ThrowsException<ArgumentException>(
                () => CostCalculator.Calculate(new[]
                {
                    negativeDimensionPackage
                })
                );
        }
    }
}