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
using System;

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
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword = "", int page = 0, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var listProductCategory = _productCategoryService.GetAll(keyword);
                totalRow = listProductCategory.Count();
                var listProductCategoryPaged =
                    listProductCategory
                        .OrderByDescending(x => x.CreatedDate)
                        .Skip(page * pageSize)
                        .Take(pageSize);
                var listProductCategoryVM = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listProductCategoryPaged);
                var paginationSet = new PaginationSet<ProductCategoryViewModel>
                {
                    Items = listProductCategoryVM,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)(totalRow / pageSize))
                };

                response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
    }
}
