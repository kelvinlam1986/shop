using Shop.Common;
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
    public interface IProductService
    {
        Product Add(Product product);
        void Update(Product product);
        Product Delete(int id);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string keyword);
        IEnumerable<Product> GetHot(int top);
        IEnumerable<Product> GetLatest(int top);
        IEnumerable<Product> GetProductListByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow);
        IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow);
        IEnumerable<Product> GetRelatedProducts(int id, int top);
        IEnumerable<string> GetProductListByName(string name);
        Product GetById(int id);
        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(
            IProductRepository productRepository, 
            ITagRepository tagRepository,
            IProductTagRepository productTagRepository,
            IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._tagRepository = tagRepository;
            this._productTagRepository = productTagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product product)
        {
            var addedProduct = _productRepository.Add(product);
            _unitOfWork.Commit();

            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (int i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        var tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }

                    var productTag = new ProductTag();
                    productTag.ProductID = addedProduct.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }

            _unitOfWork.Commit();
            return addedProduct;
        }

        public Product Delete(int id)
        {
            return this._productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return this._productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return this._productRepository.GetAll();
            }
            else
            {
                return this._productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
        }

        public Product GetById(int id)
        {
            return this._productRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            this._unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            this._productRepository.Update(product);
            this._productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (int i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        var tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    
                    var productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }

            _unitOfWork.Commit();

        }

        public IEnumerable<Product> GetHot(int top)
        {
            return this._productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetLatest(int top)
        {
            return this._productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetProductListByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = this._productRepository.GetMulti(x => x.CategoryID == categoryId && x.Status);
            switch (sort )
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.Promotion.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetProductListByName(string name)
        {
            return this._productRepository.GetMulti(x => x.Status && x.Name.Contains(name)).Select(x => x.Name);
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = this._productRepository.GetMulti(x => x.Name.Contains(keyword) && x.Status);
            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.Promotion.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetRelatedProducts(int id, int top)
        {
            var product = this._productRepository.GetSingleById(id);
            return this._productRepository.GetMulti(x => x.Status && x.ID != id && x.CategoryID == product.CategoryID)
                    .OrderByDescending(x => x.CreatedDate).Take(top);
        }
    }
}
