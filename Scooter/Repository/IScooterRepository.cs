using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beam_intern.Scooter.Repository
{
    public interface IScooterRepository
    {
        Task<IEnumerable<Domain.ScooterDomainModel>> GetAll();
        Task<Domain.ScooterDomainModel> Get(Guid id);
        Task<Domain.ScooterDomainModel> Add(Guid id, double latitude, double longitude);
        Task<Domain.ScooterDomainModel> Update(Guid id, double latitude, double longitude);
        Task<Domain.ScooterDomainModel> Delete(Guid id);
    }
}