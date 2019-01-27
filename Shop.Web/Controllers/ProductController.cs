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
using System.Web.Script.Serialization;

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
            var product = this._productService.GetById(productId);
            var productViewModel = Mapper.Map<Product, ProductViewModel>(product);
            var relatedProducts = this._productService.GetRelatedProducts(productId, 5);
            var relatedProductsViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProducts);
            ViewBag.RelatedProducts = relatedProductsViewModel;
            List<string> moreImageList = new List<string>();
            if (product.MoreImages != null)
            {
                moreImageList = new JavaScriptSerializer().Deserialize<List<string>>(product.MoreImages);
            }
           
            ViewBag.MoreImages = moreImageList;
            var tags = this._productService.GetTagListByProductId(productId);
            var tagsViewModel = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(tags);
            ViewBag.Tags = tagsViewModel;
            return View(productViewModel);
        }

        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int maxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            int totalRow = 0;
            var productModel = _productService.GetProductListByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
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

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int maxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            int totalRow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Keyword = keyword;
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

        public ActionResult ListByTag(string tagId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int maxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            int totalRow = 0;
            var productModel = _productService.GetProductsByTagId(tagId, page, pageSize, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var tag = this._productService.GetTagById(tagId);
            var tagViewModel = Mapper.Map<Tag, TagViewModel>(tag);
            ViewBag.Tag = tagViewModel;
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

        public JsonResult GetListProductByName(string keyword)
        {
            var products = this._productService.GetProductListByName(keyword);
            var productsVM = this._productService.GetProductListByName(keyword).ToList();
            return Json(new
            {
                data = products
            }, JsonRequestBehavior.AllowGet);
        }
    }
}