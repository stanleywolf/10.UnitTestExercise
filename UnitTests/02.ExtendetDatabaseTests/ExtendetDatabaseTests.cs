using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using _01.Database;

namespace UnitTests._02.ExtendetDatabaseTests
{
    [TestFixture]
    class ExtendetDatabaseTests
    {
        private ExDatabase database;

        [SetUp]
        public void TestInit()
        {
            this.database = new ExDatabase();
        }

        [Test]
        public void TestInitConstructor()
        {
            //arrange
            var firstPeople = new Person(123L, "first");
            var secondPeople = new Person(234L, "second");
            var thirdPeople = new Person(12598L, "third");
            var allPeople = new IPerson[] {firstPeople, secondPeople, thirdPeople};
            //act
            this.database = new ExDatabase(allPeople);

            //assert
            Assert.AreEqual(3,this.database.Count, $"Constructor doesn't work with {typeof(IPerson)} as parameter");
        }
        [Test]
        public void DatabaseInitializeConstructorWithNullLeadsToEmptyDb()
        {
            // Assert
            Assert.DoesNotThrow(() => this.database = new ExDatabase(null));
        }

        [Test]
        public void DatabaseAddPerson()
        {
            //arrange
            var person = new Person(444L,"Gosho");
            //act
            this.database.Add(person);
            //assert
            Assert.AreEqual(1,this.database.Count);
        }
        [Test]
        [TestCase(1L, "1L", 1L, "1L")]
        [TestCase(1L, "1L", 10L, "1L")]
        [TestCase(1L, "1L", 1L, "10L")]
        public void CanNotAddPersonWithAlreadyExistingUsernameOrId(long firstId, string firstUsername, long secondId, string secondUsername)
        {
            // Arrange
            var firstPerson = new Person(firstId, firstUsername);
            var secondPerson = new Person(secondId, secondUsername);

            // Act
            this.database.Add(firstPerson);

            // Assert
            Assert.Throws<InvalidOperationException>(() => this.database.Add(secondPerson));
        }

        [Test]
        public void RemovePersonFromDatabase()
        {
            //arrange
            var firstPerson = new Person(1L,"First");
            var secondPerson = new Person(2L,"Second");
            var thirdPerson = new Person(2, "Second");
            this.database.Add(firstPerson);
            this.database.Add(secondPerson);
            //act
            this.database.Remove(thirdPerson);
            this.database.Remove(firstPerson);
            //assert
            Assert.AreEqual(0,this.database.Count,$"Remove {typeof(IPerson)} doesn't work");
        }
        [Test]
        [TestCase(1L, "1L", 2L, "2L", 3L, "3L", 1L)]
        [TestCase(1L, "1L", 2L, "2L", 3L, "3L", 2L)]
        [TestCase(1L, "1L", 2L, "2L", 3L, "3L", 3L)]
        public void FindById(long firstId, string firstUsername, long secondId, string secondUsername, long thirdId, string thirdUsername, long keyId)
        {
            // Arrange
            this.database.Add(new Person(firstId, firstUsername));
            this.database.Add(new Person(secondId, secondUsername));
            this.database.Add(new Person(thirdId, thirdUsername));

            // Act
            var foundPerson = this.database.Find(keyId);

            // Assert
            Assert.AreEqual(keyId, foundPerson.Id, $"Found {typeof(IPerson)} by Id is not the desired one");
        }
        [Test]
        [TestCase(1L, "1L", 2L, "2L", 3L, "3L", "1L")]
        [TestCase(1L, "1L", 2L, "2L", 3L, "3L", "2L")]
        [TestCase(1L, "1L", 2L, "2L", 3L, "3L", "3L")]
        public void FindByUsername(long firstId, string firstUsername, long secondId, string secondUsername, long thirdId, string thirdUsername, string keyUsername)
        {
            // Arrange
            this.database.Add(new Person(firstId, firstUsername));
            this.database.Add(new Person(secondId, secondUsername));
            this.database.Add(new Person(thirdId, thirdUsername));

            // Act
            var foundPerson = this.database.Find(keyUsername);

            // Assert
            Assert.AreEqual(keyUsername, foundPerson.Username, $"Found {typeof(IPerson)} by Username is not the desired one");
        }
        [Test]
        public void CannotFindUnexistentId()
        {
            // Arrange 
            this.database.Add(new Person(1, "First"));

            // Assert
            Assert.Throws<InvalidOperationException>(() => this.database.Find(2));
        }

        [Test]
        public void CannotFindNegativeId()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => this.database.Find(-1));
        }

        [Test]
        public void CannotFindUnexistentUsername()
        {
            // Arrange 
            this.database.Add(new Person(1, "First"));

            // Assert
            Assert.Throws<InvalidOperationException>(() => this.database.Find("fiRst"));
        }

        [Test]
        public void CannotFindUsernameNull()
        {
            // Arrange 
            this.database.Add(new Person(1, "First"));

            // Assert
            Assert.Throws<ArgumentNullException>(() => this.database.Find(null));
        }
    }
}
