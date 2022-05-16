using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Wars
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter Grid dimensions: width (x) followed by a single space followed by length (y):\n");
            Grid.SetGridSize(Console.ReadLine());
            Console.WriteLine("If at any time you wish to terminate the application please type Q and then hit the Return key.\n");
            string line = String.Empty;
            bool notQuitting = !line.ToUpper().Contains("Q");
            for (int i = 1; ; i++)
            {
                bool isStateInvalid = true;
                bool isPathInvalid = true;
                if (!notQuitting) break;
                while (notQuitting && isStateInvalid)
                {
                    Console.WriteLine("Player {0}", i.ToString());
                    Console.WriteLine("Type the Robot's initial state in the form of x coordinate, y coordinate and cardinal compass point.\n");
                    isStateInvalid = !Robot.SetStartState(Console.ReadLine());
                }
                while (notQuitting && isPathInvalid)
                {
                    Console.WriteLine("Please type the instructions for the Robot:\n");
                    isPathInvalid = !Robot.SetRobotState(line = Console.ReadLine());
                }
                Console.WriteLine("Final state of Robot: {0}\n", Robot.MoveRobot(line));
            }
            Console.WriteLine("Program terminated");
            Console.ReadLine();
        }
    }
}
