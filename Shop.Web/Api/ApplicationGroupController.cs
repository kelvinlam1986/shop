using AutoMapper;
using Shop.Common;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Infrastructure.Extensions;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Shop.Web.Api
{
    [RoutePrefix("api/applicationgroup")]
    [Authorize]
    public class ApplicationGroupController : ApiControllerBase
    {
        private IApplicationGroupService _applicationGroupService;
        private IApplicationRoleServie _applicationRoleService;
        private ApplicationUserManager _applicationUserManager;

        public ApplicationGroupController(
            IErrorService errorService, 
            IApplicationGroupService applicationGroupService, 
            IApplicationRoleServie applicationRoleService,
            ApplicationUserManager applicationUserManager,
            IMapper mapper) 
            : base(errorService, mapper)
        {
            this._applicationGroupService = applicationGroupService;
            this._applicationRoleService = applicationRoleService;
            this._applicationUserManager = applicationUserManager;
        }

        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = this._applicationGroupService.GetAll(page, pageSize, out totalRow, filter);
                IEnumerable<ApplicationGroupViewModel> modelVm =
                    Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(model);
                PaginationSet<ApplicationGroupViewModel> pagedSet = new PaginationSet<ApplicationGroupViewModel>
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

        [Route("getlistall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = this._applicationGroupService.GetAll();
                IEnumerable<ApplicationGroupViewModel> groupsVm = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, groupsVm);
                return response;
            });
        }

        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var applicationGroup = _applicationGroupService.GetDetail(id);
                if (applicationGroup == null)
                {
                    return request.CreateErrorResponse(HttpStatusCode.NoContent, "No group");
                }

                var listRole = _applicationRoleService.GetListRoleByGroupId(applicationGroup.ID);
                var applicationGroupVM = Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(applicationGroup);
                applicationGroupVM.Roles = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(listRole);
                response = request.CreateResponse(HttpStatusCode.OK, applicationGroupVM);
                return response;
            });
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "DeleteGroupUser")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var appGroup = this._applicationGroupService.Delete(id);
            this._applicationGroupService.Save();
            return request.CreateResponse(HttpStatusCode.OK, appGroup);
        }

        [HttpDelete]
        [Route("deletemulti")]
        [Authorize(Roles = "DeleteGroupUser")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedList)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listItem = new JavaScriptSerializer().Deserialize<List<int>>(checkedList);
                    foreach (var item in listItem)
                    {
                        this._applicationGroupService.Delete(item);
                    }

                    this._applicationGroupService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "AddGroupUser")]
        public HttpResponseMessage Create(HttpRequestMessage request, ApplicationGroupViewModel applicationGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var newApplicationGroup = new ApplicationGroup();
                newApplicationGroup.Name = applicationGroupViewModel.Name;
                newApplicationGroup.Description = applicationGroupViewModel.Description;
                try
                {
                    var applicationGroup = this._applicationGroupService.Add(newApplicationGroup);
                    var listRoleGroup = new List<ApplicationRoleGroup>();
                    foreach (var role in applicationGroupViewModel.Roles)
                    {
                        listRoleGroup.Add(new ApplicationRoleGroup
                        {
                            GroupId = applicationGroup.ID,
                            RoleId = role.Id
                        });
                    }

                    this._applicationRoleService.AddRolesToGroup(listRoleGroup, applicationGroup.ID);
                    this._applicationRoleService.Save();

                    return request.CreateResponse(HttpStatusCode.OK, applicationGroupViewModel);
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

        [Route("update")]
        [Authorize(Roles = "UpdateGroupUser")]
        public async Task<HttpResponseMessage> Put(HttpRequestMessage request, ApplicationGroupViewModel applicationGroupViewModel)
        {
            HttpResponseMessage response = null;
            if (!ModelState.IsValid)
            {
                request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                var applicationGroupDb = _applicationGroupService.GetDetail(applicationGroupViewModel.ID);
                try
                {
                    applicationGroupDb.UpdateApplicationGroup(applicationGroupViewModel);
                    _applicationGroupService.Update(applicationGroupDb);

                    var listRoleGroup = new List<ApplicationRoleGroup>();
                    foreach (var item in applicationGroupViewModel.Roles)
                    {
                        listRoleGroup.Add(new ApplicationRoleGroup
                        {
                            GroupId = applicationGroupDb.ID,
                            RoleId = item.Id
                        });
                    }

                    _applicationRoleService.AddRolesToGroup(listRoleGroup, applicationGroupDb.ID);
                    _applicationRoleService.Save();

                    var listRole = _applicationRoleService.GetListRoleByGroupId(applicationGroupDb.ID);
                    var listUserInGroup = _applicationGroupService.GetListUserByGroupId(applicationGroupDb.ID);
                    foreach (var user in listUserInGroup)
                    {
                        var listRoleName = listRole.Select(x => x.Name).ToArray();
                        foreach (var roleName in listRoleName)
                        {
                            await this._applicationUserManager.RemoveFromRoleAsync(user.Id, roleName);
                            await this._applicationUserManager.AddToRoleAsync(user.Id, roleName);
                        }
                    }

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                catch (NameDuplicatedException nameDuplicatedException)
                {
                   response = request.CreateErrorResponse(HttpStatusCode.BadRequest, nameDuplicatedException.Message);
                }

            }

            return response;
        }
    }
}
