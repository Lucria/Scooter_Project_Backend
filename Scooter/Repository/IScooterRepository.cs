using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beam_intern.Scooter.Repository
{
    public interface IScooterRepository
    {
        Task<IEnumerable<Domain.Scooter>> GetAll();
        Task<Domain.Scooter> Get(Guid id);
        Task<Domain.Scooter> Add(Guid id, double latitude, double longitude);
        Task<Domain.Scooter> Update(Guid id, double latitude, double longitude);
        Task<Domain.Scooter> Delete(Guid id);
    }
}