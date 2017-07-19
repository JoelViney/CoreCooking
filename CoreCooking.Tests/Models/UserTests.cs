using CoreCooking.Data;
using CoreCooking.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CoreCooking.Models
{
    [TestClass]
    public class UserTests
    {
        #region Helper Methods...

        public static UserRepository NewRepository()
        {
            var repository = new UserRepository(SettingsFactory.GetConnectionString(), "test-users");

            return repository;
        }

        public static async Task<User> AssureUserExistsAsync(UserRepository repository = null, string name = "Test")
        {
            if (repository == null)
                repository = NewRepository();

            var item = new User() { Name = name, Password = "password" };

            await repository.SaveAsync(item);
            return item;
        }

        public static async Task AssureUserDoesntExistsAsync(UserRepository repository = null, string name = "Test")
        {
            if (repository == null)
                repository = NewRepository();

            var item = await repository.FindByNameAsync(name);

            if (item != null)
                await repository.DeleteAsync(item);
        }

        #endregion


        [TestMethod]
        public async Task CreateUserAsync()
        {
            // Arrange
            var repository = NewRepository();
            await AssureUserDoesntExistsAsync(repository, "Test");

            // Act
            var item = new User() { Name = "Test", Password = "password" };
            await repository.SaveAsync(item);

            // Assert
            var item2 = await repository.FindAsync(item.Guid);
            Assert.AreEqual(item.Guid, item2.Guid);
            Assert.AreEqual("Test", item2.Name);
        }


        [TestMethod]
        public async Task UpdateAndGetUserAsync()
        {
            // Arrange
            var repository = NewRepository();
            var item = await AssureUserExistsAsync(repository, "Test");

            // Act
            var item2 = await repository.GetAsync(item.Guid);
            item2.Name = "Updated Name";
            await repository.SaveAsync(item2);

            // Assert
            var item3 = await repository.GetAsync(item.Guid);
            Assert.AreEqual("Updated Name", item3.Name);
        }

        [TestMethod]
        public async Task DeleteUserAsync()
        {
            // Arrange
            var repository = NewRepository();
            var item = await AssureUserExistsAsync(repository);

            // Act
            await repository.DeleteAsync(item);

            // Assert
            var item2 = await repository.FindAsync(item.Guid);
            Assert.IsNull(item2);
        }

        // Tests that a newly created user exists when calling GetList
        [TestMethod]
        public async Task GetUserListAsync()
        {
            // Arrange
            var repository = NewRepository();
            var item = await AssureUserExistsAsync(repository);

            // Act
            List<User> list = await repository.GetListAsync();

            // Assert
            Assert.IsTrue(list.Any(x => x.Guid == item.Guid));
        }

    }
}
