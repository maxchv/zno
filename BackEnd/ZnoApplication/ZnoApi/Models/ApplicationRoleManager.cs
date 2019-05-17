using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace ZnoApi.Models
{
    public class ApplicationRoleManager
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationRoleManager(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(string role)
        {
            try
            {
                var adminRole = await _roleManager.FindByNameAsync(role);

                if (adminRole is null)
                    await _roleManager.CreateAsync(new IdentityRole { Name = role });
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}