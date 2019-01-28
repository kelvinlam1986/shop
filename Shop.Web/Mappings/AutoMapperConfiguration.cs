using AutoMapper;
using Shop.Model.Models;
using Shop.Web.Models;
using System.Collections.Generic;

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
                        cfg.CreateMap<Footer, FooterViewModel>();
                        cfg.CreateMap<Slide, SlideViewModel>();
                        cfg.CreateMap<Page, PageViewModel>();
                    }
                );

            var mapper = config.CreateMapper();
            return mapper;
                
        }
    }
}