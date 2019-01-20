using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Models;
using Shop.Web.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System;

namespace Shop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    [Authorize]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService, IMapper mapper) 
            : base (errorService, mapper)
        {
            this._postCategoryService = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
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
                    var listCategory = _postCategoryService.GetAll();
                    var listCategoryVM = Mapper.Map<List<PostCategoryViewModel>>(listCategory);
                    response = request.CreateResponse(HttpStatusCode.OK, listCategoryVM);
                }
                return response;
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                } else
                {
                    var postCategory = new PostCategory();
                    postCategory.UpdatePostCategory(postCategoryVM);
                    postCategory.CreatedBy = User.Identity.Name;
                    postCategory.CreatedDate = DateTime.Now;
                    postCategory.UpdatedBy = User.Identity.Name;
                    postCategory.UpdatedDate = DateTime.Now;
                    var category = _postCategoryService.Add(postCategory);
                    _postCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, category);
                }
                return response;
            });
        }

        private HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> p)
        {
            throw new NotImplementedException();
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
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
                    var postCategoryDb = _postCategoryService.GetById(postCategoryVM.ID);
                    postCategoryDb.UpdatePostCategory(postCategoryVM);
                    postCategoryDb.UpdatedDate = DateTime.Now;
                    postCategoryDb.UpdatedBy = User.Identity.Name;
                    _postCategoryService.Update(postCategoryDb);
                    _postCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    _postCategoryService.Delete(id);
                    _postCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }
    }
}