using Microsoft.AspNetCore.Identity;
using WebProject2.Models;

namespace WebProject2.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<Client> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Client.ToString()));
        }


        public static async Task AdminSeedRolesAsync(UserManager<Client> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminUser = new Client
            {
                UserName = "admin",
                Email = "adminbindingcompany25@gmail.com",
                FirstName = "system",
                LastName = "admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != adminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(adminUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(adminUser, "123Password1$");
                    await userManager.AddToRoleAsync(adminUser, Enum.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(adminUser, Enum.Roles.Client.ToString());
                }
            }
        }

        public static async Task BuyerSeedRolesAsync(UserManager<Client> userManager, RoleManager<IdentityRole> roleManager)
        {
            var clientUser = new Client
            {
                UserName = "buyer",
                Email = "buyer@gmail.com",
                FirstName = "buyer",
                LastName = "buyer",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != clientUser.Id))
            {
                var user = await userManager.FindByEmailAsync(clientUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(clientUser, "123Password1$");
                    await userManager.AddToRoleAsync(clientUser, Enum.Roles.Client.ToString());
                }
            }
        }

        public static async Task SellerSeedRolesAsync(UserManager<Client> userManager, RoleManager<IdentityRole> roleManager)
        {
            
            var clientUser = new Client
            {
                UserName = "seller",
                Email = "seller@gmail.com",
                FirstName = "seller",
                LastName = "seller",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != clientUser.Id))
            {
                var user = await userManager.FindByEmailAsync(clientUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(clientUser, "123Password1$");
                    await userManager.AddToRoleAsync(clientUser, Enum.Roles.Client.ToString());
                }
            }

        }

    }
}
