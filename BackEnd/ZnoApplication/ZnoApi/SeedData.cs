using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Zno.Server.Models;
using Zno.DAL.Context;
using Zno.DAL.Entities;
using Zno.DAL.Interfaces;
using Zno.Parser;

namespace Zno.Server
{
    public class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var context = provider.GetRequiredService<ApplicationDbContext>();
                var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = provider.GetRequiredService<ApplicationRoleManager>();
                var unitOfWork = provider.GetRequiredService<IUnitOfWork>();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var userAdmin = context.Users.FirstOrDefault(u => u.Email.Equals("admin@domain.com") ||
                                                                u.PhoneNumber.Equals("admin@domain.com"));

                        if (userAdmin is null)
                        {
                            await InitializeUsers(userManager, roleManager);
                        }

                        if (!context.QuestionTypes.Any())
                        {
                            var oneType = new QuestionType
                            {
                                Name = "C одним правильным ответом"
                            };

                            var manyType = new QuestionType
                            {
                                Name = "C несколькими правильным ответами"
                            };

                            var manualType = new QuestionType
                            {
                                Name = "С ручным вводом ответа"
                            };

                            var taskType = new QuestionType
                            {
                                Name = "С ручным вводом ответа (с ручной проверкой преподавателем)"
                            };

                            await context.QuestionTypes.AddRangeAsync(new[]
                            {
                                oneType, manyType,
                                manualType, taskType
                            });

                            await context.SaveChangesAsync();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }

                await new ZnoParser(unitOfWork).StartParsing();
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