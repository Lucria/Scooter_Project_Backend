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
            List<ScooterDomainModel> allNearestScooters = new List<ScooterDomainModel>();
            List<ScooterDomainModel> constrainNearestScooters = new List<ScooterDomainModel>();
            List <Tuple<double, Guid>> distanceOfScooters = new List<Tuple<double, Guid>>();
            foreach (ScooterDomainModel scooter in allScooters)
            {
                double distance = CoordinatesDistanceExtensions.GetDistance(
                    scooter.Longitude,
                    scooter.Latitude,
                    centreCoordinate.Longitude,
                    centreCoordinate.Latitude);
                if (!(distance <= radius)) continue;
                allNearestScooters.Add(scooter);
                distanceOfScooters.Add(new Tuple<double, Guid>(distance, scooter.Id));
            }

            distanceOfScooters.Sort((x,y) => x.Item1.CompareTo(y.Item1));

            while (nearestNumberOfScooters > 0 && distanceOfScooters.Any())
            {
                var initialElement = distanceOfScooters.First();
                ScooterDomainModel desiredScooter = allNearestScooters.Find(model => 
                    model.Id.Equals(initialElement.Item2));
                constrainNearestScooters.Add(desiredScooter);
                Console.WriteLine("Chosen UUID" + desiredScooter.Id);
                nearestNumberOfScooters--;
                distanceOfScooters.Remove(initialElement);
            }
            return constrainNearestScooters;
        }
    }
}