using System;
using System.Threading.Tasks;
using Action.Common.Auth;
using Action.Services.Identity.Domain.Models;
using Action.Services.Identity.Domain.Repositories;
using Action.Services.Identity.Domain.Services;
using Action.Services.Identity.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Action.Services.Identity.Tests.Unit.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task user_service_login_should_return_jwt()
        {
            var email = "test@test.com";
            var password = "password";
            var name = "name";
            var salt = "salt";
            var hash = "hash";
            var token = "token";

            var userRepositoryMocked = new Mock<IUserRepository>();
            var encryptorMocked = new Mock<IEncrypter>();
            var jwtHandlerMocked = new Mock<IJwtHandler>();

            encryptorMocked.Setup(x => x.GetSalt()).Returns(salt);
            encryptorMocked.Setup(x => x.GetHash(password, salt)).Returns(hash);

            jwtHandlerMocked.Setup(x => x.Create(It.IsAny<Guid>())).Returns(new JsonWebToken()
            {
                Token = token
            });

            var user = new User(email, name);

            user.SetPassword(password, encryptorMocked.Object);
            userRepositoryMocked.Setup(x => x.GetAsync(email)).ReturnsAsync(user);

            var userService = new UserService(userRepositoryMocked.Object, encryptorMocked.Object, jwtHandlerMocked.Object);
            var jwt = await userService.LoginAsync(email, password);

            userRepositoryMocked.Verify(x => x.GetAsync(email), Times.Once);
            jwtHandlerMocked.Verify(x => x.Create(It.IsAny<Guid>()), Times.Once);

            jwt.Should().NotBeNull();
            jwt.Token.ShouldBeEquivalentTo(token);
        }
    }
}