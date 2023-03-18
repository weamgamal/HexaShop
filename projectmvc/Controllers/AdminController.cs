using Microsoft.AspNetCore.Mvc;
using projectmvc.Models;
using projectmvc.Repository;

namespace projectmvc.Controllers
{
    public class AdminController : Controller
    {


        Context Context = new Context();
        IProductsRepository productsRepository;
        IProductSizeColorRepository ProductSizeColorRepository;
        ICartRepository cartRepository; 
        public AdminController(Context context, IProductsRepository productsRepository, IProductSizeColorRepository productSizeColorRepository, ICartRepository cartRepository)
        {
            Context = context;
            this.productsRepository = productsRepository;
            ProductSizeColorRepository = productSizeColorRepository;
            this.cartRepository = cartRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult showOrdersArrived()
        {
            int OrderID = Context.OrderStatuses.Where(e => e.Name == "Arrived").Select(e => e.Id).First();
            List<Orders> orders=Context.Orders.Where(e=>e.OrderStatusID==OrderID).ToList();


            return View(orders);
        }
        public IActionResult showProducts()
        {
            ViewBag.Title = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            return View(productsRepository.GetAll());
        }
        public IActionResult showCustomers()
        {
            string CustomerIDs = Context.Roles.Where(e=>e.NormalizedName=="CUSTOMER").Select(e=>e.Id).First();
            List<string> userIDs = Context.UserRoles.Where(e=>e.RoleId == CustomerIDs).Select(e=>e.UserId).ToList();
            List<ApplicationUser> customers= new List<ApplicationUser>();
            foreach (string userID in userIDs)
            {
                customers.Add(Context.Users.Where(e => e.Id == userID).First());
            }

            return View(customers);
        }
        public IActionResult showSupplier()
        {
            string CustomerIDs = Context.Roles.Where(e => e.NormalizedName == "SUPPLIER").Select(e => e.Id).First();
            List<string> userIDs = Context.UserRoles.Where(e => e.RoleId == CustomerIDs).Select(e => e.UserId).ToList();
            List<ApplicationUser> customers = new List<ApplicationUser>();
            foreach (string userID in userIDs)
            {
                customers.Add(Context.Users.Where(e => e.Id == userID).First());
            }

            return View(customers);
        }
        public IActionResult showProfits()
        {
            return View();
        }
    }
}
