using ProjetoNetCoreWebMVC.Data;
using ProjetoNetCoreWebMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjetoNetCoreWebMVC.Services
{
    public class DepartmentService
    {
        private readonly ProjetoNetCoreWebMVCContext _context;

        public DepartmentService(ProjetoNetCoreWebMVCContext context)
        {
            _context = context;
        }

        //retorna do banco a lista de todos os Departments de maneira assíncrona para que a execução não bloqueie o processamento da aplicação
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();    //acessa a fonte de dados relacionada à tabela e converte para uma lista, mas primeiro realiza a ordenação por nome
        }



        //public void Insert(Department obj)
        //{
        //    //obj.Department = _context.Department.First();
        //    _context.Add(obj);
        //    _context.SaveChanges();
        //}
    }
}
