using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TestForMaze4
{
    class PerformanceTest
    {
        public static bool isdone = false;

        public static void TestSolvingPerformance()
        {
            Console.Clear();

            Console.WriteLine("Testing started");

            Stopwatch stopWatch = new Stopwatch();

            //Skapar labyrinten och testar hur lång tid det tar för att lösa dem med hjälp av de lösningsmetoder som valts av användaren
            for (int i = 0; i < Information.timesToTest; i++)
            {
                Generator.RunGenerator();

                if (Information.useRightSolver)
                {
                    //Console.WriteLine("Right solver is being tested");
                    stopWatch.Start();
                    Solver.SolveWithRightWallFollower();
                    stopWatch.Stop();

                    Information.rightSolvingResultsInMilliseconds.Add(stopWatch.ElapsedMilliseconds);
                    Information.rightSolvingResultsInTicks.Add(stopWatch.ElapsedTicks);

                    stopWatch.Reset();
                    Information.ResetSolve();
                }
                if (Information.useLeftSolver)
                {
                    //Console.WriteLine("Left solver is being tested");
                    stopWatch.Start();
                    Solver.SolveWithLeftWallFollower();
                    stopWatch.Stop();

                    Information.leftSolvingResultsInMilliseconds.Add(stopWatch.ElapsedMilliseconds);
                    Information.leftSolvingResultsInTicks.Add(stopWatch.ElapsedTicks);

                    stopWatch.Reset();
                    Information.ResetSolve();
                }
                if (Information.useRecursiveSolver)
                {
                    //Console.WriteLine("Recursive solver is being tested");
                    stopWatch.Start();
                    Solver.RecursiveSolver(Information.FindCellAtPosition(Information.xStartingPosition, Information.yStartingPosition), Information.FindCellAtPosition(Information.xStartingPosition, Information.yStartingPosition));
                    stopWatch.Stop();

                    Information.recursiveSolvingResultsInMilliseconds.Add(stopWatch.ElapsedMilliseconds);
                    Information.recursiveSolvingResultsInTicks.Add(stopWatch.ElapsedTicks);

                    stopWatch.Reset();
                    Information.ResetSolve();
                }

                Console.WriteLine("Maze number " + (i + 1) + " has been tested");

                Information.ResetholeBoard();
            }

            //Räknar ut medelvärden för hur lång tid varje lösningsmetod tog
            if (Information.useRightSolver)
            {
                Information.rightAverageInMilliseconds = Information.CalculateAvergareTime(Information.rightSolvingResultsInMilliseconds);
                Information.rightAverageInTicks = Information.CalculateAvergareTime(Information.rightSolvingResultsInTicks);
            }
            if (Information.useLeftSolver)
            {
                Information.leftAverageInMilliseconds = Information.CalculateAvergareTime(Information.leftSolvingResultsInMilliseconds);
                Information.leftAverageInTicks = Information.CalculateAvergareTime(Information.leftSolvingResultsInTicks);
            }
            if (Information.useRecursiveSolver)
            {
                Information.recursiveAverageInMilliseconds = Information.CalculateAvergareTime(Information.recursiveSolvingResultsInMilliseconds);
                Information.recursiveAverageInticks = Information.CalculateAvergareTime(Information.recursiveSolvingResultsInTicks);
            }


            Console.Clear();

            Console.WriteLine("Average time for solving in milliseconds:");

            if (Information.useRightSolver)
            {
                Console.WriteLine("Right hand solving: " + Information.rightAverageInMilliseconds);
            }
            if (Information.useLeftSolver)
            {
                Console.WriteLine("Left hand solving: " + Information.leftAverageInMilliseconds);
            }
            if (Information.useRecursiveSolver)
            {
                Console.WriteLine("Recursive solving: " + Information.recursiveAverageInMilliseconds);
            }

            Console.WriteLine();

            Console.WriteLine("Average time for solving in ticks:");

            if (Information.useRightSolver)
            {
                Console.WriteLine("Right hand solving: " + Information.rightAverageInTicks);
            }
            if (Information.useLeftSolver)
            {
                Console.WriteLine("Left hand solving: " + Information.leftAverageInTicks);
            }
            if (Information.useRecursiveSolver)
            {
                Console.WriteLine("Recursive solving: " + Information.recursiveAverageInticks);
            }

            Console.WriteLine();

            Console.WriteLine("Please select if you would like to save the results to a file in your documents folder, type the corresponding number to your choise");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            isdone = false;
            while (isdone == false)
            {
                string input = Console.ReadLine();

                if (input == "1")
                {
                    Save.SaveResults();
                    isdone = true;
                }
                else if (input == "2")
                {
                    isdone = true;
                }
                else
                {
                    Console.WriteLine("Your input was in an incorrect format, please type 1 or 2");
                }
            }
        }
    }
}

