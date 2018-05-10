using Action.Common.Auth;
using Action.Common.Exceptions;
using Action.Services.Identity.Domain.Models;
using Action.Services.Identity.Domain.Repositories;
using Action.Services.Identity.Domain.Services;
using System.Threading.Tasks;

namespace Action.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
                throw new ActionException("invalid_credentials", "Invalid Credentials");

            if (!user.ValidatePassword(password, _encrypter))
                throw new ActionException("invalid_credentials", "Invalid Credentials");

            return _jwtHandler.Create(user.Id);
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
                throw new ActionException("email_in_use", $"Email {email} is already in use");

            user = new User(email, name);
            user.SetPassword(password, _encrypter);

            await _userRepository.AddAsync(user);
        }
    }
}