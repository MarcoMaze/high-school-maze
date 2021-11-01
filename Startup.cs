using System;
using System.Collections.Generic;
using System.Text;

namespace TestForMaze4
{
    class Startup
    {
        public static void RunStartUp() //Körs från Manager
        {
            Console.SetBufferSize(1920, 1080);

            //Hämtar in all information från användaren
            
            bool isDone;

            Console.Title = "Maze Amaze V. 3";

            Console.WriteLine("Select your mode, type the corresponding number to your choise");
            Console.WriteLine("1. Normal mode - generate one maze and use one type of solving");
            Console.WriteLine("2. Test mode - test the time the different solutions take on multiple mazed");

            isDone = false;
            while (isDone == false)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    Information.testMode = "NormalMode";
                    isDone = true;
                }
                else if (input == "2")
                {
                    Information.testMode = "MassMode";
                    isDone = true;
                }
                else
                {
                    Console.WriteLine("You input was is an incorrect format, please type 1 or 2");
                }
            }

            Console.WriteLine();

            if (Information.testMode == "NormalMode")
            {
                Console.WriteLine("Please type you desired width for the maze (10-225)");
                isDone = false;
                while (isDone == false)
                {
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int inputInt))
                    {
                        if (inputInt <= 225 && inputInt >= 10)
                        {
                            Information.widthOfMaze = inputInt;
                            isDone = true;
                        }
                        else
                        {
                            Console.WriteLine("You input was in an incorrect format, please type a number between 10 and 225");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You input was in an incorrect format, please type a number between 10 and 225");
                    }
                }

                Console.WriteLine();

