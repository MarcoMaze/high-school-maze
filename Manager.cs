using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TestForMaze4
{
    class Manager
    {
        //Kör programmet genom att kalla på alla metoder som behöver kallas på utifrån vilka val användaren gjort
        public static void RunProgam()
        {
            Startup.RunStartUp();

            Console.Clear();

            if (Information.testMode == "NormalMode")
            {
                Information.currentMessage = "Generating maze";

                Generator.RunGenerator();

                Information.currentMessage = "Generation finished, press any key to continue";
                Draw.DrawCells();

                Console.ReadKey();

                Information.currentMessage = "Solving";
                Draw.DrawCells();

                if (Information.useRightSolver)
                {
                    Solver.SolveWithRightWallFollower();
                }
                else if (Information.useLeftSolver)
                {
                    Solver.SolveWithLeftWallFollower();
                }
                else if (Information.useRecursiveSolver)
                {
                    Solver.RecursiveSolver(Information.FindCellAtPosition(Information.xStartingPosition, Information.yStartingPosition), Information.FindCellAtPosition(Information.xStartingPosition, Information.yStartingPosition));
                }

                Information.currentMessage = "Solving finished, press any key to continue";
                Draw.DrawCells();

                Console.ReadKey();
            }
            else if (Information.testMode == "MassMode")
            {
                PerformanceTest.TestSolvingPerformance();
            }
        }
    }
}