using _4erp.api.entities.candidature;
using _4erp.domain.VO;

namespace _4erp.domain.Services.Candidatures;

public interface ICandidatureService : IGeneric<Candidature>
{
    Task<List<Candidature>> GetAllByCurrentTenantByParams(
        int skip = 0,
        int take = 20,
        string? statusCandidature = null,
        string? vacancyStatus = null
    );

    Task<List<Candidature>> GetAllSubscribed(
        string id,
        int skip = 0,
        int take = 20
    );

    Task UpdateAttachAsync(Candidature entity);
}

