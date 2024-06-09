using Newtonsoft.Json;
using OOPNET_Project_Assignment.Moduls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OOPNET_Project_Assignment.DataLayer
{
    public class RepoFactoryTeam : IRepoTeam
    {
        private readonly HttpClient _client;
        private static string baseUrl = "https://worldcup-vua.nullbit.hr";
        public RepoFactoryTeam()
        {
            _client = new HttpClient();
        }

        public async Task<List<Team>> GetTeamsAsyncFile(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string jsonResponse = await reader.ReadToEndAsync();

                    List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(jsonResponse);

                    return teams;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when recieving a data: {ex.Message}");
            }
        }

        public async Task<List<Team>> GetTeamsAsyncSwagger(string url)
        {
            string endpoint = url + "/teams";

            HttpResponseMessage response = await _client.GetAsync(baseUrl + endpoint);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(jsonResponse);
                return teams;
            }
            else
            {
                throw new Exception($"Error when recieving a data: {response.StatusCode}");
            }
        }
    }
}
