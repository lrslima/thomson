using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ThomsonReuters.Business.DomainObjects;

namespace ThomsonReuters.Business.Entities
{
    [Table("LegalCase")]
    public class LegalCase : Entity, IAggregateRoot
    {
        [Key]
        public string CaseNumber { get; set; }

        [Required]
        public string CourtName { get; set; }

        [Required]
        public string NameResponsible { get; set; }

        public DateTime RegistrationDate { get; set; }

        protected LegalCase()
        {

        }

        public LegalCase(string caseNumber, string courtName, string nameResponsible, DateTime registrationDate)
        {
            CaseNumber = caseNumber;
            CourtName = courtName;
            NameResponsible = nameResponsible;
            RegistrationDate = registrationDate;
        }


        public override bool IsValid()
        {
            ValidationResult = new LegalCaseValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class LegalCaseValidation : AbstractValidator<LegalCase>
        {
            public LegalCaseValidation()
            {
                RuleFor(p => p.CaseNumber)
                    .NotEmpty().WithMessage("Case Number cannot be empty")
                    .MaximumLength(20).WithMessage("Case number cannot be greater than 20 numbers");

                RuleFor(p => p.CourtName).NotEmpty().WithMessage("Court Name cannot be empty");

                RuleFor(p => p.NameResponsible).NotEmpty().WithMessage("Reponsible name cannot be empty");
            }
        }

    }
}
