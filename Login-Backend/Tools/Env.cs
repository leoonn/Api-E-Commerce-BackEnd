using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Tools
{
    public class Env
    {
        public static void Load(string filePath)
        {
            Debug.WriteLine("Start123");
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                Debug.WriteLine("Passou");
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }

        public static void Load()
        {
            var appRoot = @"C:\Users\USERS\OneDrive\Documentos\GitHub\Login-Backend\";
            Debug.WriteLine(appRoot);
            var dotEnv = Path.Combine(appRoot, "Env.env");

            Load(dotEnv);
        }
    }
}
