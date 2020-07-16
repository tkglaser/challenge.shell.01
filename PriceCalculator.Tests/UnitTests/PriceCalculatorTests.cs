using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceCalculator.Models;
using Moq;
using Microsoft.Extensions.Options;

namespace PriceCalculator.Tests
{
    [TestClass]
    public class PriceCalculatorTests
    {
        private const string _expectedOffer1Description = "Apples 10% off";
        private const string _expectedOffer2Description = "Buy 2 cans of beans and get a loaf of bread half price";

        public Mock<IOptions<PriceCalculatorConfig>> GetMockConfig()
        {
            var mockConfig = new Mock<IOptions<PriceCalculatorConfig>>();
            mockConfig.Setup(x => x.Value).Returns(new PriceCalculatorConfig
            {
                Products = new Product[] {
                    new Product { Name = "Beans", PricePerUnit = 0.65M },
                    new Product { Name = "Bread", PricePerUnit = 0.80M },
                    new Product { Name = "Milk", PricePerUnit = 1.30M },
                    new Product { Name = "Apple", PricePerUnit = 1.00M },
                },
                Offers = new Offer[] {
                    new Offer { Condition = new Basket(new string[] {"Apple"}), Description = _expectedOffer1Description, PriceDelta = -0.1M },
                    new Offer { Condition = new Basket(new string[] {"Beans", "Beans", "Bread"}), Description = _expectedOffer2Description, PriceDelta=-0.4M }
                }
            });
            return mockConfig;
        }

        [TestMethod]
        public void Calculate_WithExample1_CreatesTheCorrectOutput()
        {
            var calculator = new PriceCalculator(GetMockConfig().Object);

            var result = calculator.Calculate(new string[] { "Apple", "Milk", "Bread" });
            Assert.AreEqual(3.10M, result.SubTotal);
            Assert.AreEqual(1, result.Offers.Count);
            Assert.AreEqual(_expectedOffer1Description, result.Offers[0].Description);
            Assert.AreEqual(-0.10M, result.Offers[0].PriceDelta);
            Assert.AreEqual(3.00M, result.Total);
        }

        [TestMethod]
        public void Calculate_WithExample2_CreatesTheCorrectOutput()
        {
            var calculator = new PriceCalculator(GetMockConfig().Object);

            var result = calculator.Calculate(new string[] { "Milk" });
            Assert.AreEqual(1.30M, result.SubTotal);
            Assert.AreEqual(0, result.Offers.Count);
            Assert.AreEqual(1.30M, result.Total);
        }

        [TestMethod]
        public void Calculate_WithBeanBreadExample_CreatesTheCorrectOutput()
        {
            var calculator = new PriceCalculator(GetMockConfig().Object);

            var result = calculator.Calculate(new string[] { "Beans", "Beans", "Bread" });
            Assert.AreEqual(2.10M, result.SubTotal);
            Assert.AreEqual(1, result.Offers.Count);
            Assert.AreEqual(_expectedOffer2Description, result.Offers[0].Description);
            Assert.AreEqual(-0.40M, result.Offers[0].PriceDelta);
            Assert.AreEqual(1.70M, result.Total);
        }

        [TestMethod]
        public void Calculate_WithBeanBreadExampleTimes2_CreatesTheCorrectOutput()
        {
            var calculator = new PriceCalculator(GetMockConfig().Object);

            var result = calculator.Calculate(new string[] { "Beans", "Beans", "Bread", "Beans", "Beans", "Bread" });
            Assert.AreEqual(4.20M, result.SubTotal);
            Assert.AreEqual(1, result.Offers.Count);
            Assert.AreEqual(_expectedOffer2Description, result.Offers[0].Description);
            Assert.AreEqual(-0.80M, result.Offers[0].PriceDelta);
            Assert.AreEqual(3.40M, result.Total);
        }

        [TestMethod]
        public void Calculate_WithEmptyInput_ReturnsZero()
        {
            var calculator = new PriceCalculator(GetMockConfig().Object);

            var result = calculator.Calculate(new string[] { });
            Assert.AreEqual(0.00M, result.SubTotal);
            Assert.AreEqual(0, result.Offers.Count);
            Assert.AreEqual(0.00M, result.Total);
        }

        [TestMethod]
        public void Calculate_WithUnknownProduct_Throws()
        {
            var calculator = new PriceCalculator(GetMockConfig().Object);
            Exception expectedException = null;

            try
            {
                var result = calculator.Calculate(new string[] { "MysteryProduct" });
            }
            catch (Exception x)
            {
                expectedException = x;
            }
            Assert.IsNotNull(expectedException);
            Assert.AreEqual("Unknown product [mysteryproduct]", expectedException.Message);
        }
    }
}