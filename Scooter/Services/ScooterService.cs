using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beam_intern.Scooter.CoordinatesHelper;
using Beam_intern.Scooter.Domain;
using Beam_intern.Scooter.Repository;
using Microsoft.EntityFrameworkCore.Metadata;

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

        public IEnumerable<ScooterDomainModel> GetClosest(ScooterClosestDomainModel scooterClosestDomainModel)
        {
            Coordinates centreCoordinate = scooterClosestDomainModel.CentreCoordinate;
            int nearestNumberOfScooters = scooterClosestDomainModel.NearestNumberOfScooters;
            int radius = scooterClosestDomainModel.Radius;
            IEnumerable<ScooterDomainModel> allScooters = _repository.GetAll().Result;
            IEnumerable<ScooterDomainModel> allNearestScooters = new List<ScooterDomainModel>();
            foreach (ScooterDomainModel scooter in allScooters)
            {
                Coordinates scooterCoordinate = new Coordinates(scooter.Latitude, scooter.Longitude);
                Console.WriteLine("ID is " + scooter.Id);
                Console.WriteLine("Latitude is " + scooter.Latitude);
                Console.WriteLine("Calculated Distance is");
                Console.WriteLine(CoordinatesDistanceExtensions.GetDistance(
                    scooterCoordinate.Longitude,
                    scooterCoordinate.Latitude,
                    centreCoordinate.Longitude,
                    centreCoordinate.Latitude));
                if (!(CoordinatesDistanceExtensions.GetDistance(
                    scooterCoordinate.Longitude,
                    scooterCoordinate.Latitude,
                    centreCoordinate.Longitude,
                    centreCoordinate.Latitude) <= radius) || nearestNumberOfScooters <= 0) continue;
                allNearestScooters.Append(scooter);
                nearestNumberOfScooters--;
            }
            return allNearestScooters;
        }
    }
}