using Microsoft.AspNetCore.Mvc;
using projectmvc.Models;
using projectmvc.Repository;
using projectmvc.ViewModel;

namespace projectmvc.Controllers
{
    public class SupplierController : Controller

   {
        Context Context = new Context();
        ICategoryRepository CategoryRepository;
        ISubCategoryRepository SubCategoryRepository;
        ISizeRepository SizeRepository;
        IColorRepository ColorRepository;
        IProductsRepository ProductRepository;
        IProductSizeColorRepository ProductSizeColorRepository;
        IWebHostEnvironment _webHost;
        public SupplierController(ICategoryRepository category, ISubCategoryRepository subCategory
            , ISizeRepository sizeRepository, IColorRepository colorRepository, IProductsRepository productsRepository
, IProductSizeColorRepository productSizeColorRepository, IWebHostEnvironment webHost)
        {
            CategoryRepository = category;
            SubCategoryRepository = subCategory;
            SizeRepository = sizeRepository;
            ColorRepository = colorRepository;
            this.ProductRepository = productsRepository;
            ProductSizeColorRepository = productSizeColorRepository;
            _webHost = webHost;
        }

        // /Supplier/index
        public IActionResult Index()
        {
            ViewBag.Title = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            var SupID = Context.Users.Where(p => p.UserName == User.Identity.Name).Select(p=>p.Id).FirstOrDefault();
           
            return View(ProductRepository.GetBySupplierId(SupID));
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.categories=CategoryRepository.GetAll();
            ViewBag.subCategories=SubCategoryRepository.GetAll();
            ViewData["colors"] = ColorRepository.GetAll();
            ViewData["Sizes"]= SizeRepository.GetAll();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct([Bind("ProductName,ProductDescription,Price,Rating,Quentity," +
            "CategoryId,SubCategoryId,ColorId,SizeId,ProductImage,ImageFile")] AddProductsViewModel Newmodel)
        {
            if (ModelState.IsValid)
            {
                // save image to wwwRoot/Image
                string wwwRootPath = _webHost.WebRootPath;
                string FileName = Path.GetFileNameWithoutExtension(Newmodel.ImageFile.FileName);
                string Extension = Path.GetExtension(Newmodel.ImageFile.FileName);
                Newmodel.ProductImage = FileName = FileName + DateTime.Now.ToString("yymmss") + Extension;
                string path = Path.Combine(wwwRootPath + "/UploadedImages/" + FileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await Newmodel.ImageFile.CopyToAsync(fileStream);
                }




                Product newPro = new Product();
                newPro.Name = Newmodel.ProductName;
                newPro.Description = Newmodel.ProductDescription;
                newPro.Price = Newmodel.Price;
                newPro.Image = Newmodel.ProductImage;
                newPro.Rating = Newmodel.Rating;
                newPro.SupplierID = Context.Users.Where(p => p.UserName == User.Identity.Name).Select(d => d.Id).FirstOrDefault();

                ProductRepository.Insert(newPro);

                ProductSizeColor productSizeColor = new ProductSizeColor();

                productSizeColor.ProductID = newPro.Id;
                productSizeColor.ProductColorID =Newmodel.ColorId;
                productSizeColor.ProductSizeID =Newmodel.SizeId;
                productSizeColor.Quantity = Newmodel.Quentity;
                productSizeColor.SubCategoryId = Newmodel.SubCategoryId;
                ProductSizeColorRepository.Insert(productSizeColor);


            }


            ViewBag.categories = CategoryRepository.GetAll();
            ViewBag.subCategories = SubCategoryRepository.GetAll();
            ViewBag.colors = ColorRepository.GetAll();
            ViewBag.Sizes = SizeRepository.GetAll();
            return View(Newmodel);

          
        }
    }
}
