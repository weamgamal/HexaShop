using System.ComponentModel.DataAnnotations.Schema;

namespace projectmvc.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public string CustomerID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        public virtual ProductSizeColor Product { get; set; }
        public virtual ApplicationUser Customer { get; set; }
    }
}
