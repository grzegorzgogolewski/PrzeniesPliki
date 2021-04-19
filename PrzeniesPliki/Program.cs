using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PrzeniesPliki
{
    static class Program
    {
        static void Main()
        {
            string startDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string[] files = Directory.GetFiles(startDirectory ?? throw new InvalidOperationException(), "*_*.*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);

                int marker = fileName.IndexOf('_');

                if (marker > 0)
                {
                    string directory = fileName.Substring(0, marker);

                    directory = Regex.Replace(directory, "[^0-9]", "");

                    directory = directory.PadLeft(4, '0');

                    string[] destination = Directory.GetDirectories(startDirectory, directory + "_*", SearchOption.AllDirectories);

                    if (destination.Length == 1)
                    {
                        File.Move(file, Path.Combine(destination[0], fileName));    

                        Console.WriteLine(file + " => " + Path.Combine(destination[0], fileName));
                    }
                }
            }

            Console.WriteLine("Koniec!");

            Console.ReadKey(false);
        }
    }
}
