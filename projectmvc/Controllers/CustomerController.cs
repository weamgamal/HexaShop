﻿using Microsoft.AspNetCore.Mvc;

namespace projectmvc.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
