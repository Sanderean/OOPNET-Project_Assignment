using Newtonsoft.Json;
using OOPNET_Project_Assignment.Moduls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OOPNET_Project_Assignment.DataLayer
{
    public interface IRepoCountry
    {
        Task<List<Country.Root>> GetCountryAsyncSwagger(string fifa, string url);
        Task<List<Country.Root>> GetCountryAsyncFile(string fifa, string filePath);
    }
}
