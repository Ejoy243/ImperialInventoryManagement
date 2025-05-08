using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Repos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ImperialInventoryManagement.Services
{
    public class FacilityService
    {
        private readonly IRepo<Facility> _IRepo;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        private Facility _facility;
        private List<Facility> _facilities;

        public FacilityService(IRepo<Facility> repo, IConfiguration config, ILogger<FacilityService> ilogger)
        {
            _IRepo = repo;
            _config = config;
            _logger = ilogger;
            _facility = new Facility();
        }


        /// <summary>
        /// check to see if facility is holding inventory
        /// to be used to delete faility if it has no inventory
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return true;
        }

        public Facility GetFacility(int Id)
        {
            try
            {
                _facility = _IRepo.Search(x => x.Id == Id && !x.IsDeleted)
                    .Include(x => x.InventoryItems)
                    .FirstOrDefault() ?? new Facility();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting Imperial Facility with Id " + Id, ex);
            }
            return _facility;
        }

        public List<Facility> GetFacilities()
        {
            try
            {
                _facilities = _IRepo.Search().Where(x => !x.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting all Facilities " + ex);
            }
            return _facilities;
        }

        public List<Facility> GetRecycleBin()
        {
            try
            {
                _facilities = _IRepo.Search(x => x.IsDeleted == true)
                    .Include(x => x.InventoryItems)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting the Recylce Bin " + ex);
            }
            return _facilities;
        }

        public void Add(Facility facility)
        {
            try
            {
                _IRepo.Add(facility);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating the Facilities " + ex);
            }
        }

        public void Remove(Facility facility)
        {
            try
            {
                _IRepo.Delete(facility);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error removing the facility" + ex);
            }
        }

        public void Delete(Facility facility)
        {
            try
            {
                facility.IsDeleted = true;
                _IRepo.Update(facility);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting the facility " + ex);
            }
        }

        public void UnDelete(Facility facility)
        {
            try
            {
                facility.IsDeleted = false;
                _IRepo.Update(facility);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error UnDeleting the Facility " + ex);
            }
        }

    }

}
