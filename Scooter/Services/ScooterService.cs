using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beam_intern.Scooter.Domain;
using Beam_intern.Scooter.Repository;

namespace Beam_intern.Scooter.Services
{
    public class ScooterService : IScooterService
    {
        private readonly IScooterRepository _repository;


        public ScooterService(IScooterRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ScooterDomainModel>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<ScooterDomainModel> Get(Guid id)
        {
            return _repository.Get(id);
        }

        public Task<ScooterDomainModel> Add(ScooterDomainModel scooterDomainModel)
        {
            return _repository.Add(scooterDomainModel);
        }

        public Task<ScooterDomainModel> Update(Guid id, double latitude, double longitude)
        {
            return _repository.Update(id, latitude, longitude);
        }

        public Task<ScooterDomainModel> Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public Task<IEnumerable<ScooterDomainModel>> GetClosest(ScooterClosestDomainModel scooterClosestDomainModel)
        {
            throw new NotImplementedException();
        }
    }
}