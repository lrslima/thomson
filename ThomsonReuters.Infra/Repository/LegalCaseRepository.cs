using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomsonReuters.Business.Entities;
using ThomsonReuters.Business.Interfaces;
using ThomsonReuters.Business.Models;

namespace ThomsonReuters.Infra.Repository
{
    public class LegalCaseRepository : ILegalCaseRepository
    {


        private readonly LegalCasesContext _context;

        public LegalCaseRepository(LegalCasesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<LegalCase> Create(LegalCase model)
        {
            await _context.LegalCases.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Delete(string caseNumber)
        {
            var legalCase = await GetByNumber(caseNumber);

            if (legalCase != null)
            {
                _context.LegalCases.Remove(legalCase);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<PagedResult<LegalCase>> GetAll(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT * FROM LegalCase 
                      WHERE (@CaseNumber IS NULL OR CaseNumber LIKE '%' + @CaseNumber + '%') 
                      ORDER BY [RegistrationDate] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS 
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(CaseNumber) FROM LegalCase 
                      WHERE (@CaseNumber IS NULL OR CaseNumber LIKE '%' + @CaseNumber + '%')";


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { CaseNumber = query });

            var legalCases = multi.Read<LegalCase>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedResult<LegalCase>()
            {
                List = legalCases,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }

        public async Task<LegalCase> GetByNumber(string caseNumber)
        {
            return _context.LegalCases.AsNoTracking().Where(x=> x.CaseNumber == caseNumber).First();
        }

        public async Task<LegalCase> Update(LegalCase model)
        {
            _context.LegalCases.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
