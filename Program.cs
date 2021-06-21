using System;
using System.IO;
using System.Threading.Tasks;
using DirectionsApp.Models;

namespace DirectionsApp
{
    class Program
    {
        public static System.Collections.Specialized.NameValueCollection AppSettings { get; }
        static async Task Main(string[] args)
        {
            var userParameters = ParametersService.GetParameters();

            var directionsResponse = await DirectionService.GetDirectionsAsync(userParameters);

            OutputResults(directionsResponse, userParameters);
        }

        private static void OutputResults(Directions directionsResponse, Parameters UserParameters)
        {
            if (!string.IsNullOrEmpty(UserParameters.FileName))
            {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                using (var outputFile = new StreamWriter(Path.Combine(docPath, UserParameters.FileName)))
                {
                    outputFile.WriteLine("Start: " + directionsResponse.Start + "| End: " + directionsResponse.End);
                    outputFile.WriteLine("Total Distance: " + directionsResponse.TotalDistance);
                    outputFile.WriteLine("Total Duration: " + directionsResponse.TotalDuration);
                    outputFile.WriteLine(string.Join("", directionsResponse.Route));
                }
            }
            else
            {
                Console.WriteLine("Start: " + directionsResponse.Start + "| End: " + directionsResponse.End);
                Console.WriteLine("Total Distance: " + directionsResponse.TotalDistance);
                Console.WriteLine("Total Duration: " + directionsResponse.TotalDuration);
                Console.WriteLine(string.Join("", directionsResponse.Route));
                Console.WriteLine(string.Join("", directionsResponse.MapURL));
            }
        }
    }
}
