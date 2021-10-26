using System;
using System.Linq;
using System.Threading.Tasks;
using Vehicle.Tracking.Business.Handlers.Authorizations.Queries;
using Vehicle.Tracking.Business.Services.Abstract;
using Vehicle.Tracking.DataAccess.Abstract;
using Vehicle.Tracking.Entities.Models;
using Vehicle.Tracking.Entities.Models.Request;
using Vehicle.Tracking.Entities.Models.Response;

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

        public UserResponse GetByFilter(UserRequest request)
        {
            var response = new UserResponse() { Entity = new User() };
            var query = _userRepository.GetAllInclude(null, new string[] { "Rights", "Rights.Role" });

            if (!string.IsNullOrEmpty(request.Filter.RefreshToken))
            {
                query = query.Where(x => x.RefreshToken == request.Filter.RefreshToken);
            }

            if (!string.IsNullOrEmpty(request.Filter.Email))
            {
                query = query.Where(x => x.Email == request.Filter.Email);
            }

            if (!string.IsNullOrEmpty(request.Filter.Password))
            {
                query = query.Where(x => x.Password == request.Filter.Password);
            }

            response.Entity = query.FirstOrDefault();


            return response;
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