                Console.WriteLine("Please type your desired height for the maze(10-55)");
                isDone = false;
                while (isDone == false)
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int inputInt))
                    {
                        if (inputInt <= 55 && inputInt >= 10)
                        {
                            Information.heightOfMaze = inputInt;
                            isDone = true;
                        }
                        else
                        {
                            Console.WriteLine("You input was in an incorrect format, please type a number between 10 and 55");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Yout input was in an incorrect format, please type a number between 10 and 55");
                    }
                }

                Console.WriteLine();

                Console.WriteLine("Please select if you would like to see the maze being generated, type the corresponding number to your choise");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");

                isDone = false;
                while (isDone == false)
                {
                    string input = Console.ReadLine();

                    if (input == "1")
                    {
                        Information.printAtGeneration = true;
                        isDone = true;
                    }
                    else if (input == "2")
                    {
                        Information.printAtGeneration = false;
                        isDone = true;
                    }
                    else
                    {
                        Console.WriteLine("Your input was in an incorrect format, please type 1 or 2");
                    }
                }

                Console.WriteLine();

                Console.WriteLine("Please select if you would like to see the maze being solved, type the corresponding number to your choise");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                isDone = false;
                while (isDone == false)
                {
                    string input = Console.ReadLine();

                    if (input == "1")
                    {
                        Information.printAtSolving = true;
                        isDone = true;
                    }
                    else if (input == "2")
                    {
                        Information.printAtSolving = false;
                        isDone = true;
                    }
                    else
                    {
                        Console.WriteLine("Your input was in an incorrect format, please type 1 or 2");
                    }
                }

                Console.WriteLine();

                Console.WriteLine("Please select the solving type for the maze, type the corresponding number to your choise");
                Console.WriteLine("1. Right hand");
                Console.WriteLine("2. Left hand");
                Console.WriteLine("3. Recursive");
                isDone = false;
                while (isDone == false)
                {
                    string input = Console.ReadLine();

                    if (input == "1")
                    {
                        Information.useRightSolver = true;
                        isDone = true;
                    }
                    else if (input == "2")
                    {
                        Information.useLeftSolver = true;
                        isDone = true;
                    }
                    else if (input == "3")
                    {
                        Information.useRecursiveSolver = true;
                        isDone = true;
                    }
                    else
                    {
                        Console.WriteLine("You input was in an incorrect format, please type 1, 2 or 3");
                    }

                    Console.WriteLine();
                }
                /*Console.WriteLine("If you would like to see the maze being solved type y, if not; just type something else");
                if (Console.ReadLine() == "y")
                {
                    Information.printAtSolving = true;
                }
                else
                {
                    Information.printAtSolving = false;
                }
                Console.WriteLine();
                */

                Console.WriteLine("Move the window to the top left corner of the scren; do not resize the window at any time");

                Console.WriteLine("Press any key to continue to generation");
                Console.ReadKey();

                Console.SetWindowSize(Information.widthOfMaze + Information.extraWidth, Information.heightOfMaze + Information.extraHeight);
            }
            else if (Information.testMode == "MassMode")
            {
                Console.WriteLine("Please type you desired width for the maze (10-255)");
                isDone = false;
                while (isDone == false)
                {
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int inputInt))
                    {
                        if (inputInt <= 255 && inputInt >= 10)
                        {
                            Information.widthOfMaze = inputInt;
                            isDone = true;
                        }
                        else
                        {
                            Console.WriteLine("You input was in an incorrect format, please type a number between 10 and 225");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You input was in an incorrect format, please type a number between 10 and 225");
                    }
                }

                Console.WriteLine();

                Console.WriteLine("Please type your desired height for the maze(10-55)");
                isDone = false;
                while (isDone == false)
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int inputInt))
                    {
                        if (inputInt <= 55 && inputInt >= 10)
                        {
                            Information.heightOfMaze = inputInt;
                            isDone = true;
                        }
                        else
                        {
                            Console.WriteLine("You input was in an incorrect format, please type a number between 10 and 55");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Yout input was in an incorrect format, please type a number between 10 and 55");
                    }
                }

                Console.WriteLine();

                Console.WriteLine("Please select if you would like to test the right hand solving method, type the corresponding number to your choise");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                isDone = false;
                while (isDone == false)
                {
                    string input = Console.ReadLine();

                    if (input == "1")
                    {
                        Information.useRightSolver = true;
                        isDone = true;
                    }
                    else if (input == "2")
                    {
                        Information.useRightSolver = false;
                        isDone = true;
                    }
                    else
                    {
                        Console.WriteLine("You input was in an incorrect format, please type 1 or 2");
                    }
                }

                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("Please seleft if you would like to test the left hand solving method, type the corresponding number to your choise");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                isDone = false;
                while (isDone == false)
                {
                    string input = Console.ReadLine();

                    if (input == "1")
                    {
                        Information.useLeftSolver = true;
                        isDone = true;
                    }
                    else if (input == "2")
                    {
                        Information.useLeftSolver = false;
                        isDone = true;
                    }
                    else
                    {
                        Console.WriteLine("Your input was in an incorrect format, please type 1 or 2");
                    }
                }

                Console.WriteLine();

                Console.WriteLine("Please select if you would like to test the recursive solving method, type the corresponding number to your choise");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                isDone = false;
                while (isDone == false)
                {
                    string input = Console.ReadLine();

                    if (input == "1")
                    {
                        Information.useRecursiveSolver = true;
                        isDone = true;
                    }
                    else if (input == "2")
                    {
                        Information.useRecursiveSolver = false;
                        isDone = true;
                    }
                    else
                    {
                        Console.WriteLine("Your input was inan incorrect format, please type 1 or 2");
                    }
                }

                Console.WriteLine();

                Console.WriteLine("Please type how many mazes you would like to test the solving methods on (1-10000)");
                isDone = false;
                while (isDone == false)
                {
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int inputInt))
                    {
                        if (inputInt <= 10000 && inputInt >= 1)
                        {
                            Information.timesToTest = inputInt;
                            isDone = true;
                        }
                        else
                        {
                            Console.WriteLine("You input was in an incorrect format, please type a number between 1 and 10 000");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You input was in an incorrect format, please type a number between 1 and 10 000");
                    }
                }

                Console.WriteLine("Press any key to continue to tesing");
                Console.ReadKey();
            }
        }
    }
}
