using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Robot_Wars
{
    public class Robot
    {
        public readonly static Vector North = new Vector(0, 1);
        public readonly static Vector East = new Vector(1, 0);
        public readonly static Vector South = new Vector(0, -1);
        public readonly static Vector West = new Vector(-1, 0);

        public static Vector currentPosition = new Vector(0, 0);
        public static Vector currentDirection = North;

        public static Matrix rotateLeft = new Matrix(0, 1, -1, 0, 0, 0);
        public static Matrix rotateRight = new Matrix(0, -1, 1, 0, 0, 0);

        public static bool SetStartState(string state)
        {
            bool isStringValid = IsInitialstateStringValid(state);
            bool isPositionValid = IsInitialstatePositionValid(state);
            if (!(isStringValid && isPositionValid))
            {
                Console.WriteLine("Please enter a valid initial state.");
                return false;
            }
            return true;
        }

        public static bool SetRobotState(string instructions)
        {
            bool isInstructionsValid = IsInstructionsStringValid(instructions);
            if (!isInstructionsValid)
            {
                Console.WriteLine("Please enter a valid set of instructions.");
                return false;
            }
            return true;
        }

        public static string MoveRobot(string instructions)
        {
            foreach (var cha in instructions)
            {
                if (cha.ToString() == "L")
                {
                    currentDirection = Vector.Multiply(currentDirection, rotateLeft);
                }
                else if (cha.ToString() == "R")
                {
                    currentDirection = Vector.Multiply(currentDirection, rotateRight);
                }
                else
                {
                    Vector potentialPosition = new Vector();
                    potentialPosition = Vector.Add(currentPosition, currentDirection);
                    bool outsideWidth = potentialPosition.X < 0 || potentialPosition.X > Grid.Width;
                    bool outsideLength = potentialPosition.Y < 0 || potentialPosition.Y > Grid.Length;
                    if (outsideWidth || outsideLength)
                    {
                        return "Your instructions will cause the Robot to leave the Grid area. Please re-enter the instructions.";
                    }
                    else
                    {
                        currentPosition = potentialPosition;
                    }
                }
            }
            return FinalState();
        }

        public static string FinalState()
        {
            string direction = String.Empty;
            if (currentDirection == North)
            {
                direction = "N";
            }
            else if (currentDirection == East)
            {
                direction = "E";
            }
            else if (currentDirection == South)
            {
                direction = "S";
            }
            else
            {
                direction = "W";
            }
            return currentPosition.X.ToString() + " " + currentPosition.Y.ToString() + " " + direction;
        }

        public static bool IsInitialstateStringValid(string initialstate)
        {
            bool match = Regex.IsMatch(initialstate, @"^[\d]*\s[\d]*\s[N,E,S,W]$");

            return match;
        }
        public static bool IsInitialstatePositionValid(string initialstate)
        {
            string[] subs = initialstate.Split(' ');
            try
            {
                var x = ushort.Parse(subs[0]);
                var y = ushort.Parse(subs[1]);
                if (x <= Grid.Width && y <= Grid.Length)
                {
                    currentPosition = currentPosition + new Vector(x, y);
                    switch (subs[2])
                    {
                        case "N":
                            currentDirection = North;
                            break;
                        case "E":
                            currentDirection = East;
                            break;
                        case "S":
                            currentDirection = South;
                            break;
                        case "W":
                            currentDirection = West;
                            break;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (OverflowException)
            {
                return false;
            }
            return true;
        }
        public static bool IsInstructionsStringValid(string instructions)
        {
            bool match = Regex.IsMatch(instructions, @"^[M,L,R]*$");

            return match;
        }
    }
}
