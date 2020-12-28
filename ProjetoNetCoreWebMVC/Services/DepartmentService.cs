using ProjetoNetCoreWebMVC.Data;
using ProjetoNetCoreWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoNetCoreWebMVC.Services
{
    public class DepartmentService
    {
        private readonly ProjetoNetCoreWebMVCContext _context;

        public DepartmentService(ProjetoNetCoreWebMVCContext context)
        {
            _context = context;
        }

        //retorna do banco a lista de todos os Departments
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();    //acessa a fonte de dados relacionada à tabela e converter para uma lista, mas primeiro realiza a ordenação por nome
        }

        public void Insert(Department obj)
        {
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
