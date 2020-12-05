using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoNetCoreWebMVC.Models;

namespace ProjetoNetCoreWebMVC.Data
{
    public class ProjetoNetCoreWebMVCContext : DbContext
    {
        public ProjetoNetCoreWebMVCContext (DbContextOptions<ProjetoNetCoreWebMVCContext> options)
            : base(options)
        {
        }

        public DbSet<ProjetoNetCoreWebMVC.Models.Department> Department { get; set; }
    }
}
