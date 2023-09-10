using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Storage.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {            
        }

        public DbSet<StringDAO> StringsSet { get; set; }
    }
}