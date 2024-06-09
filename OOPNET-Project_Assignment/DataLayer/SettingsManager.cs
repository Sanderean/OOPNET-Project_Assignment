using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SettingsManager
    {

        public static string GetSettingsPath(string relativePath)
        {
            // Получаем путь к директории, содержащей сборку (dll)
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            // Переходим на уровень выше, чтобы получить директорию проекта
            string projectDirectory = Directory.GetParent(assemblyDirectory).Parent.Parent.FullName;

            // Соединяем путь к директории проекта с относительным путем к файлу настроек
            return Path.Combine(projectDirectory, relativePath);
        }
    }
}
