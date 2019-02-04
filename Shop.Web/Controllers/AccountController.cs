﻿using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Shop.Common;
using Shop.Model.Models;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [SimpleCaptchaValidation("CaptchaCode", "RegisterCaptcha", "Mã xác nhận không đúng")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userFindByEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userFindByEmail != null)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại.");
                    return View(model);
                }

                var userFindByUserName = await _userManager.FindByNameAsync(model.UserName);
                if (userFindByUserName != null)
                {
                    ModelState.AddModelError("UserName", "Tên tài khoản này đã được sử dụng.");
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    BirthDate = DateTime.Now,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address
                };

                await _userManager.CreateAsync(user, model.Password);

                var userAddToRole = await _userManager.FindByEmailAsync(model.Email);
                if (userAddToRole != null)
                {
                    await _userManager.AddToRolesAsync(userAddToRole.Id, new string[] { "User" });
                }

                ViewData["SuccessMsg"] = "Đăng ký thành công";

                var builder = new StringBuilder();
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/newuser.html"));
                content = content.Replace("{{UserName}}", model.UserName);
                content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink") + "dang-nhap.html");

                MailHelper.SendEmail(model.Email, "Đăng ký tài khoản thành công tại MyShop", content);

            }
            return View();
        }
    }
}