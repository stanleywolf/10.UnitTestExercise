using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using NUnit.Framework;
using _01.Database;

namespace UnitTests
{
    [TestFixture]
    public class Databasetests
    {
        private Database database;

        [SetUp]
        public void TestInit()
        {
            this.database = new Database();
        }

        [Test]
        [TestCase(new int[]{ 1, 2, 3, 4 })]
        [TestCase(new int[] { -10 })]
        [TestCase(new int[] { 4, 3, 2, 1 })]
        public void TestValidConstructor(int[] values)
        {
            Database db = new Database(values);

            FieldInfo fieldInfo = typeof(Database).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(f1 => f1.FieldType == typeof(int[]));
            
            int[] fieldValues = (int[])fieldInfo.GetValue(db);
            int[] buffer = new int[fieldValues.Length - values.Length];
            Assert.That(fieldValues,Is.EquivalentTo(values.Concat(buffer)));
        }
        [Test]
        public void TestInvalidConstructor()
        {
            int[] values = new int[17];

            Assert.That(() => new Database(values),Throws.InvalidOperationException);
        }

        [Test]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        [TestCase(-20)]
        [TestCase(0)]
        [TestCase(500)]
        public void TestAddMethodValid(int value)
        {
            Database db = new Database();
            db.Add(value);

            FieldInfo valuesInfo = typeof(Database).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(f1 => f1.FieldType == typeof(int[]));

            FieldInfo currentIndexinfo = typeof(Database).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(f1 => f1.FieldType == typeof(int));

            int firstValue = ((int[])valuesInfo.GetValue(db)).First();
            Assert.That(firstValue,Is.EqualTo(value));

            int valuesCount = (int) currentIndexinfo.GetValue(db);
            Assert.That(valuesCount,Is.EqualTo(1));
        }

        [Test]
        public void TestAddMethodInvalid()
        {
            Database db = new Database();

            FieldInfo currentIndexinfo = typeof(Database).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(f1 => f1.FieldType == typeof(int));

            currentIndexinfo.SetValue(db,16);

            Assert.That(() => db.Add(1),Throws.InvalidOperationException);
        }

        [Test]
        [TestCase(new int[] { 1, 2, 3, 4 })]
        [TestCase(new int[] { -10 })]
        [TestCase(new int[] { 4, 3, 2, 1 })]
        public void TestRemoveMethodInvalid(int[] values)
        {
            Database db = new Database();

            FieldInfo fieldInfo = typeof(Database).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(f1 => f1.FieldType == typeof(int[]));

            fieldInfo.SetValue(db,values);

            FieldInfo currentIndexinfo = typeof(Database).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(f1 => f1.FieldType == typeof(int));

            currentIndexinfo.SetValue(db,values.Length);

            db.Remove();

            int[] fieldValues = (int[])fieldInfo.GetValue(db);

            int[] buffer = new int[fieldValues.Length - (values.Length - 1)];

            values = values.Take(values.Length - 1).Concat(buffer).ToArray();
            Assert.That(fieldValues, Is.EquivalentTo(values));
        }

        [Test]
        public void TestRemoveInvalid()
        {
            Database db = new Database();

            FieldInfo currentIndexinfo = GetFieldInfo(db, typeof(int));

            currentIndexinfo.SetValue(db, 0);
            Assert.That(() => db.Remove(),Throws.InvalidOperationException);
        }

        [Test]
        [TestCase(3,1)]
        [TestCase(8,4)]
        public void TestFetchMethodValid(int valuesAdd,int  valuesRemove)
        {
            Database db = new Database();
            for (int i = 0; i < valuesAdd; i++)
            {
               db.Add(i);
            }
            for (int i = 0; i < valuesRemove; i++)
            {
                db.Remove();
            }
            var content = db.Fetch();

            for (int i = 0; i < valuesAdd - valuesRemove; i++)
            {
                Assert.AreEqual(i, content[i]);
            }

        }
        [Test]
        [TestCase(3, 1)]
        [TestCase(8, 4)]
        [TestCase(16, 10)]
        public void FetchReturnsCorrectLengthOfElementsFromDatabaseAfterRemoval(int valuesAdd, int valuesRemove)
        {
            Database db = new Database();
            // Act
            for (int i = 0; i < valuesAdd; i++)
            {
                db.Add(i);
            }
            for (int i = 0; i < valuesRemove; i++)
            {
                db.Remove();
            }
            var databaseContent = db.Fetch();

            // Assert
            var expectedNumberOfElements = valuesAdd - valuesRemove;
            Assert.AreEqual(expectedNumberOfElements, databaseContent.Length);
        }
        private FieldInfo GetFieldInfo(object instance, Type fieldType)
        {
            FieldInfo fieldInfo = instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(f1 => f1.FieldType == fieldType);

            return fieldInfo;
        }

    }
}
