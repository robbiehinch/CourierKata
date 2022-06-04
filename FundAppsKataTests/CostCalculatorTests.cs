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

        [TestMethod()]
        public void OverweightSmallCharges()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm = 1,
                    WidthCm = 1,
                    DepthCm = 1,
                    WeightKg = 1
                }
            });

            var singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Small, singleResult.Size);
            Assert.AreEqual(3, singleResult.Cost);

            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm = 1,
                    WidthCm = 1,
                    DepthCm = 1,
                    WeightKg = 2
                }
            });

            singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Small, singleResult.Size);
            Assert.AreEqual(5, singleResult.Cost);

            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm = 1,
                    WidthCm = 1,
                    DepthCm = 1,
                    WeightKg = 3
                }
            });

            singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Small, singleResult.Size);
            Assert.AreEqual(7, singleResult.Cost);
        }

        [TestMethod()]
        public void OverweightMediumParcel()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=10,
                    WeightKg = 3
                }
            });

            var singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Medium, singleResult.Size);
            Assert.AreEqual(8, singleResult.Cost);

            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=10,
                    WeightKg = 4
                }
            });

            singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Medium, singleResult.Size);
            Assert.AreEqual(10, singleResult.Cost);

            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=10,
                    WeightKg = 5
                }
            });

            singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Medium, singleResult.Size);
            Assert.AreEqual(12, singleResult.Cost);
        }

        [TestMethod()]
        public void OverweightLargeParcel()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=50,
                    WeightKg = 6
                }
            });

            var singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Large, singleResult.Size);
            Assert.AreEqual(15, singleResult.Cost);

            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=50,
                    WeightKg = 7
                }
            });

            singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Large, singleResult.Size);
            Assert.AreEqual(17, singleResult.Cost);

            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=50,
                    WeightKg = 8
                }
            });

            singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Large, singleResult.Size);
            Assert.AreEqual(19, singleResult.Cost);
        }

        [TestMethod()]
        public void OverweightXlParcel()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=100,
                    WeightKg = 10
                }
            });

            var singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.XL, singleResult.Size);
            Assert.AreEqual(25, singleResult.Cost);

            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=100,
                    WeightKg = 11
                }
            });

            singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.XL, singleResult.Size);
            Assert.AreEqual(27, singleResult.Cost);

            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=100,
                    WeightKg = 12
                }
            });

            singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.XL, singleResult.Size);
            Assert.AreEqual(29, singleResult.Cost);
        }

        [TestMethod()]
        public void HeavyParcelCharges()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm = 1,
                    WidthCm = 1,
                    DepthCm = 1,
                    WeightKg = 51
                }
            });

            var singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Heavy, singleResult.Size);
            Assert.AreEqual(51, singleResult.Cost);

            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=10,
                    WeightKg = 52
                }
            });

            singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Heavy, singleResult.Size);
            Assert.AreEqual(52, singleResult.Cost);

            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=50,
                    WeightKg = 53
                }
            });

            singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Heavy, singleResult.Size);
            Assert.AreEqual(53, singleResult.Cost);

            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=100,
                    WeightKg = 54
                }
            });

            singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Heavy, singleResult.Size);
            Assert.AreEqual(54, singleResult.Cost);
            result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm=1,
                    WidthCm=1,
                    DepthCm=100,
                    WeightKg = 49
                }
            });

            singleResult = result.Packages.Single();

            //Check package calculation
            Assert.AreEqual(PackageSize.Heavy, singleResult.Size);
            Assert.AreEqual(50, singleResult.Cost); //heavy parcel charge only
        }

        [TestMethod()]
        public void SmallParcelMania()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm = 1,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 1,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 1,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 1,
                    WidthCm = 1,
                    DepthCm = 1
                }
            });

            //Check package calculation
            Assert.AreEqual(3 * 4, result.Total);
            Assert.AreEqual(3 * 3, result.MultiParcelTotal);
            Assert.AreEqual(3 * 3 * 2, result.SpeedyShippingTotal);
        }

        [TestMethod()]
        public void MediumParcelMania()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm = 10,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 10,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 10,
                    WidthCm = 1,
                    DepthCm = 1
                }
            });

            //Check package calculation
            Assert.AreEqual(8 * 3, result.Total);
            Assert.AreEqual(8 * 2, result.MultiParcelTotal);
            Assert.AreEqual(8 * 2 * 2, result.SpeedyShippingTotal);
        }

        [TestMethod()]
        public void FifthParcelMania()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm = 50,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 50,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 50,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 50,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 50,
                    WidthCm = 1,
                    DepthCm = 1
                }
            });

            //Check package calculation
            Assert.AreEqual(15 * 5, result.Total);
            Assert.AreEqual(15 * 4, result.MultiParcelTotal);
            Assert.AreEqual(15 * 4 * 2, result.SpeedyShippingTotal);
        }

        [TestMethod()]
        public void ParcelMania10Parcels3Medium()
        {
            var result = CostCalculator.Calculate(new[]
            {
                new PackageDimensions
                {
                    HeightCm = 10,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 10,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 10,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 50,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 50,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 50,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 50,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 50,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 50,
                    WidthCm = 1,
                    DepthCm = 1
                },
                new PackageDimensions
                {
                    HeightCm = 50,
                    WidthCm = 1,
                    DepthCm = 1
                }
            });

            //Check package calculation
            Assert.AreEqual(8 * 3 + 15 * 7, result.Total);
            Assert.AreEqual(8 * 1 + 15 * 7, result.MultiParcelTotal);
            Assert.AreEqual((8 * 1 + 15 * 7) * 2, result.SpeedyShippingTotal);
        }
    }
}