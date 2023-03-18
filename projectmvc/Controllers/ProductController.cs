using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using projectmvc.Hubs;
using projectmvc.Models;
using projectmvc.Repository;

namespace projectmvc.Controllers
{
    public class ProductController : Controller
    {
        Context Context = new Context();
        IProductsRepository productsRepository;
        IProductSizeColorRepository ProductSizeColorRepository;
        ICartRepository cartRepository; 
        private readonly IWebHostEnvironment _webHost;



        public ProductController
            (IProductsRepository _productsRepository, 
            IProductSizeColorRepository _productSizeColorRepository
            , ICartRepository _cartRepository,ICartRepository cartRepository)
        {
            productsRepository = _productsRepository;
            ProductSizeColorRepository = _productSizeColorRepository;
            cartRepository = _cartRepository;
            this.cartRepository = cartRepository;
        }
        public IActionResult Index()
        {
          
            return View();
        }


        public IActionResult SingleProduct(int id)
        {
            ViewBag.Title = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.Sizes = productsRepository.sizes(id);
            ViewBag.Colors= productsRepository.Colors(id);
            //ViewBag.Notification = 0;
            return View(ProductSizeColorRepository.GetProductSizeColor(id));
        }

        public IActionResult AllProduct()
        {
            ViewBag.Title = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            return View(productsRepository.GetAll());
        }


        // /product/Cart
        [HttpPost]
        public IActionResult AddToCart(ProductSizeColor obj) {
            ViewBag.Title = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            ProductSizeColorRepository.Insert(obj);
            Cart cart = new Cart();

            cart.ProductID=obj.ID;
            cart.CustomerID = Context.Users.Where(p => p.UserName == User.Identity.Name).Select(d => d.Id).FirstOrDefault();
                

            cartRepository.Insert(cart);

            return View();
        }

    }
}
