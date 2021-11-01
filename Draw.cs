using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestForMaze4
{
    class Draw
    {
        //Ritar ut rätt sak för alla ställen i fönstret
        public static void DrawCells()
        {
            Console.BackgroundColor = ConsoleColor.Black;

            for (int i = 0; i < Information.allCells.Length; i++)
            {
                if (Information.allCells[i].wall)
                {
                    Console.BackgroundColor = Information.wallColor;
                }
                if (Information.allCells[i].aisle)
                {
                    Console.BackgroundColor = Information.aisleColor;
                }
                /*if (Information.allCells[i].available)
                {
                    Console.BackgroundColor = Information.availableColor; //Sätter alla celler son är Available till bestämd färg - för debugsyften
                }*/
                if (Information.allCells[i].considered)
                {
                    Console.BackgroundColor = Information.consideredColor;
                }
                if (Information.allCells[i].visitedBySolver)
                {
                    Console.BackgroundColor = Information.visitedColor;
                }
                if (Information.allCells[i].current)
                {
                    Console.BackgroundColor = Information.currentPositionColor;
                }
                if (Information.allCells[i].isExit)
                {
                    Console.BackgroundColor = Information.exitColor;
                }

                if (Information.allCells[i].currentColor != Console.BackgroundColor)
                {
                Console.SetCursorPosition(Information.allCells[i].xPosition, Information.allCells[i].yPosition);
                Console.Write(" ");
                }

                Information.allCells[i].currentColor = Console.BackgroundColor;
            }
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;


            //Ritar ut en "topMessage" ovanför labyrinten
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Information.widthOfMaze - 1; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(Information.currentTopMessage);


            //Ritar ut "currentMessage" under labyrinten
            Console.SetCursorPosition(0, Information.heightOfMaze -1);
            for (int i = 0; i < Information.widthOfMaze-1; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(0, Information.heightOfMaze -1);
            Console.Write(Information.currentMessage);

            Thread.Sleep(Information.drawCellsDelay); //För debugsyften, delay ställs in i Information
        }
    }
}
