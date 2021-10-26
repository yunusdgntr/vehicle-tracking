using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicle.Tracking.Business.Services.Abstract;
using Vehicle.Tracking.DataAccess.Abstract;
using Vehicle.Tracking.Entities.Models;

namespace Vehicle.Tracking.Business.Services.Concrete
{
    public class RightManager:IRightManager
    {
        IRightRepository _repository;
        public RightManager(IRightRepository repository)
        {
            _repository = repository;
        }

        public Right Add(Right entity)
        {
            return _repository.Add(entity);
        }


        public void Delete(Right entity)
        {
            _repository.Delete(entity);
        }

        public Right GetById(int id)
        {
            return _repository.Get(x => x.Id == id);
        }

        public List<Right> GetList()
        {
            return _repository.GetList().ToList();
        }

        public Right Update(Right entity)
        {
            return _repository.Update(entity);
        }

   

        public async Task<IEnumerable<Right>> FindUserRolesAsync(int userId)
        {
            return await _repository.GetListAsync(x=>x.UserId==userId);
        }

        public Task<bool> IsUserInRoleAsync(int userId, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Right>> FindUsersInRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
