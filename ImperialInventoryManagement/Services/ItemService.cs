using ImperialInventoryManagement.Repos;
using ImperialInventoryManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace ImperialInventoryManagement.Services
{
    public class ItemService
    {
        private readonly IRepo<Item> _IRepo;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        private Item _item;
        private List<Item> _items;
        public ItemService(IConfiguration config, IRepo<Item> itemRepo)
        {
            _IRepo = itemRepo;
            _config = config;
        }


        /// <summary>
        /// Should return a String describing if the item is below the min value
        /// If it is not needing restocking return a string saying that and tell how many are stocked
        /// If there are none of the item in stock describe that in the string
        /// </summary>
        /*public bool IsStockedString(Item item)
        {
            if (item.Current < item.Min)
            {
                return false;
            }
            else
            {
                return true;
            }
        }*/

        /// <summary>
        /// Returns a boolean value telling if there is any of the item left in stock, returns true if there is no more of the item
        /// This should be used to validate if items are able to be added to cart by users.
        /// </summary>
        /// <returns></returns>
        /*public bool IsOutOfStock(Item item)
        {
            if (item.Current == 0)
            {
                return true;
            }
            else { return false; }
        }*/

        public Item GetItem(int id)
        {
            try
            {
                _item = _IRepo.Search(x => x.Id == id && !x.IsDeleted)
                    .Include(x => x.ItemCategories)
                    .FirstOrDefault() ?? new Item();

            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting the Item " + id, ex);
            }
            return _item;
        }

        public List<Item> GetItems()
        {
            try
            {
                _items = _IRepo.Search().Where(x => !x.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting all Items " + ex);
            }
            return _items;
        }

        public List<Item> GetRecycleBin()
        {
            try
            {
                _items = _IRepo.Search(x => x.IsDeleted == true)
                    .Include(x => x.ItemCategories)                   
                    .ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting the Item recycle bin " + ex);
            }
            return _items;
        }

        public void Add(Item item)
        {
            try
            {
                _IRepo.Add(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error adding item " + ex);
            }
        }

        public void Update(Item item)
        {
            try
            {

                Item existing = GetItem(item.Id);
                     if (existing != null)
                       {
                            _IRepo.Update(item);
                       }
                     else
                       {
                           _logger.LogWarning("Inventory item not found");
                       }
                }
            
            catch (Exception ex)
            {
                _logger.LogError("Error updating item " + item, ex);
            }
        }

        public void Remove(Item item)
        {
            try
            {
                _IRepo.Delete(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error removing the Item " + ex);
            }
        }

        public void Delete(Item item)
        {
            try
            {
                item.IsDeleted = true;
                _IRepo.Update(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting the item " + ex);
            }
        }

        public void UnDelete(Item item)
        {
            try
            {
                item.IsDeleted = false;
                _IRepo.Update(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error UnDeleting the item " + ex);
            }
        }


    }
}

