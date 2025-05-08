using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Repos;
using Microsoft.EntityFrameworkCore;

namespace ImperialInventoryManagement.Services
{
    public class ShipmentService
    {
        private readonly IRepo<Shipment> _IRepo;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        private Shipment _shipment;
        private List<Shipment> _shipments;

        public ShipmentService(IRepo<Shipment> repo, IConfiguration config, ILogger<ShipmentService> ilogger)
        {
            _IRepo = repo;
            _config = config;
            _logger = ilogger;
            _shipment = new Shipment();
        }


    
        public String GenTrackId()
        {
            return new Random().Next().ToString();
        }


        public Shipment GetShipment(int Id)
        {
            try
            {
                _shipment = _IRepo.Search(x => x.Id == Id && !x.IsDeleted)
                    .Include(x => x.CreatedBy)
                    .Include(x => x.Order)
                    .FirstOrDefault() ?? new Shipment();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting Shipment with Id " + Id, ex);
            }
            return _shipment;
        }

        public List<Shipment> GetShipments()
        {
            try
            {
                _shipments = _IRepo.Search().Where(x => !x.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting all Shipments " + ex);
            }
            return _shipments;
        }

        public List<Shipment> GetRecycleBin()
        {
            try
            {
                _shipments = _IRepo.Search(x => x.IsDeleted == true)
                    .Include(x => x.CreatedBy)
                    .Include(x => x.Order)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting the Recylce Bin " + ex);
            }
            return _shipments;
        }

        public void Add(Shipment shipment)
        {
            try
            {
                shipment.TrackingID = GenTrackId();
                _IRepo.Add(shipment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating the Shipments " + ex);
            }
        }

        public void Update(Shipment shipment)
        {
            try
            {
               Shipment existing = GetShipment(shipment.Id);
               if (existing != null)
               {
                   _IRepo.Update(shipment);
               }
               else
               {
                   _logger.LogWarning("Shipment not found");
               }
            }

            catch (Exception ex)
            {
               _logger.LogError("Error updating shipment " + shipment, ex);
            }
           
        }

        public void Remove(Shipment shipment)
        {
            try
            {
                _IRepo.Delete(shipment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error removing the shipment" + ex);
            }
        }

        public void Delete(Shipment shipment)
        {
            try
            {
                shipment.IsDeleted = true;
                _IRepo.Update(shipment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting the Shipment " + ex);
            }
        }

        public void UnDelete(Shipment shipment)
        {
            try
            {
                shipment.IsDeleted = false;
                _IRepo.Update(shipment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error UnDeleting the Shipment " + ex);
            }
        }



    }
}

