using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beam_intern.Scooter.Domain;
using Beam_intern.Scooter.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<ScooterDomainModel>> GetAll()
        {
            var scooterDataModels = await _databaseContext.Scooters.ToListAsync();
            var scooterDomainModels = scooterDataModels.Select(ToScooterDomainModel);
            return scooterDomainModels;
        }

        public async Task<ScooterDomainModel> Get(Guid id)
        {
            var scooterDataModel = await _databaseContext.Scooters.FindAsync(id);
            ScooterDomainModel scooterDomainModel;
            if (scooterDataModel == null)
            {
                scooterDomainModel = null;
            }
            else
            {
                scooterDomainModel = ToScooterDomainModel(scooterDataModel);
            }

            return scooterDomainModel;
        }

        public async Task<ScooterDomainModel> Add(ScooterDomainModel scooterDomainModel)
        {
            ScooterDataModel scooterDataModel = ToScooterDataModel(scooterDomainModel);

            await _databaseContext.AddAsync(scooterDataModel);
            await _databaseContext.SaveChangesAsync();

            return await Get(scooterDomainModel.Id);
        }

        public async Task<ScooterDomainModel> Update(Guid id, double latitude, double longitude)
        {
            var scooterDataModel = await _databaseContext.Scooters.FindAsync(id);

            if (scooterDataModel == null)
            {
                return null;
            }

            scooterDataModel.Latitude = latitude;
            scooterDataModel.Longitude = longitude;

            await _databaseContext.SaveChangesAsync();
            return ToScooterDomainModel(scooterDataModel);
        }

        public async Task<ScooterDomainModel> Delete(Guid id)
        {
            var scooterDataModel = await _databaseContext.Scooters.FindAsync(id);
            if (scooterDataModel == null)
            {
                return null;
            }

            _databaseContext.Scooters.Remove(scooterDataModel);
            await _databaseContext.SaveChangesAsync();
            return ToScooterDomainModel(scooterDataModel);
        }

        private ScooterDomainModel ToScooterDomainModel(ScooterDataModel scooterDataModel)
        {
            return new ScooterDomainModel(
                scooterDataModel.Id, 
                scooterDataModel.Latitude, 
                scooterDataModel.Longitude);
        }

        private ScooterDataModel ToScooterDataModel(ScooterDomainModel scooterDomainModel)
        {
            return new ScooterDataModel
            {
                Id = scooterDomainModel.Id,
                Latitude = scooterDomainModel.Latitude,
                Longitude = scooterDomainModel.Longitude
            };
        }
    }
}