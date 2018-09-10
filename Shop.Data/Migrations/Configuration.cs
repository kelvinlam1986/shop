namespace Shop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Shop.Data.ShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Shop.Data.ShopDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ShopDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ShopDbContext()));
            var user = new ApplicationUser
            {
                UserName = "admin",
                Email = "kelvincoder@gmail.com",
                BirthDate = new DateTime(1986, 10, 10),
                FullName = "Lam Su Minh",
            };

            userManager.Create(user, "12345678x@X");
            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole("User"));
                roleManager.Create(new IdentityRole("Admin"));
            }

            var adminUser = userManager.FindByEmail("kelvincoder@gmail.com");
            userManager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
    }
}