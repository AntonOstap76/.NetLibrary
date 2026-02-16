using CommonRepositoryProject;
using DomainProject;

namespace PatentRepositoryProject;

public class PatentRepository : CommonEntityRepository<Patent>, IPatentRepository
{
    
}