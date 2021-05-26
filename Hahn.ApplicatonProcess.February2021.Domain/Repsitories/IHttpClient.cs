using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain.Repsitories
{
    public interface IHttpClient
    {
        Task<string> GetCountryByName(string countryName);
    }
}
