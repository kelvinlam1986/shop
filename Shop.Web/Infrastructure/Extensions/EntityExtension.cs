﻿using Shop.Model.Models;
using Shop.Web.Models;
using System;

namespace Shop.Web.Infrastructure.Extensions
{
    public static class EntityExtension
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVM)
        {
            postCategory.ID = postCategoryVM.ID;
            postCategory.Name = postCategoryVM.Name;
            postCategory.Alias = postCategoryVM.Alias;
            postCategory.Description = postCategoryVM.Description;
            postCategory.ParentID = postCategoryVM.ParentID;
            postCategory.Image = postCategoryVM.Image;
            postCategory.HomeFlag = postCategoryVM.HomeFlag;
            postCategory.CreatedDate = postCategoryVM.CreatedDate;
            postCategory.CreatedBy = postCategoryVM.CreatedBy;
            postCategory.UpdatedDate = postCategoryVM.UpdatedDate;
            postCategory.UpdatedBy = postCategoryVM.UpdatedBy;
            postCategory.MetaKeywork = postCategoryVM.MetaKeywork;
            postCategory.MetaDescription = postCategoryVM.MetaDescription;
            postCategory.Status = postCategoryVM.Status;
        }

        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVM)
        {
            productCategory.ID = productCategoryVM.ID;
            productCategory.Name = productCategoryVM.Name;
            productCategory.Alias = productCategoryVM.Alias;
            productCategory.Description = productCategoryVM.Description;
            productCategory.ParentID = productCategoryVM.ParentID;
            productCategory.DisplayOrder = productCategoryVM.DisplayOrder;
            productCategory.Image = productCategoryVM.Image;
            productCategory.HomeFlag = productCategoryVM.HomeFlag;
            productCategory.CreatedDate = productCategoryVM.CreatedDate;
            productCategory.CreatedBy = productCategoryVM.CreatedBy;
            productCategory.UpdatedDate = productCategoryVM.UpdatedDate;
            productCategory.UpdatedBy = productCategoryVM.UpdatedBy;
            productCategory.MetaKeywork = productCategoryVM.MetaKeywork;
            productCategory.MetaDescription = productCategoryVM.MetaDescription;
            productCategory.Status = productCategoryVM.Status;
        }

        public static void UpdatePost(this Post post, PostViewModel postVM)
        {
            post.ID = postVM.ID;
            post.Name = postVM.Name;
            post.Alias = postVM.Alias;
            post.Description = postVM.Description;
            post.CategoryID = postVM.CategoryID;
            post.Image = postVM.Image;
            post.Content = postVM.Content;
            post.HomeFlag = postVM.HomeFlag;
            post.HotFlag = postVM.HotFlag;
            post.ViewCount = postVM.ViewCount;
            post.CreatedDate = postVM.CreatedDate;
            post.CreatedBy = postVM.CreatedBy;
            post.UpdatedDate = postVM.UpdatedDate;
            post.UpdatedBy = postVM.UpdatedBy;
            post.MetaKeywork = postVM.MetaKeywork;
            post.MetaDescription = postVM.MetaDescription;
            post.Status = postVM.Status;
        }

        public static void UpdateProduct(this Product product, ProductViewModel productVm)
        {
            product.ID = productVm.ID;
            product.Name = productVm.Name;
            product.Alias = productVm.Alias;
            product.Description = productVm.Description;
            product.CategoryID = productVm.CategoryID;
            product.Image = productVm.Image;
            product.MoreImages = productVm.MoreImages;
            product.DisplayOrder = productVm.DisplayOrder;
            product.Price = productVm.Price;
            product.Promotion = productVm.Promotion;
            product.Waranty = productVm.Waranty;
            product.Content = productVm.Content;
            product.HomeFlag = productVm.HomeFlag;
            product.HotFlag = productVm.HotFlag;
            product.ViewCount = productVm.ViewCount;
            product.CreatedDate = productVm.CreatedDate;
            product.CreatedBy = productVm.CreatedBy;
            product.UpdatedDate = productVm.UpdatedDate;
            product.UpdatedBy = productVm.UpdatedBy;
            product.MetaKeywork = productVm.MetaKeywork;
            product.MetaDescription = productVm.MetaDescription;
            product.Status = productVm.Status;
            product.Tags = productVm.Tags;
            product.Quantity = productVm.Quantity;
        }

        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackVm)
        {
            feedback.ID = feedbackVm.ID;
            feedback.Name = feedbackVm.Name;
            feedback.Email = feedbackVm.Email;
            feedback.Message = feedbackVm.Message;
            feedback.CreatedDate = DateTime.Now;
            feedback.Status = feedbackVm.Status;
        }

        public static void UpdateOrder(this Order order, OrderViewModel orderVm)
        {
            order.CustomerName = orderVm.CustomerName;
            order.CustomerAddress = orderVm.CustomerAddress;
            order.CustomerEmail = orderVm.CustomerEmail;
            order.CustomerMobile = orderVm.CustomerMobile;
            order.CustomerMessage = orderVm.CustomerMessage;
            order.PaymentMethod = orderVm.PaymentMethod;
            order.PaymentStatus = orderVm.PaymentStatus;
            order.CreatedDate = DateTime.Now;
            order.CreatedBy = orderVm.CreatedBy;
            order.CustomerId = orderVm.CustomerId;
        }

        public static void UpdateApplicationGroup(this ApplicationGroup applicationGroup, ApplicationGroupViewModel applicationGroupVm)
        {
            applicationGroup.Name = applicationGroupVm.Name;
            applicationGroup.Description = applicationGroupVm.Description;
        }

        public static void UpdateUser(this ApplicationUser applicationUser, ApplicationUserViewModel applicationUserVm)
        {
            applicationUser.Id = applicationUserVm.Id;
            applicationUser.FullName = applicationUserVm.FullName;
            applicationUser.BirthDate = applicationUserVm.BirthDate;
            applicationUser.Email = applicationUserVm.Email;
            applicationUser.UserName = applicationUserVm.UserName;
            applicationUser.PhoneNumber = applicationUserVm.PhoneNumber;
        }
    }
}