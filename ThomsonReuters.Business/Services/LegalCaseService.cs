using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomsonReuters.Application;
using ThomsonReuters.Business.Entities;
using ThomsonReuters.Business.Interfaces;
using ThomsonReuters.Business.Models;

namespace ThomsonReuters.Business.Services
{
    public class LegalCaseService : CNJStandard, ILegalCaseService
    {
        private readonly ILegalCaseRepository _legalCaseRepository;
        public LegalCaseService(ILegalCaseRepository legalCaseRepository)
        {
            _legalCaseRepository = legalCaseRepository;
        }

        public async Task<LegalCase> Create(LegalCaseViewModel vm)
        {
            try
            {
                string caseNumberFormmatted = FormatCaseNumber(vm.CaseNumber);
                LegalCase legalCase = new LegalCase
                (
                    caseNumberFormmatted, vm.CourtName, vm.NameResponsible, DateTime.Now
                );
                return await _legalCaseRepository.Create(legalCase);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(string caseNumber)
        {
            try
            {
                string caseNumberFormmatted = FormatCaseNumber(caseNumber);

                var legalCase = await _legalCaseRepository.GetByNumber(caseNumberFormmatted);
                if (legalCase != null)
                {
                    return await _legalCaseRepository.Delete(caseNumberFormmatted);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override string FormatCaseNumber(string caseNumber)
        {
            string value = caseNumber.Replace("-", "");
            value = value.Replace(".", "");

            var length = value.Length;
            var difference = 20 - length;
            string result = "";

            if (difference > 0)
            {
                result = caseNumber.ToString().PadLeft(20, '0');

            }
            else
            {
                result = value;
            }

            result = result.Insert(7, "-");
            result = result.Insert(10, ".");
            result = result.Insert(15, ".");
            result = result.Insert(17, ".");
            result = result.Insert(20, ".");

            return result;
        }

        public async Task<PagedResult<LegalCase>> GetAll(int pageSize, int pageIndex, string query = null)
        {
            try
            {
                return await _legalCaseRepository.GetAll(pageSize, pageIndex, query);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LegalCase> GetByNumber(string caseNumber)
        {
            try
            {
                return await _legalCaseRepository.GetByNumber(caseNumber);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LegalCase> Update(LegalCaseViewModel vm)
        {
            try
            {
                string caseNumberFormmatted = FormatCaseNumber(vm.CaseNumber);
                vm.CaseNumber = caseNumberFormmatted;

                var legalCase = await _legalCaseRepository.GetByNumber(vm.CaseNumber);

                if (legalCase != null)
                {
                    LegalCase updateLegalCase = new LegalCase
                    (
                        vm.CaseNumber, vm.CourtName, vm.NameResponsible, DateTime.Now
                    );

                    return await _legalCaseRepository.Update(updateLegalCase);

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
