using Shop.Web.Infrastructure.Core;
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

namespace Shop.Web.Api
{
    [RoutePrefix("api/applicationuser")]
    [Authorize]
    public class ApplicationUserController : ApiControllerBase
    {
        private ApplicationUserManager _applicationUserManager;

        public ApplicationUserController(
            ApplicationUserManager applicationUserManager,
            IErrorService errorService, 
            IMapper mapper) 
            : base(errorService, mapper)
        {
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
    }
}
