using Microsoft.AspNetCore.Identity;
using ImperialInventoryManagement.Models;

namespace ImperialInventoryManagement.Services
{
    public class UserService
    {
        private readonly UserManager<User> _UserManager;

        private readonly ILogger<UserService> _Logger;
        private User User;

        public UserService(UserManager<User> userManager,  ILogger<UserService> logger)
        {
            _UserManager = userManager;
            _Logger = logger;
            User = new User();

        }

        public async Task<User> GetUserAsync(String UserId)
        {
             User = await _UserManager.FindByIdAsync(UserId);
            if (User == null)
            {
                throw new ArgumentException("No user with the UserID " + UserId + " was found.");
            }
            return User;
        }

        public List<User> GetAllUsers()
        {
            return _UserManager.Users.ToList();
        }

        /// <summary>
        /// Rae Sloane can change user's permission roles
        ///Use an authorization check to verify correct permission level to use this method
        ///Use a drop down box that can only be seen by Sloane 
        ///Permissions for box are Auditor, Administrator, Manager, and regular user
        /// </summary>
        public void ChangeClaim()
        {
           
        }

        
    }
}
