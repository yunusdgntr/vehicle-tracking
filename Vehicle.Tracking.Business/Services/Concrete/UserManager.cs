using System;
using System.Threading.Tasks;
using Vehicle.Tracking.Business.Handlers.Authorizations.Queries;
using Vehicle.Tracking.Business.Services.Abstract;
using Vehicle.Tracking.DataAccess.Abstract;
using Vehicle.Tracking.Entities.Models;

namespace Vehicle.Tracking.Business.Services.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(LoginUserQuery query)
        {
            return await _userRepository.GetAsync(x => x.Email == query.Email && x.Password == query.Password);

        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
