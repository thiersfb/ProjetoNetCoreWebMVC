using Microsoft.AspNetCore.Mvc;
using ProjetoNetCoreWebMVC.Models;
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

        //metodo construtor, realiza injeção de dependência
        public  SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name")] Sellers sellers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(department);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(department);
        //}
        //

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
