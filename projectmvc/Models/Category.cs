using System.ComponentModel.DataAnnotations;

namespace projectmvc.Models
{
    public class Category
    {
        public int Id { get; set; }


        //[Display(Name = "Category Name")]
        public string Name { get; set; }
   
        public virtual List<SubCategory>? SubCategories  { get; set; }
    }
}
