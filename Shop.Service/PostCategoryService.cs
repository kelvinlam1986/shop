using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service
{
    public interface IPostCategoryService
    {
        PostCategory Add(PostCategory postCategory);
        void Update(PostCategory postCategory);
        PostCategory Delete(int id);
        IEnumerable<PostCategory> GetAll();
        IEnumerable<PostCategory> GetAllByParentId(int parentId);
        PostCategory GetById(int id);
        void SaveChanges();
    }

    public class PostCategoryService : IPostCategoryService
    {
        private IPostCategoryRepository _postCategoryRepositry;
        private IUnitOfWork _unitOfWork;

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._postCategoryRepositry = postCategoryRepository;
            this._unitOfWork = unitOfWork;
        }
            

        public PostCategory Add(PostCategory postCategory)
        {
            return this._postCategoryRepositry.Add(postCategory);
        }

        public PostCategory Delete(int id)
        {
            return this._postCategoryRepositry.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return this._postCategoryRepositry.GetAll();
        }

        public IEnumerable<PostCategory> GetAllByParentId(int parentId)
        {
            return this._postCategoryRepositry.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public PostCategory GetById(int id)
        {
            return this._postCategoryRepositry.GetSingleById(id);
        }

        public void SaveChanges()
        {
            this._unitOfWork.Commit();
        }

        public void Update(PostCategory postCategory)
        {
            this._postCategoryRepositry.Update(postCategory);
        }
    }
}


