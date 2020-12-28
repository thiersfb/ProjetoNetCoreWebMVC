using ProjetoNetCoreWebMVC.Data;
using ProjetoNetCoreWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProjetoNetCoreWebMVC.Services
{
    public class SellerService
    {
        private readonly ProjetoNetCoreWebMVCContext _context;

        public SellerService(ProjetoNetCoreWebMVCContext context)
        {
            _context = context;
        }

        //retorna do banco a lista de todos os sellers
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();    //acessa a fonte de dados relacionada à tabela e converter para uma lista
        }

        public void Insert(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);  //"include" realiza o inner join das tabelas na expressão Lambda do Linq
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);    //objeto removido do DbSet
            _context.SaveChanges();
        }

    }
}
