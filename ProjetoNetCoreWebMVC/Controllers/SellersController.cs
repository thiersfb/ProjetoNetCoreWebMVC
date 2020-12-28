using Microsoft.AspNetCore.Mvc;
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
    }
}
