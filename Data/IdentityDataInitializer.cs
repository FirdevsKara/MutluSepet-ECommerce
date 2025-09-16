using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MutluSepet.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MutluSepet.Data
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            // UserManager ve RoleManager al
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Rol isimleri
            string adminRole = "Admin";
            string userRole = "User";

            // Roller yoksa oluştur
            if (!await roleManager.RoleExistsAsync(adminRole))
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            if (!await roleManager.RoleExistsAsync(userRole))
                await roleManager.CreateAsync(new IdentityRole(userRole));

            // Admin kullanıcısı oluştur (var mı diye kontrol et)
            string adminEmail = "admin@mutlusepet.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Site Admin",
                    EmailConfirmed = true // Admin için email doğrulaması tamam
                };

                var createResult = await userManager.CreateAsync(adminUser, "Admin123!");
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
                else
                {
                    var errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
                    throw new Exception("Admin kullanıcı oluşturulamadı: " + errors);
                }
            }
            else
            {
                // Kullanıcı zaten varsa, admin rolü yoksa ekle
                if (!await userManager.IsInRoleAsync(adminUser, adminRole))
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }

            // Optional: Eğer istersen buraya default normal kullanıcı da ekleyebilirsin
        }
    }
}
