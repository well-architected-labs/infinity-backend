using _4erp.api.entities.status;
using _4erp.domain.VO;

namespace _4erp.api.DT.Vacancies;
public class VacancyDT
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Generic? Ocupation { get; set; }
    public Status? Status { get; set; }
    public DateTime DateInit { get; set; }
    public DateTime DateEnd { get; set; }
    public Generic? Person { get; set; }
    public List<string>? Skills { get; set; }
}