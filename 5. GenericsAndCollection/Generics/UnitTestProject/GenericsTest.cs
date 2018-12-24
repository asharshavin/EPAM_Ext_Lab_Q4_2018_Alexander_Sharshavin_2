﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Generics;

namespace UnitTestProject
{
    [TestClass]
    public class GenericsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var MyRepository = new Repository();
            MyRepository.Save(new User() { name = "alpha" });
            MyRepository.Save(new User() { name = "beta" });

            // Act
            var MyList = MyRepository.GetAll();

            // Assert
            Assert.AreEqual(MyList.Count, 2, "GetAll: Количество элементов не равно 2");

        }
        [TestMethod]
        public void TestMethod2()
        {
            // Arrange
            var myResult = false;
            var MyRepository = new Repository();
            MyRepository.Save(new User() { name = "alpha" });
            MyRepository.Save(new User() { name = "beta" });

            // Act
            myResult  = MyRepository.Delete(0);
            var MyList = MyRepository.GetAll();

            // Assert
            Assert.AreEqual<long>(MyList.Count, 1, "Delte: Количество элементов не равно 1");

        }

        [TestMethod]
        public void TestMethod3()
        {
            // Arrange
            var MyRepository = new Repository();
            MyRepository.Save(new User() { name = "alpha" });
            MyRepository.Save(new User() { name = "beta" });

            // Act
            var myName = MyRepository.Get(0).name;

            // Assert
            Assert.AreEqual<string>(myName, "beta", "Get: Некорректное имя пользователя");

        }
    }
}