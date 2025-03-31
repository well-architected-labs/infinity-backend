using System.Linq.Expressions;
using System.Threading.Tasks;
using _4erp.api.entities;
using _4erp.domain.Ports;
using _4erp.domain.repositories;
using _4erp.domain.Services.Users;
using _4erp.domain.VO;

namespace _4erp.application.Inbound.Users;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IGenericRepository<User> _repository;

    private readonly IGenericRepository<Role> _roleRepository;

    public UserService(
        IUserRepository userRepository,
        IGenericRepository<User> repository,
        IGenericRepository<Role> roleRepository
        )
    {
        _userRepository = userRepository;
        _repository = repository;
        _roleRepository = roleRepository;
    }

    public async void Remove(Guid id)
    {
        var userFound = await _repository.FindFirstAsync(
            c => c.Id.ToString() != null && c.Id.Equals(id))
            ?? throw new Exception("Não foi possível remover usuário! O ID não corresponde a nenhum usuário!");

        _repository.Remove(userFound);
    }

    public void Update(User entity)
    {
        _repository.Update(entity);
    }

    public async Task UpdateAsync(User entity)
    {
        await _userRepository.UpdateAsync(entity);
    }

    public async Task<int> CountAsync()
    {
        return await _repository.CountAsync();
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _userRepository.FindByEmailAsync(email);
    }

    public async Task<List<User>> GetAllAsync(int skip, int take)
    {
        return await _repository.GetAllAsync(skip, take);
    }

    public async Task<List<User>> GetAllAsync(
        int skip, int take, Expression<Func<User, bool>> predicate)
    {
        return await _repository.GetAllAsync(skip, take, predicate);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _userRepository.FindByIdAsync(id);
    }

    public async Task AddAsync(User entity)
    {
        entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
        await _userRepository.SaveAsync(entity);
    }


    public async Task<User?> FindFirstAsync(string id)
    {
        return await _repository.FindByFieldAsync(
            c => c.Id.Equals(Guid.Parse(id)),
            n => n.Person,
            n => n.Person.Phone,
            n => n.Person.Bio,
            n => n.Person.Bio.Experiences,
            n => n.Person.Bio.Educations,
            n => n.Person.Skills,
            n => n.Role,
            n => n.Role.Scopes
            );
    }
}