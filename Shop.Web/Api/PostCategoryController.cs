using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using System.Net;
using System.Net.Http;

namespace Shop.Web.Api
{
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) 
            : base (errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        public HttpResponseMessage Create(HttpRequestMessage request, PostCategory postCategory)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                } else
                {
                    var category = _postCategoryService.Add(postCategory);
                    _postCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, category);
                }
                return response;
            });
        }
    }
}