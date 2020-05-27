using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Domain.Services
{
    public interface ICountryService
    {
          Task<bool> isExistsAsync(string countryName);
    }
}
