using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceCalculator.Models;

namespace PriceCalculator.Tests
{
    [TestClass]
    public class CalculationResultTests
    {
        [TestInitialize]
        public void Init()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);
        }

        [TestMethod]
        public void Ctor_WithNoParams_InitialisesWithExpectedValues()
        {
            var cResult = new CalculationResult();
            Assert.AreEqual(0.0M, cResult.SubTotal);
            Assert.AreEqual(0, cResult.Offers.Count);
            Assert.AreEqual(0.0M, cResult.Total);
        }

        [TestMethod]
        public void Add_WithValue_AddsToSubTotalAndTotal()
        {
            var cResult = new CalculationResult();
            cResult.Add(12.34M);
            Assert.AreEqual(12.34M, cResult.SubTotal);
            Assert.AreEqual(12.34M, cResult.Total);
        }

        [TestMethod]
        public void AddOffer_WithValidOffer_AddsToTotal()
        {
            var cResult = new CalculationResult();
            cResult.Add(12.34M);
            cResult.AddOffer(new Offer
            {
                Description = "TestOffer",
                PriceDelta = -1M

            }, -3M);
            Assert.AreEqual(12.34M, cResult.SubTotal);
            Assert.AreEqual(9.34M, cResult.Total);
        }

        [TestMethod]
        public void ToString_WithNoOffers_FormatsOutputCorrectly()
        {
            var cResult = new CalculationResult();
            cResult.Add(12.34M);
            Assert.AreEqual(
                "Subtotal: £12.34" + Environment.NewLine +
                "(No offers available)" + Environment.NewLine +
                "Total: £12.34" + Environment.NewLine, cResult.ToString());
        }

        [TestMethod]
        public void ToString_WithOffers_FormatsOutputCorrectly()
        {
            var cResult = new CalculationResult();
            cResult.Add(12.34M);
            cResult.AddOffer(new Offer
            {
                Description = "TestOffer",
                PriceDelta = -1M

            }, -3M);
            Assert.AreEqual(
                "Subtotal: £12.34" + Environment.NewLine +
                "TestOffer: -£3.00" + Environment.NewLine +
                "Total: £9.34" + Environment.NewLine, cResult.ToString());
        }

        [TestMethod]
        public void ToString_WithOffersInPence_FormatsOutputCorrectly()
        {
            var cResult = new CalculationResult();
            cResult.Add(12.34M);
            cResult.AddOffer(new Offer
            {
                Description = "TestOffer",
                PriceDelta = -0.01M

            }, -0.03M);
            Assert.AreEqual(
                "Subtotal: £12.34" + Environment.NewLine +
                "TestOffer: -3p" + Environment.NewLine +
                "Total: £12.31" + Environment.NewLine, cResult.ToString());
        }

        [TestMethod]
        public void ToString_WithOffersInPence2Digits_FormatsOutputCorrectly()
        {
            var cResult = new CalculationResult();
            cResult.Add(12.34M);
            cResult.AddOffer(new Offer
            {
                Description = "TestOffer",
                PriceDelta = -0.1M

            }, -0.3M);
            Assert.AreEqual(
                "Subtotal: £12.34" + Environment.NewLine +
                "TestOffer: -30p" + Environment.NewLine +
                "Total: £12.04" + Environment.NewLine, cResult.ToString());
        }
    }
}