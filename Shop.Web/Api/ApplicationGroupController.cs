using AutoMapper;
using Shop.Common;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Infrastructure.Extensions;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Shop.Web.Api
{
    [RoutePrefix("api/applicationgroup")]
    [Authorize]
    public class ApplicationGroupController : ApiControllerBase
    {
        private IApplicationGroupService _applicationGroupService;

        public ApplicationGroupController(IErrorService errorService, IApplicationGroupService applicationGroupService, IMapper mapper) 
            : base(errorService, mapper)
        {
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
                var applicationGroupVM = Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(applicationGroup);
                response = request.CreateResponse(HttpStatusCode.OK, applicationGroupVM);
                return response;
            });
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var appGroup = this._applicationGroupService.Delete(id);
            this._applicationGroupService.Save();
            return request.CreateResponse(HttpStatusCode.OK, appGroup);
        }

        [HttpDelete]
        [Route("deletemulti")]
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
                    this._applicationGroupService.Save();

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
        public HttpResponseMessage Put(HttpRequestMessage request, ApplicationGroupViewModel applicationGroupViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var applicationGroupDb = _applicationGroupService.GetDetail(applicationGroupViewModel.ID);
                    applicationGroupDb.UpdateApplicationGroup(applicationGroupViewModel);
                    _applicationGroupService.Update(applicationGroupDb);
                    _applicationGroupService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }
    }
}
