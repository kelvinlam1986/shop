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
using Shop.Web.Infrastructure.Extensions;

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
        [HttpGet]
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

        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var productCategory = _productCategoryService.GetById(id);
                var productCategoryVM = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
                response = request.CreateResponse(HttpStatusCode.OK, productCategoryVM);
                return response;
            });
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAllParents(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var listProductCategory = _productCategoryService.GetAll();
                var listProductCategoryVM = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listProductCategory);
                response = request.CreateResponse(HttpStatusCode.OK, listProductCategoryVM);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
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
                    var newProductCategory = new ProductCategory();
                    newProductCategory.UpdateProductCategory(productCategoryVm);
                    newProductCategory.CreatedBy = "admin";
                    newProductCategory.CreatedDate = DateTime.Now;
                    newProductCategory.UpdatedBy = "admin";
                    newProductCategory.UpdatedDate = DateTime.Now;
                    _productCategoryService.Add(newProductCategory);
                    _productCategoryService.SaveChanges();
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
               
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
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
                    var dbProductCategory = _productCategoryService.GetById(productCategoryVm.ID);
                    dbProductCategory.UpdateProductCategory(productCategoryVm);
                    dbProductCategory.UpdatedBy = "admin";
                    dbProductCategory.UpdatedDate = DateTime.Now;
                    _productCategoryService.Update(dbProductCategory);
                    _productCategoryService.SaveChanges();
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(dbProductCategory);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    var oldProduct = _productCategoryService.Delete(id);
                    _productCategoryService.SaveChanges();
                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(oldProduct);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }
    }
}
