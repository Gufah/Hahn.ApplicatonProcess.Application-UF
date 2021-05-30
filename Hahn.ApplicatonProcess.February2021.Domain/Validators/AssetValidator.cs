using FluentValidation;
using System;
using Hahn.ApplicatonProcess.February2021.Domain.Models;
using Hahn.ApplicatonProcess.February2021.Domain.Repsitories;

namespace Hahn.ApplicatonProcess.February2021.Domain.Validators
{
    public class AssetValidator : AbstractValidator<Asset>
    {
        private readonly IHttpClient _httpClient;
        public AssetValidator(IHttpClient httpClient)
        {
            _httpClient = httpClient;
            
            RuleFor(p => p.AssetName)
                .NotEmpty().MinimumLength(5);
            RuleFor(p => p.Department)
                .NotEmpty().IsInEnum();
            RuleFor(p => p.CountryOfDepartment)
                .NotEmpty().Must(CountryIsValid);
            RuleFor(p => p.EMailAddress)
                .NotEmpty().EmailAddress()
                .Matches("/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$/");
            RuleFor(p => p.PurchaseDate)
                .NotEmpty().Must(NotOlderThanAYear);
        }

        private bool NotOlderThanAYear(DateTime pDate)
        {
            var oneYearAgo = DateTime.Today.AddYears(-1);
            return pDate >= oneYearAgo;
        }

        private bool CountryIsValid(string pCountry)
        {
            var countryName = _httpClient.GetCountryByName(pCountry).Result;
            if (countryName != string.Empty)
            {
                return true;
            }
            return false;
        }

    }
}
