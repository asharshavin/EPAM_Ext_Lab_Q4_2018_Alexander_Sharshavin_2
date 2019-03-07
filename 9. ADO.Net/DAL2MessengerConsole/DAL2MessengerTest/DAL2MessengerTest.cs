using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL2MessengerConsole;
using System.Configuration;

namespace DAL2MessengerTest
{
    [TestClass]
    public class DAL2MessengerTest
    {
        private const string MessengerConnectionString = "MessengerConection";
        UserRepository MyRepository;

        public DAL2MessengerTest()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings[MessengerConnectionString];
            MyRepository = new UserRepository(connectionStringItem);

        }

        [TestMethod]
        public void TestSave()
        {
            // Arrange
            var name1String = "aplha";
            var name2String = "beta";

            MyRepository.Save(new User() { id = 1, name = name1String });
            MyRepository.Save(new User() { id = 2, name = name2String });

            // Act
            var name1 = MyRepository.Get(1).name;
            var name2 = MyRepository.Get(2).name;

            // Assert
            Assert.AreEqual(name1, name1String, String.Format("Save: имя пользователя с id {0} '{1}' не соответствует имени {2}", 1, name1, name1String));
            Assert.AreEqual(name2, name2String, String.Format("Save: имя пользователя с id {0} '{1}' не соответствует имени {2}", 2, name2, name2String));

        }
         
        [TestMethod]
        public void TestDelete()
        {
            // Arrange
            var name1String = "aplha";
            var name2String = "beta";

            MyRepository.Save(new User() { id = 1, name = name1String });
            MyRepository.Save(new User() { id = 2, name = name2String });

            // Act
            MyRepository.Delete(1);

            // Assert
            Assert.AreNotEqual(MyRepository.Get(1).id, 0, "Delete: Не удален пользователь с id 1");

        }

        [TestMethod]
        public void TestGet()
        {
            // Arrange
            var name1String = "aplha";
            var name2String = "beta";

            MyRepository.Save(new User() { id = 1, name = name1String });
            MyRepository.Save(new User() { id = 2, name = name2String });

            // Act
            var resultName = MyRepository.Get(2).name;

            // Assert
            Assert.AreEqual<string>(resultName, name2String, String.Format("Get: Некорректное имя пользователя с id 2. Получено {0}, ожидается {1}", resultName, name2String));

        }

        [TestMethod]
        public void TestGetAll()
        {
            // Arrange
            var name1String = "aplha";
            var name2String = "beta";

            MyRepository.Save(new User() { id = 1, name = name1String });
            MyRepository.Save(new User() { id = 2, name = name2String });

            var resultString = new StringBuilder();

            // Act
            foreach(var element in MyRepository.GetAll())
            {
                MyRepository.Delete(element.id); 
            }
             
            // Assert
            Assert.AreNotEqual(MyRepository.GetAll().Count, 0, String.Format("GetAll: Некорректное количество пользователей в списке. Получено {0}, ожидается {1}", MyRepository.GetAll().Count, 0));

        }
    }
}
