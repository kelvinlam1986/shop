using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Web.Api
{
    [RoutePrefix("api/applicationrole")]
    [Authorize]
    public class ApplicationRoleController : ApiControllerBase
    {
        private IApplicationRoleServie _applicationRoleService;

        public ApplicationRoleController(
            IApplicationRoleServie applicationRoleService, 
            IErrorService errorService,
            IMapper mapper) : base(errorService, mapper)
        {
            this._applicationRoleService = applicationRoleService;
        }

        [Route("getlistall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = this._applicationRoleService.GetAll();
                IEnumerable<ApplicationRoleViewModel> roles =
                    Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, roles);
                return response;
            });
        }
    }
}
