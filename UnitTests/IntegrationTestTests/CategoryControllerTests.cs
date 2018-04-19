using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace UnitTests.IntegrationTestTests
{
    public class CategoryControllerTests
    {
        private CaregoryController categoryController;
        private HashSet<ICategory> categories;

        [SetUp]
        public void TestInit()
        {
            this.categoryController = new CaregoryController();
            this.categories = (HashSet<ICategory>) this.categoryController.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic).First(f => f.Name == "categories")
                .GetValue(this.categoryController);
        }

        [Test]
        public void AddCategorySaveTheCategory()
        {
            //arr
            var categoryName = "Stanislav";
            //act
            this.categoryController.AddCategory(categoryName);
            //ass
            Assert.AreEqual(1, this.categories.Count);
        }

        [Test]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(30)]
        public void AddMoreThanOneCategory(int numsOfCategories)
        {
            //arr
            var categoryName = "Nikola";
            //act
            for (int i = 0; i < numsOfCategories; i++)
            {
                this.categoryController.AddCategory($"{categoryName} - {i}");
            }
            //ass
            Assert.AreEqual(numsOfCategories, this.categories.Count);
        }

        [Test]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(30)]
        public void AddMultipleCategoriesAtOneShouldSaveAllOfThem(int numberOfCategories)
        {
            // Arrange
            var categoryNames = new string[numberOfCategories];
            for (int i = 0; i < categoryNames.Length; i++)
            {
                categoryNames[i] = $"Test - {i}";
            }

            // Act
            this.categoryController.AddCategory(categoryNames);

            // Assert
            Assert.AreEqual(numberOfCategories, this.categories.Count, "Add categories whith collection of names doesn't save them correctly");
        }

        [Test]
        public void AddCategoryWhitoutName()
        {
            //ass
            Assert.Throws<ArgumentException>(() => this.categoryController.AddCategory(""));
        }
        [Test]
        public void RemoveCategoryByNameShouldDeleteIt()
        {
            // Arrange
            var numberOfCategories = 10;
            var categoryname = "Test - {0}";
            for (int i = 0; i < numberOfCategories; i++)
            {
                this.categoryController.AddCategory(string.Format(categoryname, i));
            }

            // Act
            this.categoryController.RemoveCategory(string.Format(categoryname, 0));

            // Assert
            Assert.AreEqual(numberOfCategories - 1, this.categories.Count, "Remove doesn't delete the category");
        }
       
    }
}
