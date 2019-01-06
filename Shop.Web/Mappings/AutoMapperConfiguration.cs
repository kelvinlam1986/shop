using AutoMapper;
using Shop.Model.Models;
using Shop.Web.Models;

namespace Shop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public IMapper Configure()
        {
            var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                        cfg.CreateMap<Post, PostViewModel>();
                        cfg.CreateMap<Tag, TagViewModel>();
                        cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                        cfg.CreateMap<Product, ProductViewModel>();
                        cfg.CreateMap<ProductTag, ProductTagViewModel>();
                    }
                );

            var mapper = config.CreateMapper();
            return mapper;
                
        }
    }
}