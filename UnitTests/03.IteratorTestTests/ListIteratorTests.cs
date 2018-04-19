using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace UnitTests._03.IteratorTestTests
{
   public class ListIteratorTests
   {
        private ListIterator listIterator;
        private string[] initializingCollection;

        [SetUp]
        public void TestInit()
        {
            this.initializingCollection = new string[] { "qwe", "asd", "zxc" };
            this.listIterator = new ListIterator(this.initializingCollection);
        }

        [Test]
        public void InitializationConstructorCannotWorkWithNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ListIterator(null));
        }

        [Test]
        public void MoveReturnsTrueWhenSuccessful()
        {
            // Assert
            Assert.AreEqual(true, this.listIterator.Move());
            Assert.AreEqual(true, this.listIterator.Move());
        }

        [Test]
        public void MoveReturnsFalseWhenThereIsNoMoreElements()
        {
            // Act
            this.listIterator.Move();
            this.listIterator.Move();

            // Assert
            Assert.AreEqual(false, this.listIterator.Move());
        }

        [Test]
        public void MoveMovesTheInternalIndexToTheNextElementInTheCollection()
        {
            // Act
            this.listIterator.Move();
            var internalIndexValue = typeof(ListIterator)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .First(f => f.Name == "Index")
                .GetValue(this.listIterator);

            // Assert
            Assert.AreEqual(1, internalIndexValue, "Move doesn't influence the internal index");
        }

        [Test]
        public void HasNextReturnsTrueIfThereIsNextElement()
        {
            // Act
            this.listIterator.Move();

            // Assert
            Assert.IsTrue(this.listIterator.HasNext());
        }

        [Test]
        public void HasNextReturnsFalseIfThereIsNotNextElement()
        {
            // Act
            this.listIterator.Move();
            this.listIterator.Move();

            // Assert
            Assert.IsFalse(this.listIterator.HasNext());
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void PrintReturnsCurrentElement(int elementToreturn)
        {
            // Act
            for (int i = 0; i < elementToreturn; i++)
            {
                this.listIterator.Move();
            }

            // Assert
            Assert.AreEqual(this.initializingCollection[elementToreturn], this.listIterator.Print());
        }

        [Test]
        public void CannotPrintWithEmptyIterator()
        {
            // Arrange
            this.listIterator = new ListIterator(new string[0]);

            // Assert
            
            Assert.AreEqual("Invalid Operation!", this.listIterator.Print());
        }
    }
}
