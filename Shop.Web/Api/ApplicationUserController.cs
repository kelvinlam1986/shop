﻿using Shop.Web.Infrastructure.Core;
using Shop.Web.Infrastructure.Extensions;
using System.Web.Http;
using AutoMapper;
using Shop.Service;
using System.Net.Http;
using Shop.Web.Models;
using Shop.Model.Models;
using System.Collections.Generic;
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Shop.Common;

namespace Shop.Web.Api
{
    [RoutePrefix("api/applicationuser")]
    [Authorize]
    public class ApplicationUserController : ApiControllerBase
    {
        private ApplicationUserManager _applicationUserManager;
        private IApplicationRoleServie _applicationRoleService;
        private IApplicationGroupService _applicationGroupService;

        public ApplicationUserController(
            ApplicationUserManager applicationUserManager,
            IApplicationRoleServie applicationRoleService,
            IApplicationGroupService applicationGroupService,
            IErrorService errorService, 
            IMapper mapper) 
            : base(errorService, mapper)
        {
            this._applicationUserManager = applicationUserManager;
            this._applicationRoleService = applicationRoleService;
            this._applicationGroupService = applicationGroupService;
        }

        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = this._applicationUserManager.Users;
                IEnumerable<ApplicationUserViewModel> modelVm =
                    Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(model);
                totalRow = modelVm.Count();
                var pagedSet = new PaginationSet<ApplicationUserViewModel>
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        [HttpPost]
        [Route("add")]
        public async Task<HttpResponseMessage> Create(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var newApplicationUser = new ApplicationUser();
                newApplicationUser.UpdateUser(applicationUserViewModel);

                try
                {
                    newApplicationUser.Id = Guid.NewGuid().ToString();
                    var result = await this._applicationUserManager.CreateAsync(newApplicationUser, applicationUserViewModel.Password);
                    if (result.Succeeded)
                    {
                        var listUserGroup = new List<ApplicationUserGroup>();
                        foreach (var group in applicationUserViewModel.Groups)
                        {
                            listUserGroup.Add(new ApplicationUserGroup
                            {
                                GroupId = group.ID,
                                UserId = newApplicationUser.Id
                            });

                            var listRole = this._applicationRoleService.GetListRoleByGroupId(group.ID);
                            foreach (var role in listRole)
                            {
                                await this._applicationUserManager.RemoveFromRoleAsync(newApplicationUser.Id, role.Name);
                                await this._applicationUserManager.AddToRoleAsync(newApplicationUser.Id, role.Name);
                            }
                        }

                        this._applicationGroupService.AddUserToGroups(listUserGroup, newApplicationUser.Id);
                        this._applicationGroupService.Save();
                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
                    }
                    else
                    {
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                    }
                }
                catch (NameDuplicatedException ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
