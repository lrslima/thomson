using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomsonReuters.Business.Entities;
using ThomsonReuters.Business.Models;

namespace ThomsonReuters.Business.Interfaces
{
    public interface ILegalCaseRepository : IRepository<LegalCase>
    {
        public Task<LegalCase> Create(LegalCase model);
        public Task<LegalCase> Update(LegalCase model);
        public Task<bool> Delete(string caseNumber);
        public Task<PagedResult<LegalCase>> GetAll(int pageSize, int pageIndex, string query = null);
        public Task<LegalCase> GetByNumber(string caseNumber);

    }
}
