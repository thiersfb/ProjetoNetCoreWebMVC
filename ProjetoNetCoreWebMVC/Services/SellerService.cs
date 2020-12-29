using ProjetoNetCoreWebMVC.Data;
using ProjetoNetCoreWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ProjetoNetCoreWebMVC.Services.Exceptions;

namespace ProjetoNetCoreWebMVC.Services
{
    public class SellerService
    {
        private readonly ProjetoNetCoreWebMVCContext _context;

        public SellerService(ProjetoNetCoreWebMVCContext context)
        {
            _context = context;
        }

        //retorna do banco a lista de todos os sellers  de maneira assíncrona para que a execução não bloqueie o processamento da aplicação
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.OrderBy(x => x.Name).ToListAsync();
            //return await _context.Seller.ToListAsync();    //acessa a fonte de dados relacionada à tabela e converter para uma lista
        }

        public async Task InsertAsync(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);  //"include" realiza o inner join das tabelas na expressão Lambda do Linq
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);    //objeto removido do DbSet
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                //throw new IntegrityException(e.Message);
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);    //objeto atualizado no DbSet
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
