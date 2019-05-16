using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZnoApi.Models;
using ZnoModelLibrary.EF;
using ZnoModelLibrary.Entities;

namespace ZnoApi
{
    public class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var context = provider.GetRequiredService<ApplicationContext>();
                var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = provider.GetRequiredService<ApplicationRoleManager>();

                var userAdmin = context.Users.FirstOrDefault(u => u.Email.Equals("admin@domain.com") || u.PhoneNumber.Equals("admin@domain.com"));

                if (userAdmin is null)
                {
                    await InitializeUsers(userManager, roleManager);
                }

                ZnoParser.ZnoParser parser = new ZnoParser.ZnoParser(context);
                // FIXME: Раскомментить когда будет полностью готов парсер
                //parser.StartParsing();
            }
        }

        private async static Task InitializeUsers(UserManager<ApplicationUser> userManager, ApplicationRoleManager roleManager)
        {
            const string PHONE_NUMBER = "+ 38 (000) 000-00-00";
            const string EMAIL = "admin@domain.com";
            const string USERNAME = "Admin";
            const string PASSWORD = "QwertY123@";
            const string ROLE = "Admin";

            var admin = new ApplicationUser
            {
                PhoneNumber = PHONE_NUMBER,
                PhoneNumberConfirmed = true,
                UserName = USERNAME,
                Email = EMAIL,
                EmailConfirmed = true,
            };

            IdentityResult result = await userManager.CreateAsync(admin, PASSWORD);

            if (result.Succeeded)
            {
                await roleManager.CreateRoleAsync(ROLE);
                await userManager.AddToRoleAsync(admin, ROLE);
            }
            else
            {
                throw new Exception("Default users can not create!!!");
            }
        }
    }
}