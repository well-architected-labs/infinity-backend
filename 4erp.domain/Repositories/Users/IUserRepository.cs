using _4erp.api.entities;
using _4erp.domain.VO;

namespace _4erp.domain.Ports;
public interface IUserRepository {
    Task<User?> FindByEmailAsync(string email);
    Task<User?> FindByIdAsync(Guid id);
    Task UpdateAsync(User user);
    Task SaveAsync(User entity);
}