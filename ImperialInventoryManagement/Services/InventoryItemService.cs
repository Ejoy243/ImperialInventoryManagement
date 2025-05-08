using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Repos;
using Microsoft.EntityFrameworkCore;

namespace ImperialInventoryManagement.Services
{
    public class InventoryItemService
    {
        private readonly IRepo<InventoryItem> _repo;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        private InventoryItem _inventoryItem;
        private List<InventoryItem> _inventoryItems;

        public InventoryItemService(IRepo<InventoryItem> repo, IConfiguration config, ILogger<InventoryItemService> iLogger)
        {
            _repo = repo;
            _config = config;
            _logger = iLogger;
            _inventoryItem = new InventoryItem();
            _inventoryItems = new List<InventoryItem>();
        }

/*        public bool IsAvailable CheckFacilityForItem(int facilityId, int itemId)
        {
            
           
                return true;
            
        }*/
        public List<InventoryItem> GetItemsForFacility(int facilityId)
        {
            try
            {
                _inventoryItems = _repo.Search().Where(x => !x.IsDeleted && (x.FacilityId == facilityId)).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError("Error getting Inventory Items with FacilityId: " + facilityId, ex);
            }
            return _inventoryItems;
        }

        public InventoryItem GetFromItemAndFacil(int facilityId, int itemId)
        {
            try
            {
                    _inventoryItem = _repo.Search()
                    .Include(x => x.Item)
                    .Include(x => x.Facility)
                    .FirstOrDefault(x => (x.FacilityId == facilityId) && (x.ItemId == itemId))
                    ?? new InventoryItem
                    {
                        FacilityId = facilityId,
                        ItemId = itemId,
                        ItemAmount = 0
                    };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting InventoryItem with facilityId " + facilityId + " and itemId " +  itemId, ex);
            }
            return _inventoryItem;
        }
        public InventoryItem GetInventoryItem(int Id)
        {
            try
            {
                _inventoryItem = _repo.Search(x => x.Id == Id && !x.IsDeleted)
                    .Include(x => x.Item)
                    .Include(x => x.Facility)
                    .FirstOrDefault() ?? new InventoryItem();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error gettinng Inventory Item with Id: " + Id, ex);
            }
            return _inventoryItem;

        }

        public List<InventoryItem> GetInventoryItems()
        {
            try
            {
                _inventoryItems = _repo.Search().Where(x => !x.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting all Inventory Items " + ex);
            }
            return _inventoryItems;
        }

        public void Update(InventoryItem invItem)
        {
            try
            {
                var existing = GetInventoryItem(invItem.Id);
                if(existing != null)
                {
                    _repo.Update(invItem);
                }
                else
                {
                    _logger.LogWarning("Inventory item not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating inventory Item " + ex);
            }
        }


        public void Add(InventoryItem item)
        {
            try
            {
                _repo.Add(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding Inventory Item " + ex);
            }
        }
        public void Remove(InventoryItem item)
        {
            try
            {
                _repo.Delete(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error removing inventory item " + ex);
            }
        }

        public void Delete(InventoryItem item)
        {
            try
            {
                item.IsDeleted = true;
                _repo.Update(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting inventory item " + ex);
            }
        }

        public void UnDelete(InventoryItem item)
        {
            try
            {
                item.IsDeleted = false;
                _repo.Update(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error undeleting inventory item " + ex);
            }
        }
    }



           
}
