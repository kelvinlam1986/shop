using Shop.Web.Infrastructure.Core;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using AutoMapper;
using Shop.Service;
using Shop.Model.Models;
using Shop.Web.Models;
using System.Web.Http;

namespace Shop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService, IMapper mapper) : base(errorService, mapper)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
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
                    var listProductCategory = _productCategoryService.GetAll().ToList();
                    var listProductCategoryVM = Mapper.Map<List<ProductCategory>, List<ProductCategoryViewModel>>(listProductCategory);
                    response = request.CreateResponse(HttpStatusCode.OK, listProductCategoryVM);
                }
                return response;
            });
        }
    }
}
