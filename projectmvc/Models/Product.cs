using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectmvc.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        public int? Rating { get; set; }


        [ForeignKey("Supplier")]
        public string SupplierID { get; set; }

        public virtual ApplicationUser Supplier { get; set; }

        public virtual List<Review> Reviews { get; set; }



    }
}
