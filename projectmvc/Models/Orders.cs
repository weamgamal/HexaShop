using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectmvc.Models
{

    public class Orders
    {
        public int Id { get; set; }

        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }

        public double Discount { get; set; }

        [ForeignKey("Customer")]
        public string CustomerID{ get; set; }

        [ForeignKey("OrderStatus")]
        public int OrderStatusID { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ApplicationUser Customer{ get; set; }  
    }
}
