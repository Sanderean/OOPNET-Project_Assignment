using OOPNET_Project_Assignment.Moduls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataLayer
{
    public interface IRepoResults
    {
        Task<List<Results>> GetResultsAsyncSwagger(string url);
        Task<List<Results>> GetResultsAsyncFile(string filePath);
    }
}
