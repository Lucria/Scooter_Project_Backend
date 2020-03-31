using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beam_intern.Scooter.Repository
{
    public class ScooterRepository : IScooterRepository
    {
        public Task<IEnumerable<Domain.Scooter>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Scooter> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Scooter> Add(Guid id, double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Scooter> Update(Guid id, double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Scooter> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}