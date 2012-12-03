/// User service tests
/// Codeora 2012
///

namespace ServiceTests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Prestige.Services;
    using Prestige.Repositories;
    using Moq;
    using FluentAssertions;
    using Prestige.DB.Models;

    [TestClass]
    public class UserServiceTests
    {
        /// <summary>
        /// Gets or sets the service being tested.
        /// </summary>
        /// <value>
        /// The service being tested.
        /// </value>
        public IUserService Service { get; set; }

        /// <summary>
        /// Gets or sets the mock user repo.
        /// </summary>
        /// <value>
        /// The mock user repo.
        /// </value>
        public Mock<IUserRepository> UserRepo { get; set; }

        /// <summary>
        /// Gets or sets the mock role repo.
        /// </summary>
        /// <value>
        /// The mock role repo.
        /// </value>
        public Mock<IRoleRepository> RoleRepo { get; set; }

        /// <summary>
        /// Gets or sets the encryptor.
        /// </summary>
        /// <value>
        /// The encryptor.
        /// </value>
        public Mock<IEncryptor> Encryptor { get; set; }

        /// <summary>
        /// Initializes the test suite.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this.UserRepo = new Mock<IUserRepository>();
            this.RoleRepo = new Mock<IRoleRepository>();
            this.Encryptor = new Mock<IEncryptor>();
            this.Encryptor.Setup(e => e.GetHash(It.IsAny<string>()))
                .Returns<string>(s => s);
            this.Service = new UserService(
                    this.UserRepo.Object,
                    this.RoleRepo.Object,
                    this.Encryptor.Object);
        }

        /// <summary>
        /// Tests for invalid constructor arguments.
        /// </summary>
        [TestMethod]
        public void InvalidConstructorTest()
        {
            // act
            Action act1 = () => new UserService(null, this.RoleRepo.Object, this.Encryptor.Object);
            Action act2 = () => new UserService(this.UserRepo.Object, null, this.Encryptor.Object);
            Action act3 = () => new UserService(this.UserRepo.Object, this.RoleRepo.Object, null);

            // assert
            act1.ShouldThrow<ArgumentNullException>();
            act2.ShouldThrow<ArgumentNullException>();
            act3.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Test for a valid constructor call.
        /// </summary>
        [TestMethod]
        public void ValidConstructorTest()
        {
            // setup
            IUserService service = null;

            // act
            Action act = () =>
                service =
                    new UserService(
                        this.UserRepo.Object,
                        this.RoleRepo.Object,
                        this.Encryptor.Object);

            // assert
            act.ShouldNotThrow();
            service.Should().NotBeNull();
            service.Should().BeOfType<UserService>();
        }

        /// <summary>
        /// Tests the add functionality.
        /// </summary>
        [TestMethod]
        public void TestAdd()
        {
            // setup
            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "test_user_1",
                Password = "i am a password!"
            };

            var list = new List<User>();
            this.UserRepo.Setup(r => r.Add(It.IsAny<User>()))
                .Callback<User>(u => list.Add(u))
                .Verifiable();
            this.UserRepo.Setup(r => r.SaveChanges())
                .Verifiable();

            // act
            Action act = () => this.Service.Add(user);

            // assert
            act.ShouldNotThrow();
            list.Count.Should().Be(1);
            list.Should().Contain(user);
            this.UserRepo.VerifyAll();
        }

        /// <summary>
        /// Tests the change password functionality.
        /// </summary>
        [TestMethod]
        public void PasswordChangeTest()
        {
            // setup
            var id = Guid.NewGuid();
            var oldPass = "i am old!";
            var newPass = "i am new!";
            var user = new User()
            {
                Id = id,
                UserName = "test_user_1",
                Password = oldPass
            };

            var list = new List<User>() { user };
            this.UserRepo.SetupIQueryable(list.AsQueryable());
            this.UserRepo.Setup(r => r.SaveChanges())
                .Verifiable();

            // act
            Action act = () => this.Service.ChangePassword(id, newPass);

            // assert
            act.ShouldNotThrow();
            list.Count.Should().Be(1);
            list.First().Password.Should().Be(newPass);
        }
    }
}
