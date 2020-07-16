using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriceCalculator.Models;

namespace PriceCalculator.Tests
{
    [TestClass]
    public class BasketTests
    {
        [TestMethod]
        public void Ctor_WithNoParams_IsAvailable()
        {
            var basket = new Basket();

            // no crash
        }

        [TestMethod]
        public void Ctor_WithStringArray_CreatesCorrectLowercaseDictionary()
        {
            var items = new string[] { "item1", "Item2", "item2", "Item3" };
            var basket = new Basket(items);

            Assert.AreEqual(3, basket.Keys.Count);
            CollectionAssert.AreEqual(new string[] { "item1", "item2", "item3" }, basket.Keys.ToArray());
            CollectionAssert.AreEqual(new int[] { 1, 2, 1 }, basket.Values.ToArray());
        }

        [TestMethod]
        public void Ctor_WithOtherbasket_CreatesACopy()
        {
            var items = new string[] { "item1", "Item2", "item2", "Item3" };
            var basket = new Basket(items);
            var copy = new Basket(basket);

            CollectionAssert.AreEqual(copy, basket);
        }

        [TestMethod]
        public void FitsTimes_WithNonMatchingProduct_ReturnsZero()
        {
            var basket = new Basket(new string[] { "item1", "Item2", "item2", "Item3" });
            var otherBasket = new Basket(new string[] { "item1", "item4" });

            var result = basket.FitsTimes(otherBasket);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void FitsTimes_WithEmptyOtherBasket_ReturnsZero()
        {
            var basket = new Basket(new string[] { "item1", "Item2", "item2", "Item3" });
            var otherBasket = new Basket(new string[] { });

            var result = basket.FitsTimes(otherBasket);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void FitsTimes_WithEmptyBaskets_ReturnsZero()
        {
            var basket = new Basket(new string[] { });
            var otherBasket = new Basket(new string[] { });

            var result = basket.FitsTimes(otherBasket);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void FitsTimes_WithSingleFit_ReturnsOne()
        {
            var basket = new Basket(new string[] { "item1", "Item2", "item2", "Item3" });
            var otherBasket = new Basket(new string[] { "Item1" });

            var result = basket.FitsTimes(otherBasket);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void FitsTimes_WithDoubleFit_ReturnsTwo()
        {
            var basket = new Basket(new string[] { "item1", "Item1", "Item2", "item2", "Item3" });
            var otherBasket = new Basket(new string[] { "Item1", "item2" });

            var result = basket.FitsTimes(otherBasket);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void SubtractTimes_WithFittingbasket_Subtracts()
        {
            var basket = new Basket(new string[] { "item1", "Item1", "Item2", "item2", "Item3" });
            var otherBasket = new Basket(new string[] { "Item1", "item2" });

            basket.SubtractTimes(otherBasket, 2);

            Assert.AreEqual(1, basket.Keys.Count);
            CollectionAssert.AreEqual(new string[] { "item3" }, basket.Keys.ToArray());
            CollectionAssert.AreEqual(new int[] { 1 }, basket.Values.ToArray());
        }
    }
}
