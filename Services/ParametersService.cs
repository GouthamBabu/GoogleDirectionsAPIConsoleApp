using DirectionsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirectionsApp
{
    public static class ParametersService
    {
        public static Parameters GetParameters()
        {
                var UserParameters = new Parameters();

                UserParameters.Start = GetInput("Enter Starting Location: ");
                UserParameters.End = GetInput("Enter Destination: ");
                UserParameters.FileName = GetFileName();
                UserParameters.map = Getmap();
                return UserParameters;
        }

        private static string GetInput(string message)
        {
            Console.Write(message);
            string value = "";
            do
            {
                value = Console.ReadLine();
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Input required. " + message);
                }

            } while (string.IsNullOrEmpty(value));
            return value;
        }

        private static string GetFileName()
        {
            bool valid = false;
            do
            {
                Console.Write("Do you want to output to a file? (Yes or No) ");
                string outputfile = Console.ReadLine();
                if (outputfile.ToLower() == "yes")
                {
                    valid = true;
                    Console.Write("Name your file: ");
                    string fileName = Console.ReadLine() + ".text";
                    return fileName;
                }
                else if (outputfile.ToLower() == "no")
                {
                    valid = true;
                }
                else
                {
                    Console.Write("Invalid, please enter Yes or No, ");
                }
            } while (!valid);
            return null;
        }


        private static bool Getmap()
        {
           Console.Write("Would you like to add a map?(Yes or No)");
            bool map = false;
            var inputString = Console.ReadLine();
            if (inputString.ToLower() == "yes")
            {
                map = true;
            }
            if (inputString.ToLower() == "no")
            {
                map = false;
            }
            return map;
        }
    }       
}
