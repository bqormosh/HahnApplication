using Hahn.ApplicatonProcess.May2020.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Hahn.ApplicatonProcess.May2020.Domain.Services;
using System.Text.RegularExpressions;

namespace Hahn.ApplicatonProcess.May2020.Domain.ValidationModels
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        private ICountryService _countryService;
        Regex _emailRegex = new Regex(@"^*@*\.([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);

        public ApplicantValidator()
        {
            _countryService = new CountryService();
            RuleFor(Applicant => Applicant.Name).MinimumLength(5).WithMessage("Name must be at least 5 characters");
            RuleFor(Applicant => Applicant.FamilyName).MinimumLength(5).WithMessage("FamilyName must be at least 5 characters");
            RuleFor(Applicant => Applicant.Address).MinimumLength(10).WithMessage("Address must be at least 10 characters");

            RuleFor(x => x.CountryOfOrigin).MustAsync(async (id, cancellation) => {
                bool exists = await _countryService.isExistsAsync(id);
                return exists;
            }).WithMessage("Country Name is not valid");
            RuleFor(Applicant => Applicant.EmailAdress).Matches(_emailRegex).WithMessage("Please provide a valid email address");
            RuleFor(Applicant => Applicant.Age).InclusiveBetween(20,60).WithMessage("Age must be between 20 and 60 years old");
            RuleFor(Applicant => Applicant.Hired).NotNull().WithMessage("Please Choose if Applicant is Hired or not");

        }
    }
}
