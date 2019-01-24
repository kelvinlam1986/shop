using AutoMapper;
using Shop.Common;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;

        public IMapper Mapper { get; set; }

        public ProductController(IProductService productService, IProductCategoryService productCategoryService, IMapper mapper)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
            Mapper = mapper;
        }

        public ActionResult Detail(int productId)
        {
            return View();
        }

        public ActionResult Category(int id, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int maxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            int totalRow = 0;
            var productModel = _productService.GetProductListByCategoryIdPaging(id, page, pageSize, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow/pageSize);
            var category = _productCategoryService.GetById(id);
            var categoryViewModel = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);
            ViewBag.Category = categoryViewModel;

            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = productViewModel,
                MaxPage = maxPage,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }
    }
}