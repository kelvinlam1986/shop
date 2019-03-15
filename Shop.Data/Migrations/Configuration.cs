namespace Shop.Data.Migrations
{
    using Common;
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
            CreateUserData();
            CreateFooterData(context);
            CreateSilderData(context);
            CreateProductCategoryData(context);
            CreateProductData(context);
            CreatePageData(context);
            CreateContactDetailsData(context);
        }

        private void CreateUserData()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ShopDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ShopDbContext()));
            if (!userManager.Users.Any())
            {
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
                    roleManager.Create(new ApplicationRole("User"));
                    roleManager.Create(new ApplicationRole("Admin"));

                    var adminUser = userManager.FindByEmail("kelvincoder@gmail.com");
                    userManager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
                }
            }
        }
        private void CreateProductCategoryData(ShopDbContext context)
        {
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
        }
        private void CreateProductData(ShopDbContext context)
        {
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
                    UpdatedDate = DateTime.Now,
                    Quantity = 1,
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
                    UpdatedDate = DateTime.Now,
                    Quantity = 1
                });

                context.Products.AddRange(productList);
                context.SaveChanges();
            }
        }
        private void CreateFooterData(ShopDbContext context)
        {
            if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId) == 0)
            {
                var footer = new Footer
                {
                    ID = CommonConstants.DefaultFooterId,
                    Content = SampleData.Footer
                };

                context.Footers.Add(footer);
                context.SaveChanges();
            }
        }
        private void CreateSilderData(ShopDbContext context)
        {
            if (!context.Slides.Any())
            {
                var listSlide = new List<Slide>
                {
                    new Slide { Name = "Slide 1", DisplayOrder = 1, Status = true, Url = "#", Image = "/Assets/client/images/bag.jpg", Description = SampleData.Slider1Description },
                    new Slide { Name = "Slide 2", DisplayOrder = 2, Status = true, Url = "#", Image = "/Assets/client/images/bag1.jpg", Description = SampleData.Slider2Description },
                };

                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreatePageData(ShopDbContext context)
        {
            if (!context.Pages.Any())
            {
                var page = new Page
                {
                    Name = "Giới thiệu",
                    Alias = "gioi-thieu",
                    Content = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    Status = true,
                    CreatedBy = "admin",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "admin",
                    UpdatedDate = DateTime.Now,
                    MetaKeywork = "Giới thiệu",
                    MetaDescription = "Giới thiệu"
                };

                context.Pages.Add(page);
                context.SaveChanges();
            }
        }

        private void CreateContactDetailsData(ShopDbContext context)
        {
            if (!context.ContactDetails.Any())
            {
                var contactDetail = new ContactDetail
                {
                    Name = "Shop thời trang",
                    Address = "240/2 Lê Thánh Tôn P.Bến Thành Q.1 TPHCM",
                    Email = "kelvincoder@gmail.com",
                    Lat = 10.7725631,
                    Lng = 106.6938348,
                    Phone = "0902305226",
                    Website = "myshop.com.vn",
                    Status = true,
                    Other = "Shop thời trang chuyên bán các sản phẩm thời trang mới nhất"
                };

                context.ContactDetails.Add(contactDetail);
                context.SaveChanges();
            }
        }
    }
}
