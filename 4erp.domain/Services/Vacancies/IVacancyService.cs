using _4erp.api.entities.vacancy;
using _4erp.domain.VO;

namespace _4erp.domain.Services.Vacancies;
public interface IVacancyService : IGeneric<Vacancy>
{
    Task<List<Vacancy>> GetAllAsync(
        int skip,
        int take,
        string? title = null,
        DateTime? dateInit = null,
        DateTime? dateEnd = null,
        List<string>? skills = null,
        string? ocupation = null,
        string? status = null
    );
}