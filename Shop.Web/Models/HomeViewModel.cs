using System.Collections.Generic;

namespace Shop.Web.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Slides = new List<SlideViewModel>();
            LatestProducts = new List<ProductViewModel>();
            TopSalesProducts = new List<ProductViewModel>();
        }

        public IEnumerable<SlideViewModel> Slides { get; set; }
        public IEnumerable<ProductViewModel> LatestProducts { get; set; }
        public IEnumerable<ProductViewModel> TopSalesProducts { get; set; }
    }
}