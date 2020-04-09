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

        /**
         * Domain logic responsible for calculating distance and finding the closest few scooters to a given coordinate.
         * First, we get all Scooters that's available in the database at the current moment.
         * Next, we iterate through all the Scooters available and calculate their distance.
         * If the distance is smaller than the specified distance (termed radius),
         * the Scooter will be added to a List holding all nearest scooters.
         * At the same time, their distance and UUID will be saved in a List of Tuples, which will be sorted.
         * We will then find the UUID of scooters from the shortest distance first and add these Scooters to another
         * list (called constrainNearestScooters). constrainNearestScooters will then hold a
         * specific number of closest scooters to the given point and will be returned by this function.
         */
        public IEnumerable<ScooterDomainModel> GetClosest(ScooterClosestDomainModel scooterClosestDomainModel)
        {
            Coordinates centreCoordinate = scooterClosestDomainModel.CentreCoordinate;
            int nearestNumberOfScooters = scooterClosestDomainModel.NearestNumberOfScooters;
            int radius = scooterClosestDomainModel.Radius;
            
            IEnumerable<ScooterDomainModel> allScooters = _repository.GetAll().Result;
            List<ScooterDomainModel> allNearestScooters = new List<ScooterDomainModel>();
            List<ScooterDomainModel> constrainNearestScooters = new List<ScooterDomainModel>();
            List <Tuple<double, Guid>> distanceOfAllNearestScooters = new List<Tuple<double, Guid>>();
            
            foreach (ScooterDomainModel scooter in allScooters)
            {
                double distance = CoordinatesDistanceExtensions.GetDistance(
                    scooter.Longitude,
                    scooter.Latitude,
                    centreCoordinate.Longitude,
                    centreCoordinate.Latitude);
                if (!(distance <= radius)) continue;
                allNearestScooters.Add(scooter);
                distanceOfAllNearestScooters.Add(new Tuple<double, Guid>(distance, scooter.Id));
            }

            distanceOfAllNearestScooters.Sort((x,y) 
                                                            => x.Item1.CompareTo(y.Item1));

            while (nearestNumberOfScooters > 0 && distanceOfAllNearestScooters.Any())
            {
                var initialElement = distanceOfAllNearestScooters.First();
                ScooterDomainModel desiredScooter = allNearestScooters.Find(model => 
                    model.Id.Equals(initialElement.Item2));
                constrainNearestScooters.Add(desiredScooter);
                nearestNumberOfScooters--;
                distanceOfAllNearestScooters.Remove(initialElement);
            }
            return constrainNearestScooters;
        }
    }
}