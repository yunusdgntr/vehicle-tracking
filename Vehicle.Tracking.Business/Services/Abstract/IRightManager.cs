using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Tracking.Entities.Models;

namespace Vehicle.Tracking.Business.Services.Abstract
{
    public interface IRightManager
    {
        Right Add(Right entity);

        Right Update(Right entity);

        void Delete(Right entity);

        Right GetById(int id);

        List<Right> GetList();

        IEnumerable<Right> FindUserRoles(int userId);

        Task<bool> IsUserInRoleAsync(int userId, string roleName);

        Task<List<Right>> FindUsersInRoleAsync(string roleName);
    }
}
