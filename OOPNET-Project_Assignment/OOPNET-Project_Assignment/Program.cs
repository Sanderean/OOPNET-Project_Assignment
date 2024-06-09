using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using OOPNET_Project_Assignment.Moduls;
using OOPNET_Project_Assignment.DataLayer;
using System.Threading;

namespace OOPNET_Project_Assignment
{
    internal static class Program
    {
        private static string baseUrl = "https://worldcup-vua.nullbit.hr";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            System.Windows.Forms.Application.Run(new Application());
        }

        public static string GetUrl()
        {
            return baseUrl;
        }
    }
}
