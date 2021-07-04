using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomsonReuters.Business.Entities;
using ThomsonReuters.Business.Interfaces;

namespace ThomsonReuters.Infra
{
    public class LegalCasesContext : DbContext, IUnitOfWork
    {
        public LegalCasesContext(DbContextOptions<LegalCasesContext> options): base(options)
        {
        }

        public DbSet<LegalCase> LegalCases { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
