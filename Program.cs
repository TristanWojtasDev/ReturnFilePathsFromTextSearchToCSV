using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Collections.Generic;
using CsvHelper;

namespace ReturnFilePathsFromTextSearch
{








    class Program
    {

        public class AppNameSearchDestination
        {
            public string AppName { get; set; }
            public string SearchCriteria { get; set; }
            public string FilePath { get; set; }
        }
        static void Main(string[] args)
        {
            // Open text file and create string array of all of the batch files being used

            string[] textInput = System.IO.File.ReadAllLines(@"input.txt");
            string[] path = System.IO.File.ReadAllLines(@"path.txt");
            string[] allFiles = { };
            IDictionary<string, string> d = new Dictionary<string, string>();
            List < AppNameSearchDestination > finalProduct = new List <AppNameSearchDestination>();
            string[] stringArray = new string[2];

            foreach (string line in textInput)
            {

                if (line.Contains("name"))
                {
                     stringArray[0] = line.Substring(line.LastIndexOf(':') + 1);
                }
                if (line.Contains("searchcriteria"))
                { 
                    stringArray[1] = line.Substring(line.LastIndexOf(':') + 1);
                }
                if (!String.IsNullOrEmpty(stringArray[0])&& !String.IsNullOrEmpty(stringArray[1]))
                {
                    d.Add(new KeyValuePair<string, string>(stringArray[0], stringArray[1]));
                    stringArray[0] = null;
                    stringArray[1] = null;
                }

            }


            // Have application search directory and all subdirectories for matches on the array

            foreach (var item in d)
            {
                // Input starting directory
                
                string toSearchOn = "*" + item.Value;

                // If there is a match append the filename, filepath, and search criteria it matched on

                allFiles = Directory.GetFiles(path[0], toSearchOn, SearchOption.AllDirectories);
                foreach(var file in allFiles)
                {
                    var _AppNameSearchDestination = new AppNameSearchDestination();
                    _AppNameSearchDestination.AppName = item.Key;
                    _AppNameSearchDestination.SearchCriteria = item.Value;
                    _AppNameSearchDestination.FilePath = file;
                    finalProduct.Add(_AppNameSearchDestination);
                }
            }

            // To Console

            foreach(var item in finalProduct)
            {
                Console.WriteLine("Application: {0} Search Criteria: {1} FilePath {2}", item.AppName, item.SearchCriteria, item.FilePath);
            }


            // Output to text file or csv file

            string[] outputPath = System.IO.File.ReadAllLines(@"outputpath.txt");
            string outputFullPath = Path.Combine(outputPath[0], "csvoutput.csv");
            using (var writer = new StreamWriter(outputFullPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(finalProduct);
            }

            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}
