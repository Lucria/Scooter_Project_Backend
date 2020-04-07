using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beam_intern.Scooter.Domain;

namespace Beam_intern.Scooter.Services
{
    public interface IScooterService
    {
        Task<IEnumerable<ScooterDomainModel>> GetAll();
        Task<ScooterDomainModel> Get(Guid id);
        Task<ScooterDomainModel> Add(ScooterDomainModel scooterDomainModel);
        Task<ScooterDomainModel> Update(Guid id, double latitude, double longitude);
        Task<ScooterDomainModel> Delete(Guid id);
    }
}