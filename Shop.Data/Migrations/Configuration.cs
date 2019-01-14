namespace Shop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
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

            if (!context.ProductCategories.Any())
            {
                var productCategoryList = new List<ProductCategory>();
                productCategoryList.Add(new ProductCategory { Name = "Điện lạnh", Alias = "dien-lanh", Status = true });
                productCategoryList.Add(new ProductCategory { Name = "Viễn thông", Alias = "vien-thong", Status = true });
                productCategoryList.Add(new ProductCategory { Name = "Đồ gia dụng", Alias = "do-gia-dung", Status = true });
                productCategoryList.Add(new ProductCategory { Name = "Mỹ phẩm", Alias = "my-pham", Status = true });
                context.ProductCategories.AddRange(productCategoryList);
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                var productList = new List<Product>();
                productList.Add(new Product
                {
                    Name = "Máy lạnh",
                    Alias = "may-lanh",
                    CategoryID = 1,
                    Price = 2000000,
                    Status = true,
                    CreatedBy = "admin",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "admin",
                    UpdatedDate = DateTime.Now
                });
                productList.Add(new Product
                {
                    Name = "Ti vi",
                    Alias = "ti-vi",
                    CategoryID = 3,
                    Price = 4000000,
                    Status = true,
                    CreatedBy = "admin",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "admin",
                    UpdatedDate = DateTime.Now
                });

                context.Products.AddRange(productList);
                context.SaveChanges();
            }
        }
    }
}
