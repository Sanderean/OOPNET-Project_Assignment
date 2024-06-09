using OOPNET_Project_Assignment.Moduls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPNET_Project_Assignment.DataLayer
{
    public interface IRepoTeam
    {
        Task<List<Team>> GetTeamsAsyncSwagger(string url);
        Task<List<Team>> GetTeamsAsyncFile(string filePath);
    }
}
