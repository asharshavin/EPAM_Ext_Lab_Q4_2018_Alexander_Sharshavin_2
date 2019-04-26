using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL2Messenger;
using System.Configuration;

namespace DAL2MessengerTest
{
    [TestClass]
    public class DAL2MessengerTest
    {
        private const string MessengerConnectionString = "MessengerConection";
        private UserRepository testRepo;

        public DAL2MessengerTest()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings[MessengerConnectionString];
            testRepo = new UserRepository(connectionStringItem);
        }

        [TestMethod]
        public void TestSave()
        {
            // Arrange
            var name1String = "aplha";
            var name2String = "beta";

            var entity1 = new User() { id = 1, name = name1String };
            var entity2 = new User() { id = 2, name = name2String };

            testRepo.Save(entity1);
            testRepo.Save(entity2);

            // Act
            var name1 = testRepo.Get(1).name;
            var name2 = testRepo.Get(2).name;

            // Assert
            // ki. я бы вообще переопределил метод equal у объекта пользователя и прям сравнивал бы данные с заранее определенными полностью
            // на реальном проекте такой вот тест не прокатил бы
            Assert.AreEqual(name1, name1String, string.Format("Save: имя пользователя с id {0} '{1}' не соответствует имени {2}", 1, name1, name1String));
            Assert.AreEqual(name2, name2String, string.Format("Save: имя пользователя с id {0} '{1}' не соответствует имени {2}", 2, name2, name2String));
        }

        [TestMethod]
        public void TestDelete()
        {
            // Arrange
            var name1String = "aplha";
            var name2String = "beta";

            testRepo.Save(new User() { id = 1, name = name1String });
            testRepo.Save(new User() { id = 2, name = name2String });

            // Act
            testRepo.Delete(1);

            // Assert
            // ki. тут согласен, всё хорошо
            Assert.AreEqual(testRepo.Get(1).id, 0, string.Format("Delete: Не удален пользователь с id 1. Получено {0}, ожидается {1}", testRepo.Get(1).id, 0));
        }

        [TestMethod]
        public void TestGet()
        {
            // Arrange
            var name1String = "aplha";
            var name2String = "beta";
            
            testRepo.Save(new User() { id = 1, name = name1String });
            testRepo.Save(new User() { id = 2, name = name2String });

            // Act
            var resultName = testRepo.Get(2).name;

            // Assert
            // ki. я бы вообще переопределил метод equal у объекта пользователя и прям сравнивал бы данные с заранее определенными полностью
            // на реальном проекте такой вот тест не прокатил бы
            Assert.AreEqual<string>(resultName, name2String, string.Format("Get: Некорректное имя пользователя с id 2. Получено {0}, ожидается {1}", resultName, name2String));

        }

        [TestMethod]
        public void TestGetAll()
        {
            // Arrange
            var name1String = "aplha";
            var name2String = "beta";

            testRepo.Save(new User() { id = 1, name = name1String });
            testRepo.Save(new User() { id = 2, name = name2String });

            var resultString = new StringBuilder();

            // Act
            foreach (var element in testRepo.GetAllUsers(5))//testRepo.GetAll())
            {
                testRepo.Delete(element.id); 
            }
             
            // Assert
            // ki. это точно не релевантный тест. Надо проверить, что не только количество выбралось правильно, но и выбралсь именно те данные. 
            Assert.AreEqual(testRepo.GetAll().Count, 0, string.Format("GetAll: Некорректное количество пользователей в списке. Получено {0}, ожидается {1}", testRepo.GetAll().Count, 0));
        }
    }
}
