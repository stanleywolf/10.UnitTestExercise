using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnitTests._02.ExtendetDatabaseTests
{
   public class PersonTests
    {
        [Test]
        public void TestValidConstructor()
        {
            //Arange
            var people = new Person(123,"Test");
            //Assert
            Assert.AreNotEqual(null, people);
            Assert.AreEqual(123,people.Id);
            Assert.AreEqual("Test",people.Username);
        }

        [Test]
        public void AllSetersIsNotPublic()
        {
            //arrange
            var personType = typeof(Person);
            var propWhithPublicSetters = personType.GetProperties().Where(p => p.SetMethod.IsPublic).ToArray();

            //assert
            Assert.AreEqual(0,propWhithPublicSetters.Length);
        }
    }
}
