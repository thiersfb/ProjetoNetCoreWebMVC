using Microsoft.AspNetCore.Mvc;
using ProjetoNetCoreWebMVC.Models;
using ProjetoNetCoreWebMVC.Models.ViewModels;
using ProjetoNetCoreWebMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoNetCoreWebMVC.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        //metodo construtor, realiza injeção de dependência
        public  SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        //metodo controlador
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list); //na exibição é passada a lista de dados obtida através do metodo FindAll
            //return View();
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Details()
        {
            var list = _sellerService.FindAll();
            return View(list); //na exibição é passada a lista de dados obtida através do metodo FindAll
            //return View();
        }

        public IActionResult Delete()
        {
            var list = _sellerService.FindAll();
            return View(list); //na exibição é passada a lista de dados obtida através do metodo FindAll
            //return View();
        }
    }
}
