using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using ThomsonReuters.Application.ViewModels;

namespace ThomsonReuters.Application
{
    public class LegalCaseViewModel : ViewModel
    {
        public LegalCaseViewModel(string caseNumber, string courtName, string nameResponsible)
        {
            CaseNumber = caseNumber;
            CourtName = courtName;
            NameResponsible = nameResponsible;
        }

        [Required]
        public string CaseNumber { get; set; }

        [Required]
        public string CourtName { get; set; }

        [Required]
        public string NameResponsible { get; set; }


        public override bool IsValid()
        {
            ValidationResult = new LegalCaseValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        



        private class LegalCaseValidation : AbstractValidator<LegalCaseViewModel>
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
