using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Models;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactDetailService _contactService;
        public IMapper Mapper { get; set; }

        public ContactController(IContactDetailService contactService, IMapper mapper)
        {
            this._contactService = contactService;
            Mapper = mapper;
        }

        // GET: Contact
        public ActionResult Index()
        {
            var contactDetail = this._contactService.GetDefaultContactDetail();
            var contactDetailViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(contactDetail);
            return View(contactDetailViewModel);
        }
    }
}