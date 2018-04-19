using System;
using System.Collections.Generic;
using System.Text;
using CustomLinkedList;
using NUnit.Framework;

namespace UnitTests.CustomLinkedListTests
{
    class DynamicListTests
    {
        private DynamicList<int> list;

        [SetUp]
        public void TestInit()
        {
            this.list = new DynamicList<int>();
        }

        [Test]
        public void CannotCallElementWhitNegativeIndex()
        {
            //arr
            var incorectIndex = -1;
            //ass
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var tesr = this.list[incorectIndex];
            });
        }

        [Test]
        public void CannotCallIndexOutOfRange()
        {
            //arr
            var incorectIndex = 55;
            //ass
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var test = this.list[incorectIndex];
            });
        }

        [Test]
        public void AddIncreaseCollCount()
        {
            //act
            this.list.Add(1);
            //ass
            Assert.AreEqual(1,this.list.Count);
        }
        [Test]
        [TestCase(1, 0)]
        [TestCase(5, 3)]
        [TestCase(10, 8)]
        [TestCase(15, 14)]
        public void AddShouldSaveTheElementInTheCollection(int numberOfAdditions, int indexToCheck)
        {
            // Act
            this.AddElements(numberOfAdditions);

            // Assert
            Assert.AreEqual(indexToCheck, this.list[indexToCheck], "Element is not the same as the one added");
        }
        [Test]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(-5)]
        [TestCase(5)]
        public void RemoveAtIndexOusideTheBoundariesOfTheCollectionIsImpossible(int indexToRemove)
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => this.list.RemoveAt(indexToRemove));
        }

        [Test]
        [TestCase(3, 1)]
        [TestCase(10, 5)]
        [TestCase(10, 0)]
        [TestCase(10, 8)]
        public void RemoveAtShouldRemoveTheElementAtTheGivenIndex(int numberOfAdditions, int indexToRemove)
        {
            //arr
            this.AddElements(numberOfAdditions);
            //act
            this.list.RemoveAt(indexToRemove);
            //ass
            Assert.AreEqual(indexToRemove + 1,this.list[indexToRemove]);
        }

        [Test]
        [TestCase(3, 1)]
        [TestCase(5, 4)]
        [TestCase(10, 7)]
        public void IndexOfShouldReturnTheIndexPointingAtTheCurrentLocationOfTheElement(int numberOfAdditions,
            int keyElement)
        {
            //arr
            this.AddElements(numberOfAdditions);
            //act
            var indexToFound = this.list.IndexOf(keyElement);
            //ass
            Assert.AreEqual(indexToFound, this.list[indexToFound]);
        }
        [Test]
        [TestCase(3, 3)]
        [TestCase(5, -1)]
        [TestCase(10, 15)]
        public void IndexOfShouldReturnNegativeIntegerIfTheGivenElementDoesNotExists(int numberOfAdditions, int keyElement)
        {
            // Arrange
            this.AddElements(numberOfAdditions);

            // Act
            var isReturnedValueLessThanZero = this.list.IndexOf(keyElement) < 0;

            // Assert
            Assert.IsTrue(isReturnedValueLessThanZero, "Returned index is not negative");
        }

        [Test]
        [TestCase(3, 1)]
        [TestCase(10, 5)]
        [TestCase(10, 0)]
        [TestCase(10, 8)]
        [TestCase(10, 9)]
        public void RemoveShouldDeleteParticularElement(int numberOfAdditions, int elementToRemove)
        {
            // Arrange
            this.AddElements(numberOfAdditions);

            // Act
            this.list.Remove(elementToRemove);

            // Assert
            Assert.AreEqual(-1, this.list.IndexOf(elementToRemove), "Removed element is still in the collection");
        }
        [Test]
        [TestCase(3, 1)]
        [TestCase(5, 4)]
        [TestCase(10, 7)]
        public void RemoveShouldReturnTheIndexOfTheRemovedElement(int numberOfAdditions, int elementToRemove)
        {
            // Arrange 
            this.AddElements(numberOfAdditions);
            var expectedIndex = elementToRemove;

            // Act
            var returnedIndex = this.list.Remove(elementToRemove);

            // Assert
            Assert.AreEqual(expectedIndex, returnedIndex, "Remove returns wrong index");
        }

        [Test]
        [TestCase(3, 1)]
        [TestCase(5, 4)]
        [TestCase(10, 7)]
        public void ContainsShouldReturnTrueIfElementExists(int numberOfAdditions, int keyElement)
        {
            // Arrange
            this.AddElements(numberOfAdditions);

            // Assert
            Assert.IsTrue(this.list.Contains(keyElement), "Contains returns false for existing element");
        }

        [Test]
        [TestCase(3, 3)]
        [TestCase(5, 10)]
        [TestCase(10, 15)]
        public void ContainsShouldReturnfalseIfElementDoesNotExists(int numberOfAdditions, int keyElement)
        {
            // Arrange
            this.AddElements(numberOfAdditions);

            // Assert
            Assert.IsFalse(this.list.Contains(keyElement), "Contains returns true for element which doesn't exists");
        }

        private void AddElements(int numberOfAdditions)
        {
            for (int i = 0; i < numberOfAdditions; i++)
            {
                this.list.Add(i);
            }
        }
    }
}
