using Microsoft.AspNetCore.Mvc;
using ProjetoNetCoreWebMVC.Data;
using ProjetoNetCoreWebMVC.Models;
using ProjetoNetCoreWebMVC.Models.ViewModels;
using ProjetoNetCoreWebMVC.Services;
using ProjetoNetCoreWebMVC.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using ClosedXML.Excel;
using System.IO;

namespace ProjetoNetCoreWebMVC.Controllers
{
    public class SellersController : Controller
    {
        //private readonly ProjetoNetCoreWebMVCContext _context;

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        //metodo construtor, realiza injeção de dependência
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        //metodo controlador
        //public async Task<IActionResult> Index()
        public async Task<IActionResult> Index(int? pagina)
        {
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);
            var list = await _sellerService.FindAllAsync();
            return View(list.ToPagedList(paginaNumero, paginaTamanho)); //na exibição é passada a lista de dados obtida através do metodo FindAll
            //return View(list); //na exibição é passada a lista de dados obtida através do metodo FindAll
            //return View();
        }

        // GET: Sellers/Create
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            //return View(viewModel);
            return PartialView(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {

            //valida se o formulário foi preenchido
            if (ModelState.IsValid)
            {
                await _sellerService.InsertAsync(seller);
                //return RedirectToAction(nameof(Index));
            }

            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return PartialView("Create", viewModel);

            
        }

        // GET: Sellers/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            //return View(obj);
            return PartialView(obj);
        }

        // POST: Sellers/Delete
        //[HttpPost, ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // GET: Sellers/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            //return View(obj);
            return PartialView(obj);
        }

        // GET: Seller/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            //return View(viewModel);
            return PartialView(viewModel);
        }

        // POST: Sellers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            //valida se o formulário foi preenchido
            if (ModelState.IsValid)
            {
                try
                {
                    await _sellerService.UpdateAsync(seller);
                    //return RedirectToAction(nameof(Index));
                    //return PartialView(nameof(Index));
                }
                catch (ApplicationException e)
                {
                    //return NotFound();
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
            }
            if (id != seller.Id)
            {
                //return BadRequest();
                return RedirectToAction(nameof(Error), new { message = "Id mistach" });
            }
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

            //return View(viewModel);
            return PartialView(viewModel);

        }


        // GET: Sellers/Eror
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }


        public async Task<IActionResult> ExportToExcel()
        {
            var list = await _sellerService.FindAllAsync();
            
            using(var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sellers");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 2).Value = "Nome";
                worksheet.Cell(currentRow, 3).Value = "E-mail";
                worksheet.Cell(currentRow, 4).Value = "Data de Nascimento";
                worksheet.Cell(currentRow, 5).Value = "Salário Base";

                foreach (var item in list)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.Id;
                    worksheet.Cell(currentRow, 2).Value = item.Name;
                    worksheet.Cell(currentRow, 3).Value = item.Email;
                    worksheet.Cell(currentRow, 4).Value = item.BirthDate;
                    worksheet.Cell(currentRow, 5).Value = item.BaseSalary;

                }

                using(var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sellers_"+ DateTime.Now +".xlsx");
                }
            }
            
        }

    }
}
