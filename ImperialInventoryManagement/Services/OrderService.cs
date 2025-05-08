using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImperialInventoryManagement.Services
{
    public class OrderService
    {

        private readonly IRepo<Order> _IRepo;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly UserManager<Order> _userManager;
        private readonly ShipmentService _shipmentService;

        private Order _order;
        private List<Order> _orders;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shippingInformation"></param>
        /// <param name="orderId"></param>
        public void SetShippingInformation(String shippingInformation, int orderId)
        {

        }

      
          
            public OrderService(IRepo<Order> repo, IConfiguration config, ILogger<OrderService> ilogger)
            {
                _IRepo = repo;
                _config = config;
                _logger = ilogger;
                _order = new Order();
            }

            public Order GetOrder(int Id)
            {
                try
                {
                    _order = _IRepo.Search(x => x.Id == Id && !x.IsDeleted)
                    .Include(x => x.InventoryItem)
                        .FirstOrDefault() ?? new Order();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error getting Order with Id " + Id, ex);
                }
                return _order;
            }

            public List<Order> GetOrders()
            {
                try
                {
                    _orders = _IRepo.Search().Where(x => !x.IsDeleted).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error getting all Orders " + ex);
                }
                return _orders;
            }

            public List<Order> GetRecycleBin()
            {
                try
                {
                    _orders = _IRepo.Search(x => x.IsDeleted == true)
                        .Include(x => x.InventoryItem)
                        .ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error getting the Recylce Bin " + ex);
                }
                return _orders;
            }

            public void Add(Order order)
            {
                try
                {
                    _IRepo.Add(order);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error updating the Orders " + ex);
                }
            }

            public void Remove(Order order)
            {
                try
                {
                    _IRepo.Delete(order);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error removing the order" + ex);
                }
            }

            public void Delete(Order order)
            {
                try
                {
                    order.IsDeleted = true;
                    _IRepo.Update(order);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error deleting the Order " + ex);
                }
            }

            public void UnDelete(Order order)
            {
                try
                {
                    order.IsDeleted = false;
                    _IRepo.Update(order);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error UnDeleting the Order " + ex);
                }
            }



        }
    }




