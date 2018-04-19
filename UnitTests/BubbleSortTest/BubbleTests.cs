using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using _04.BubbleSort;

namespace UnitTests.BubbleSortTest
{
    class BubbleTests
    {
        [Test]
        [TestCase(2,1,5,3,4)]
        [TestCase(5,4,3,2,1)]
        public void BubbleCanSortNumbers(params int[] numbers)
        {
            //arrange
            var bubble = new Bubble();
            var sortedNumber = new int[] {1, 2, 3, 4, 5};
            //act
            bubble.Swap(numbers);
            //assert
            Assert.AreEqual(sortedNumber,numbers);
            //same 
            CollectionAssert.AreEqual(numbers,sortedNumber);
        }
    }
}
