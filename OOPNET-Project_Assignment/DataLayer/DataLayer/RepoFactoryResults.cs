using Newtonsoft.Json;
using OOPNET_Project_Assignment.Moduls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataLayer
{
    public class RepoFactoryResults : IRepoResults
    {
        private readonly HttpClient _client;
        private static string baseUrl = "https://worldcup-vua.nullbit.hr";
        public RepoFactoryResults()
        {
            _client = new HttpClient();
        }
        public async Task<List<Results>> GetResultsAsyncFile(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string jsonResponse = await reader.ReadToEndAsync();

                    List<Results> results = JsonConvert.DeserializeObject<List<Results>>(jsonResponse);

                    return results;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when recieving a data: {ex.Message}");
            }
        }

        public async Task<List<Results>> GetResultsAsyncSwagger(string url)
        {
            string endpoint = url + "/teams/results";

            HttpResponseMessage response = await _client.GetAsync(baseUrl + endpoint);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Results> results = JsonConvert.DeserializeObject<List<Results>>(jsonResponse);
                return results;
            }
            else
            {
                throw new Exception($"Error when recieving a data: {response.StatusCode}");
            }
        }
    }
}
