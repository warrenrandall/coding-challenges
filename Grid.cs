using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Robot_Wars
{
    public static class Grid
    {
        public static ushort Width { get; set; }
        public static ushort Length { get; set; }

        public static void SetGridSize(string gridinput)
        {
            if (IsGridStringValid(gridinput) && IsGridDimensionsValid(gridinput))
            {
                string[] subs = gridinput.Split(' ');
                Width = ushort.Parse(subs[0]);
                Length = ushort.Parse(subs[1]);
            }
            else
            {
                Console.WriteLine("Please enter valid grid dimensions.");
            }

        }

        public static bool IsGridStringValid(string gridinput)
        {
            bool match = Regex.IsMatch(gridinput, @"^[\d]*\s[\d]*$");

            return match;
        }

        public static bool IsGridDimensionsValid(string gridinput)
        {
            string[] subs = gridinput.Split(' ');
            bool isValid = true;
            try
            {
                ushort width = UInt16.Parse(subs[0]);
                ushort length = UInt16.Parse(subs[1]);
            }
            catch (OverflowException)
            {
                isValid = false;
            }
            return isValid;
        }
    }
}
