using System.Threading.Tasks;
using Vehicle.Tracking.Business.Handlers.Authorizations.Queries;
using Vehicle.Tracking.Entities.Models;

namespace Vehicle.Tracking.Business.Services.Abstract
{
    public interface IUserManager
    {
        Task<User> AddAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task DeleteAsync(User entity);
        Task<User> GetAsync(LoginUserQuery query);
    }
}
