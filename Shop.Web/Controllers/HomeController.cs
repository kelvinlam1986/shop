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

        public IMapper Mapper { get; set; }

        public HomeController(IProductCategoryService productCategoryService, ICommonService commonService, IMapper mapper)
        {
            this._productCategoryService = productCategoryService;
            this._commonService = commonService;
            Mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
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

        public ActionResult Category()
        {
            var categories = _productCategoryService.GetAll();
            var categoryListViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(categories);
            return PartialView(categoryListViewModel);
        }
    }
}