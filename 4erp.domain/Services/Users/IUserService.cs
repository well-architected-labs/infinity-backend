using _4erp.api.entities;
using _4erp.domain.VO;

namespace _4erp.domain.Services.Users;
public interface IUserService :  IGeneric<User>{
    Task<User?> FindByEmailAsync(string email);
    Task<User?> FindFirstAsync(string id);
    Task UpdateAsync(User entity);
}