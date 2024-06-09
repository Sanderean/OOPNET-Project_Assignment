using Newtonsoft.Json;
using OOPNET_Project_Assignment.Moduls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OOPNET_Project_Assignment.DataLayer
{
    public class RepoFactoryCountry : IRepoCountry
    {
        private readonly HttpClient _client;
        private static string baseUrl = "https://worldcup-vua.nullbit.hr";
        public RepoFactoryCountry()
        {
            _client = new HttpClient();
        }

        public async Task<List<Country.Root>> GetCountryAsyncFile(string fifa, string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string jsonResponse = await reader.ReadToEndAsync();

                    List<Country.Root> roots = JsonConvert.DeserializeObject<List<Country.Root>>(jsonResponse);

                    return roots;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when reading data from file: {ex.Message}");
            }
        }

        public async Task<List<Country.Root>> GetCountryAsyncSwagger(string fifa_code, string url)
        {
            string endpoint = url + $"/matches/country";

            HttpResponseMessage response = await _client.GetAsync($"{baseUrl}{endpoint}?fifa_code={fifa_code}");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Country.Root> roots = JsonConvert.DeserializeObject<List<Country.Root>>(jsonResponse);
                return roots;

            }
            else
            {
                throw new Exception($"Error when recieving a data: {response.StatusCode}");
            }

        }
    }
}
