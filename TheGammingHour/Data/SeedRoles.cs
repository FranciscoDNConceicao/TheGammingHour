using Microsoft.AspNetCore.Identity;

namespace UTAD.LAB._2022.TheGammingHour.Data
{
    public static class SeedRoles
    {
        public static void Seed(RoleManager<IdentityRole> roleManager)
        {

            if(roleManager.Roles.Any() == false)
            {
                roleManager.CreateAsync(new IdentityRole("Admnistrador")).Wait();
                roleManager.CreateAsync(new IdentityRole("Cliente")).Wait();
            }
            
        }
    }
}
