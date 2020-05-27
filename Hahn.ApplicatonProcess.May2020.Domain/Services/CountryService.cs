using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.May2020.Domain.Services
{
    public class CountryService : ICountryService
    {
        static HttpClient client = new HttpClient();
        private string path = "https://restcountries.eu/rest/v2/name/?fullText=true";
        public async Task<bool> isExistsAsync(string countryName)
        {
            path = $"https://restcountries.eu/rest/v2/name/{countryName}?fullText=true";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
