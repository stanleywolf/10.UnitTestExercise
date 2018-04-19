using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using _09.DateTime;

namespace UnitTests.DateTimeTests
{
    class MyDateTimeTests
    {
        private IDateTime time;

        [SetUp]
        public void TestInit()
        {
            this.time = new MyDateTime();
        }

        [Test]
        public void ReturnCurrentDate()
        {
            //arr
            var expectedTime = DateTime.Now.Date;
            //ass
            Assert.AreEqual(expectedTime, this.time.Now().Date);
        }

        [Test]
        public void AddDaysToTheLastOneOfTheMonth()
        {
            //arr
            var testDate = new DateTime(2000,10,31);
            var expectedDay = new DateTime(2000,11,1);
            //act
            testDate = testDate.AddDays(1);
            //ass
            Assert.AreEqual(expectedDay,testDate);
        }
    }
}
