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
        
        /**
         * Read operation
         * Retrieves the data of all scooters available in the database.
         */
        public async Task<IEnumerable<ScooterDomainModel>> GetAll()
        {
            var scooterDataModels = await _databaseContext.Scooters.ToListAsync();
            var scooterDomainModels = scooterDataModels.Select(ToScooterDomainModel);
            return scooterDomainModels;
        }

        /**
         * Read operation
         * One of the 4 main operations under CRUD.
         * Reads the data for a specific scooter using its UUID
         */
        public async Task<ScooterDomainModel> Get(Guid id)
        {
            var scooterDataModel = await _databaseContext.Scooters.FindAsync(id);
            var scooterDomainModel = scooterDataModel == null ? null : ToScooterDomainModel(scooterDataModel);

            return scooterDomainModel;
        }

        /**
         * Create operation
         * One of the 4 main operations under CRUD
         * Adds a new Scooter object to the database
         */
        public async Task<ScooterDomainModel> Add(ScooterDomainModel scooterDomainModel)
        {
            ScooterDataModel scooterDataModel = ToScooterDataModel(scooterDomainModel);

            await _databaseContext.AddAsync(scooterDataModel);
            await _databaseContext.SaveChangesAsync();

            return await Get(scooterDomainModel.Id);
        }

        /**
         * Update operation
         * One of the 4 main operations under CRUD
         * Updates a Scooter with new latitude and longitude.
         */
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

        /**
         * Delete operation
         * One of the 4 main operations under CRUD
         * Deletes a Scooter via the UUID.
         */
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

        /**
         * Method responsible for converting a Data model to a Domain model.
         * Data models are used for communication between the Repository and the Databases (through ORM)
         * Domain models are used for communication between the Repository and the Domain layer.
         */
        private ScooterDomainModel ToScooterDomainModel(ScooterDataModel scooterDataModel)
        {
            return new ScooterDomainModel(
                scooterDataModel.Id, 
                scooterDataModel.Latitude, 
                scooterDataModel.Longitude);
        }

        /**
         * Method responsible for converting a Domain model to a Data model.
         * Data models are used for communication between the Repository and the Databases (through ORM)
         * Domain models are used for communication between the Repository and the Domain layer.
         */
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