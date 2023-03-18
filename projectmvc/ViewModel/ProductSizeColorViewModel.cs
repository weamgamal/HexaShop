using projectmvc.Models;

namespace projectmvc.ViewModel
{
    public class ProductSizeColorViewModel
    {
        public Product Product { get; set; }
        public List<Color> Colors { get; set; } = new List<Color>();
        public List<Size> Sizes { get; set; } = new List<Size>();

    }
}
