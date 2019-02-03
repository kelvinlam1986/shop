using AutoMapper;
using BotDetect.Web.Mvc;
using Shop.Common;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Extensions;
using Shop.Web.Models;
using System.Text;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactDetailService _contactService;
        private IFeedbackService _feedbackService;

        public IMapper Mapper { get; set; }

        public ContactController(IContactDetailService contactService, IFeedbackService feedbackService, IMapper mapper)
        {
            this._contactService = contactService;
            this._feedbackService = feedbackService;
            Mapper = mapper;
        }

        // GET: Contact
        public ActionResult Index()
        {
            var feedbackViewModel = new FeedbackViewModel();
            feedbackViewModel.ContactViewModel = GetDetail();
            return View(feedbackViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [SimpleCaptchaValidation("CaptchaCode", "ContactCaptcha", "Mã xác nhận không đúng")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback();
                feedback.UpdateFeedback(feedbackViewModel);
                this._feedbackService.Create(feedback);
                this._feedbackService.Save();
                ViewData["SuccessMsg"] = "Gửi phản hồi thành công !";
                var builder = new StringBuilder();
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/contact_template.html"));
                content = content.Replace("{{Name}}", feedbackViewModel.Name);
                content = content.Replace("{{Email}}", feedbackViewModel.Email);
                content = content.Replace("{{Message}}", feedbackViewModel.Message);

                MailHelper.SendEmail(feedbackViewModel.Email, "Thông tin liên hệ từ website", content);

                feedbackViewModel.Name = "";
                feedbackViewModel.Message = "";
                feedbackViewModel.Email = "";
            }

            feedbackViewModel.ContactViewModel = GetDetail();
            return View("Index", feedbackViewModel);
        }

        private ContactDetailViewModel GetDetail()
        {
            var model = this._contactService.GetDefaultContactDetail();
            var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return contactViewModel;
        }

    }
}