using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private ICommonService _commonService;
        private IProductService _productService;

        public IMapper Mapper { get; set; }

        public HomeController(
            IProductCategoryService productCategoryService,
            IProductService productService, 
            ICommonService commonService, 
            IMapper mapper)
        {
            this._productCategoryService = productCategoryService;
            this._commonService = commonService;
            this._productService = productService;
            Mapper = mapper;
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var slides = this._commonService.GetSlides();
            var latestProducts = this._productService.GetLatest(3);
            var hotProducts = this._productService.GetHot(3);

            var slidesViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slides);
            var latestProductsViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(latestProducts);
            var hotProductsViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(hotProducts);

            var homeViewModel = new HomeViewModel
            {
                Slides = slidesViewModel,
                LatestProducts = latestProductsViewModel,
                TopSalesProducts = hotProductsViewModel
            };

            return View(homeViewModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            var footer = this._commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footer);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Category()
        {
            var categories = _productCategoryService.GetAll();
            var categoryListViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(categories);
            return PartialView(categoryListViewModel);
        }
    }
}