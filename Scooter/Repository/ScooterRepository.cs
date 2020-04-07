using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beam_intern.Scooter.Repository
{
    public class ScooterRepository : IScooterRepository
    {
        private readonly ScooterDbContext _databaseContext;

        public ScooterRepository(ScooterDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        // All methods should map data models to domain models
        public Task<IEnumerable<Domain.ScooterDomainModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.ScooterDomainModel> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.ScooterDomainModel> Add(Guid id, double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.ScooterDomainModel> Update(Guid id, double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.ScooterDomainModel> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}