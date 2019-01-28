using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Models;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class PageController : Controller
    {
        private IPageService _pageService;

        public IMapper Mapper { get; set; }

        public PageController(IPageService pageService, IMapper mapper)
        {
            this._pageService = pageService;
            Mapper = mapper;
        }

        // GET: Page
        public ActionResult Index(string alias)
        {
            var page = this._pageService.GetPageByAlias(alias);
            var pageViewModel = Mapper.Map<Page, PageViewModel>(page);
            return View(pageViewModel);
        }
    }
}