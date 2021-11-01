using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security;

namespace TestForMaze4
{
    class Save
    {
        public static string saveFilePath = @"C:\Users\" + Environment.UserName + @"\Documents\MazeResults.txt";
        public static List<string> output = new List<string>();

        //Sparar all testdata som sparats från test-läget
        public static void SaveResults()
        {
            output.Add("Average time for solving in milliseconds");
            if (Information.useRightSolver)
            {
                output.Add("Right hand solving: " + Information.rightAverageInMilliseconds);
            }
            if (Information.useLeftSolver)
            {
                output.Add("Left hand solving: " + Information.leftAverageInMilliseconds);
            }
            if (Information.useRecursiveSolver)
            {
                output.Add("Recursive solving: " + Information.recursiveAverageInMilliseconds);
            }

            output.Add("");

            output.Add("Average time for solving in ticks:");
            if (Information.useRightSolver)
            {
                output.Add("Right hand solving: " + Information.rightAverageInTicks);
            }
            if (Information.useLeftSolver)
            {
                output.Add("Left hand solving: " + Information.leftAverageInTicks);
            }
            if (Information.useRecursiveSolver)
            {
                output.Add("Recursive solving: " + Information.recursiveAverageInticks);
            }

            File.WriteAllLines(saveFilePath, output);
        }
    }
}
