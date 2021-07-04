using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomsonReuters.Application;
using ThomsonReuters.Business.Entities;
using ThomsonReuters.Business.Models;

namespace ThomsonReuters.Business.Interfaces
{
    public interface ILegalCaseService
    {
        public Task<LegalCase> Create(LegalCaseViewModel model);
        public Task<LegalCase> Update(LegalCaseViewModel model);
        public Task<bool> Delete(string caseNumber);
        public Task<PagedResult<LegalCase>> GetAll(int pageSize, int pageIndex, string query = null);
        public Task<LegalCase> GetByNumber(string caseNumber);

    }
}
