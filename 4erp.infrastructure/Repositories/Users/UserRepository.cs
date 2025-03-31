using System.Linq.Expressions;
using System.Threading.Tasks;
using _4erp.api.entities;
using _4erp.domain.Ports;
using _4erp.domain.VO;
using _4erp.infrastructure.data.context;
using Microsoft.EntityFrameworkCore;

namespace _4erp.infrastructure.Repositories.Users;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly AppDBContext _context;

    public UserRepository(AppDBContext context) : base(context)
    {
        _context = context;
    }

    public int Count()
    {
        return _context.Users.Count();
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(c => c.Email != null && c.Email.Contains(email));

    }

    public async Task<User?> FindByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(v => v.Person)
            .Include(v => v.Role)
            .Include(v => v.Role.Scopes)
            .FirstOrDefaultAsync(c => c.Id.Equals(id));
    }

    public async Task SaveAsync(User entity)
    {

        _context.Phones.Entry(entity.Person.Phone).State = EntityState.Added;
        _context.Persons.Entry(entity.Person).State = EntityState.Added;
        _context.Bios.Entry(entity.Person.Bio).State = EntityState.Added;

        foreach (var include in entity.Person.Bio.Experiences)
            _context.Experiences.Entry(include).State = EntityState.Added;

        foreach (var include in entity.Person.Bio.Educations)
            _context.Educations.Entry(include).State = EntityState.Added;

        foreach (var include in entity.Person.Skills)
            _context.Skills.Entry(include).State = EntityState.Added;

        _context.Roles.Entry(entity.Role).State = EntityState.Unchanged;

        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User entity)
    {
        var experiencesInDataBase = _context.Experiences
            .Where(c => c.BioId.Equals(entity.Person.Bio.Id))
            .ToList();

        var entityExperienceIds = entity.Person.Bio.Experiences.Select(e => e.Id).ToList();
        var dbExperienceIds = experiencesInDataBase.Select(e => e.Id).ToList();

        var experiencesToAdd = entity.Person.Bio.Experiences
            .Where(e => !dbExperienceIds.Contains(e.Id))
            .ToList();

        var experiencesToRemove = experiencesInDataBase
            .Where(e => !entityExperienceIds.Contains(e.Id))
            .ToList();

        var experiencesToUpdate = experiencesInDataBase
            .Where(e => entityExperienceIds.Contains(e.Id))
            .ToList();

        foreach (var include in experiencesToRemove)
            _context.Experiences.Entry(include).State = EntityState.Deleted;

        foreach (var include in experiencesToAdd)
            _context.Experiences.Entry(include).State = EntityState.Added;

        foreach (var include in experiencesToUpdate)
            _context.Experiences.Entry(include).State = EntityState.Modified;



        var educationInDataBase = _context.Educations
            .Where(c => c.BioId.Equals(entity.Person.Bio.Id))
            .ToList();

        var entityEducationsIds = entity.Person.Bio.Educations.Select(e => e.Id).ToList();
        var dbEducationsIds = educationInDataBase.Select(e => e.Id).ToList();

        var educationsToAdd = entity.Person.Bio.Educations
            .Where(e => !dbExperienceIds.Contains(e.Id))
            .ToList();

        var educationsToRemove = educationInDataBase
            .Where(e => !entityExperienceIds.Contains(e.Id))
            .ToList();

        var educationsToUpdate = educationInDataBase
            .Where(e => entityExperienceIds.Contains(e.Id))
            .ToList();

        foreach (var include in educationsToRemove)
            _context.Educations.Entry(include).State = EntityState.Deleted;

        foreach (var include in educationsToAdd)
            _context.Educations.Entry(include).State = EntityState.Added;

        foreach (var include in educationsToUpdate)
            _context.Educations.Entry(include).State = EntityState.Modified;



        foreach (var include in entity.Person.Skills)
            _context.Skills.Attach(include).State = EntityState.Unchanged;

        _context.Roles.Entry(entity.Role).State = EntityState.Unchanged;
        _context.Persons.Entry(entity.Person).State = EntityState.Modified;
        _context.Bios.Entry(entity.Person.Bio).State = EntityState.Modified;
        _context.Phones.Entry(entity.Person.Phone).State = EntityState.Modified;
        _context.Persons.Entry(entity.Person).State = EntityState.Modified;

        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }
}