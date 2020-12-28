using ProjetoNetCoreWebMVC.Data;
using ProjetoNetCoreWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
