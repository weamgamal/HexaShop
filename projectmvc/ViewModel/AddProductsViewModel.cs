using projectmvc.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectmvc.ViewModel
{
    public class AddProductsViewModel
    {

        public  String ProductName { get; set; }
        public String ProductDescription { get; set; }

        public String ProductImage { get; set; }

        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }
        public  double Price { get;set; }

        public int Rating { get;set; }

        public int Quentity { get; set; }


        public int CategoryId { get; set; }

        public int SubCategoryId  { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }


        /*   public List<Category> Category { get; set; }

           public List< SubCategory> SubCategory { get; set; }
          public List<Color> Color { get; set; }
           public List<Size> Size { get; set; } */

    }
}
