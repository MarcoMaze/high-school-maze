using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestForMaze4
{
    class Information
    {
        //Här sätter man färger som ska användas när informationen ritas ut
        public static ConsoleColor wallColor = ConsoleColor.Black;
        public static ConsoleColor aisleColor = ConsoleColor.White;
        public static ConsoleColor currentPositionColor = ConsoleColor.Red;
        public static ConsoleColor backgroundColorForText = ConsoleColor.DarkBlue;
        public static ConsoleColor textColor = ConsoleColor.White;
        public static ConsoleColor consideredColor = ConsoleColor.DarkGreen;
        public static ConsoleColor exitColor = ConsoleColor.DarkMagenta;
        public static ConsoleColor availableColor = ConsoleColor.Cyan;
        public static ConsoleColor visitedColor = ConsoleColor.DarkGray;

        public static List<string> debugList = new List<string>(); //Lista som används i debugsyften, resultaten hamnar i en textfil

        public static bool solved = false; //Ifalll labyrinten är löst eller inte av en solver

        public static string testMode = "";

        public static bool useRightSolver; //Ifall högerhandslösning ska användas
        public static bool useLeftSolver; //Ifall vänsterhandslösning ska användas
        public static bool useRecursiveSolver; //Ifall rekusiva lösningska användas

        public static int extraWidth = 0; //Extra bredd för konsollfönstret
        public static int extraHeight = 1; //Extra höjd för konsollfönstret

        public static int standardWidth = 50; //Ifall något skulle bli fel i hämtningen från användaren används standardvärden för labyrintens storlek (Används inte just nu)
        public static int standardHeight = 25;

        public static int widthOfMaze; //Storlek på labyrinten
        public static int heightOfMaze;

        public static bool printAtSolving = false; //Ifall konsollfönstret ska uppdateras medans labyrinten skapas eller blir löst
        public static bool printAtGeneration = false;

        public static int drawCellsDelay; //Lägger till en delay i millisekunder mellan varje draw-cykel
        public static int cellInfoDelay; //Lägger till en delay i millisekunder mellan varje värde som skrivs ut där cellers information skrivs ut

        public static int xStartingPosition = 1; //Startposition för lösare och genererare
        public static int yStartingPosition = 1;

        public static int currentXPosition;
        public static int currentYPosition;

        public static string currentMessage; //Text som skrivs ut ovanför labyrinten
        public static string currentTopMessage; //Text som skrivs ut under labyrinten

        public static Cell[] allCells; //Alla cell-objekt
        public static Cell currentCell; //Den cell som just nu är aktiv 

        public static List<Cell> consideredCells = new List<Cell>(); //Alla grannar som blivit hittade men inte valda av generatorn


        public static int timesToTest; //Hur många labyrinten som ska skapas och lösas i testläget

        public static List<long> leftSolvingResultsInMilliseconds = new List<long>(); //Variabler som sparar hur lång tid det tog att lösa de olika labyrinterna i testläget
        public static List<long> rightSolvingResultsInMilliseconds = new List<long>();
        public static List<long> recursiveSolvingResultsInMilliseconds = new List<long>();
        public static List<long> leftSolvingResultsInTicks = new List<long>();
        public static List<long> rightSolvingResultsInTicks = new List<long>();
        public static List<long> recursiveSolvingResultsInTicks = new List<long>();

        public static long leftAverageInMilliseconds;
        public static long rightAverageInMilliseconds;
        public static long recursiveAverageInMilliseconds;
        public static long leftAverageInTicks;
        public static long rightAverageInTicks;
        public static long recursiveAverageInticks;

        //Gör så att storleken på labyrinten alltid är ojämn
        public static void CorrectUneven()
        {
            if (heightOfMaze % 2 == 0)
            {
                heightOfMaze = heightOfMaze += 1;
            }

            if (widthOfMaze % 2 == 0)
            {
                widthOfMaze = widthOfMaze += 1;
            }
        }

        //Skapar en array med storleken av antalet celler som kommer skapas
        public static void CreateCellArray()
        {
            allCells = new Cell[widthOfMaze * heightOfMaze];
        }

        //Skapar alla celler
        public static void CreateCells()
        {
            int cellCount = 0;


            for (int x = 0; x < widthOfMaze; x++)
            {
                for (int y = 0; y < heightOfMaze; y++)
                {
                    Cell createdCell = new Cell(y, x);
                    allCells[cellCount] = createdCell;

                    cellCount++;
                }
            }
        }

        //Skriver ut information om alla celler för debugsyften
        public static void PrintAllCreatedCellsInformaion()
        {
            for (int i = 0; i < allCells.Length; i++)
            {
                Console.WriteLine("Cell at " + "x: " + allCells[i].xPosition + "  y; " + allCells[i].yPosition + " available = " + allCells[i].available);

                Thread.Sleep(cellInfoDelay);
            }
        }

        //Testar ifall det finns en cell på ett visst y- samt x-värde
        public static bool TestIfThereIsCellAtPosition(int _xPosition, int _yPosition)
        {
            for (int i = 0; i < allCells.Length; i++)
            {
                if (allCells[i].xPosition == _xPosition && allCells[i].yPosition == _yPosition)
                {
                    return true;
                }
            }

            return false;
        }

        //returnerar vilken cell som ligger på inskickade koordinater
        public static Cell FindCellAtPosition(int _xPosition, int _yPosition)
        {
            for (int i = 0; i < allCells.Length; i++)
            {
                if (allCells[i].xPosition == _xPosition && allCells[i].yPosition == _yPosition)
                {
                    return Information.allCells[i];
                }
            }

            Console.WriteLine("FindCellAtPosition() could not find a cell with the input values"); //Ifall den inte skulle hitta ett objekt som matchar positionen som skickats in kommer den att stoppa applikationen då detta inte borde hända då det ska vara testat ifall det finns en cell på den positionen som skickas in
            Environment.Exit(0);
            return allCells[0];
        }

        //Kollar ifall det finns en Cell som är available
        public static bool CellsAvailable()
        {
            for (int i = 0; i < allCells.Length; i++)
            {
                if (allCells[i].available == true)
                {
                    return true;
                }
            }

            return false;
        }

        //Returnerar cellen mellan de celler som skickas in
        public static Cell FindCellInbetween(Cell cell1, Cell cell2)
        {
            int returnCellX;
            int returnCellY;

            returnCellX = (cell1.xPosition + cell2.xPosition) / 2;
            returnCellY = (cell1.yPosition + cell2.yPosition) / 2;

            return FindCellAtPosition(returnCellX, returnCellY);
        }

        //Retrnerar en lista med alla grannar som är väggar
        public static List<Cell> FindNeighbouringWalls(Cell centerCell)
        {
            List<Cell> returnList = new List<Cell>();

            //up
            if (TestIfThereIsCellAtPosition(centerCell.xPosition, centerCell.yPosition - 2))
            {
                if (FindCellAtPosition(centerCell.xPosition, centerCell.yPosition - 2).wall)
                {
                    returnList.Add(FindCellAtPosition(centerCell.xPosition, centerCell.yPosition - 2));

                    consideredCells.Add(FindCellAtPosition(centerCell.xPosition, centerCell.yPosition - 2));
                }
            }

            //down
            if (TestIfThereIsCellAtPosition(centerCell.xPosition, centerCell.yPosition + 2))
            {
                if (FindCellAtPosition(centerCell.xPosition, centerCell.yPosition + 2).wall)
                {
                    returnList.Add(FindCellAtPosition(centerCell.xPosition, centerCell.yPosition + 2));

                    consideredCells.Add(FindCellAtPosition(centerCell.xPosition, centerCell.yPosition + 2));
                }

            }

            //left
            if (TestIfThereIsCellAtPosition(centerCell.xPosition - 2, centerCell.yPosition) == true)
            {
                if (FindCellAtPosition(centerCell.xPosition - 2, centerCell.yPosition).wall)
                {
                    returnList.Add(FindCellAtPosition(centerCell.xPosition - 2, centerCell.yPosition));

                    consideredCells.Add(FindCellAtPosition(centerCell.xPosition - 2, centerCell.yPosition));
                }
            }

            //right
            if (TestIfThereIsCellAtPosition(centerCell.xPosition + 2, centerCell.yPosition) == true)
            {
                if (FindCellAtPosition(centerCell.xPosition + 2, centerCell.yPosition).wall)
                {
                    returnList.Add(FindCellAtPosition(centerCell.xPosition + 2, centerCell.yPosition));

                    consideredCells.Add(FindCellAtPosition(centerCell.xPosition + 2, centerCell.yPosition));
                }
            }

            return returnList;
        }

        //De celler som skickas in i litan blir markerade som grannar som blivit hittade men inte valda av cell-objektet som skickas med
        public static void MarkCellsInListConsidered(IList<Cell> cellList, Cell consideredBy)
        {
            for (int i = 0; i < cellList.Count; i++)
            {
                cellList[i].considered = true;
                cellList[i].markedConsideredBy = consideredBy;
            }
        }

        //Tar in en lista celler och skickar tillbaka en slumpvald cell i listan
        public static Cell RandomizeCellFromList(IList<Cell> cellList)
        {
            Random rnd = new Random();
            int chosenCellNumber;
            Cell chosenCell;

            chosenCellNumber = rnd.Next(0, cellList.Count); //+1 för att gränsen ligger "över max"
            chosenCell = cellList[chosenCellNumber];

            return chosenCell;

        }

        //Tar fram en riktning som har celler utifrån en startcell
        public static string FindStartingDirection(Cell startingCell)
        {
            List<Cell> returnList = new List<Cell>();
            Random randomObject = new Random();
            int startingDirectionTest = randomObject.Next(1, (4 + 1));


            if (startingDirectionTest == 1)
            {
                //up
                if (TestIfThereIsCellAtPosition(startingCell.xPosition, startingCell.yPosition - 2))
                {
                    if (FindCellAtPosition(startingCell.xPosition, startingCell.yPosition - 2).aisle)
                    {
                        return "up";
                    }
                }
            }

            if (startingDirectionTest == 2)
            {
                //down
                if (TestIfThereIsCellAtPosition(startingCell.xPosition, startingCell.yPosition + 2))
                {
                    if (FindCellAtPosition(startingCell.xPosition, startingCell.yPosition + 2).aisle)
                    {
                        return "down";
                    }

                }
            }

            if (startingDirectionTest == 3)
            {
                //left
                if (TestIfThereIsCellAtPosition(startingCell.xPosition - 2, startingCell.yPosition) == true)
                {
                    if (FindCellAtPosition(startingCell.xPosition - 2, startingCell.yPosition).aisle)
                    {
                        return "left";
                    }
                }
            }

            if (startingDirectionTest == 4)
            {
                //right
                if (TestIfThereIsCellAtPosition(startingCell.xPosition + 2, startingCell.yPosition) == true)
                {
                    if (FindCellAtPosition(startingCell.xPosition + 2, startingCell.yPosition).aisle)
                    {
                        return "right";
                    }
                }

            }

            return "up";
        }


        //Kollar ifall det finns en gång som är nåbar åt hållet som är specificerat i titeln
        //up
        public static bool CheckAisleUp(Cell currentCell)
        {
            if (TestIfThereIsCellAtPosition(currentCell.xPosition, currentCell.yPosition - 2))
            {
                if (FindCellAtPosition(currentCell.xPosition, currentCell.yPosition - 2).aisle)
                {
                    if (FindCellInbetween(currentCell, FindCellAtPosition(currentCell.xPosition, currentCell.yPosition - 2)).aisle)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //down
        public static bool CheckAisleDown(Cell currentCell)
        {
            if (TestIfThereIsCellAtPosition(currentCell.xPosition, currentCell.yPosition + 2))
            {
                if (FindCellAtPosition(currentCell.xPosition, currentCell.yPosition + 2).aisle)
                {
                    if (FindCellInbetween(currentCell, FindCellAtPosition(currentCell.xPosition, currentCell.yPosition + 2)).aisle)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //left
        public static bool CheckAisleLeft(Cell currentCell)
        {
            if (TestIfThereIsCellAtPosition(currentCell.xPosition - 2, currentCell.yPosition))
            {
                if (FindCellAtPosition(currentCell.xPosition - 2, currentCell.yPosition).aisle)
                {
                    if (FindCellInbetween(currentCell, FindCellAtPosition(currentCell.xPosition - 2, currentCell.yPosition)).aisle)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        //right
        public static bool CheckAisleRight(Cell currentCell)
        {
            if (TestIfThereIsCellAtPosition(currentCell.xPosition + 2, currentCell.yPosition))
            {
                if (FindCellAtPosition(currentCell.xPosition + 2, currentCell.yPosition).aisle)
                {
                    if (FindCellInbetween(currentCell, FindCellAtPosition(currentCell.xPosition + 2, currentCell.yPosition)).aisle)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        //Returnerar en lista med alla grannar som är unvisited och nåbara, utgår från cellen som skickas in som parameter
        public static List<Cell> FindAllNeibouringUnvisited(Cell centerCell)
        {
            List<Cell> returnList = new List<Cell>();

            //down
            if (TestIfThereIsCellAtPosition(centerCell.xPosition, centerCell.yPosition + 2))
            {
                if (FindCellAtPosition(centerCell.xPosition, centerCell.yPosition + 2).visitedBySolver == false)
                {
                    if (FindCellInbetween(centerCell, FindCellAtPosition(centerCell.xPosition, centerCell.yPosition + 2)).aisle == true)
                    {
                        returnList.Add(FindCellAtPosition(centerCell.xPosition, centerCell.yPosition + 2));
                    }
                }
            }

            //right
            if (TestIfThereIsCellAtPosition(centerCell.xPosition + 2, centerCell.yPosition))
            {
                if (FindCellAtPosition(centerCell.xPosition + 2, centerCell.yPosition).visitedBySolver == false)
                {
                    if (FindCellInbetween(centerCell, FindCellAtPosition(centerCell.xPosition + 2, centerCell.yPosition)).aisle == true)
                    {
                        returnList.Add(FindCellAtPosition(centerCell.xPosition + 2, centerCell.yPosition));
                    }
                }
            }

            //left
            if (TestIfThereIsCellAtPosition(centerCell.xPosition - 2, centerCell.yPosition))
            {
                if (FindCellAtPosition(centerCell.xPosition - 2, centerCell.yPosition).visitedBySolver == false)
                {
                    if (FindCellInbetween(centerCell, FindCellAtPosition(centerCell.xPosition - 2, centerCell.yPosition)).aisle == true)
                    {
                        returnList.Add(FindCellAtPosition(centerCell.xPosition - 2, centerCell.yPosition));
                    }
                }
            }

            //up
            if (TestIfThereIsCellAtPosition(centerCell.xPosition, centerCell.yPosition - 2))
            {
                if (FindCellAtPosition(centerCell.xPosition, centerCell.yPosition - 2).visitedBySolver == false)
                {
                    if (FindCellInbetween(centerCell, FindCellAtPosition(centerCell.xPosition, centerCell.yPosition - 2)).aisle == true)
                    {
                        returnList.Add(FindCellAtPosition(centerCell.xPosition, centerCell.yPosition - 2));
                    }
                }
            }

            return returnList;
        }

        //Tar bort alla celler så att man kan generera en nya labyrint
        public static void ResetholeBoard()
        {
            allCells = null;
            currentCell = null;
            consideredCells = null;
            consideredCells = new List<Cell>();
        }

        //Återställer lösningsinformationen i alla celler så att man kan lösa samma labyrint igen
        public static void ResetSolve()
        {
            solved = false;

            for (int i = 0; i < allCells.Length; i++)
            {
                allCells[i].current = false;
                allCells[i].visitedBySolver = false;
            }
        }


        //Räknar ut medelvärdet på värdena som skickas in i listan
        public static long CalculateAvergareTime(List<long> allTimes)
        {
            long totaltime = 0;

            for (int i = 0; i < allTimes.Count; i++)
            {
                totaltime = totaltime + allTimes[i];
            }

            return (totaltime / allTimes.Count);
        }

        public static void RandomizeStartPosition()
        {
            int startY;
            int startX;

            Random randomObject = new Random();

            bool isDone = false;
            while (isDone == false)
            {
                startX = randomObject.Next(0, (widthOfMaze + 1));

                if (startX != widthOfMaze - 2 && startX % 2 != 0 && startX <= (widthOfMaze - 2))
                {
                    xStartingPosition = startX;
                    isDone = true;
                }
            }

            isDone = false;
            while (isDone == false)
            {
                startY = randomObject.Next(0, (heightOfMaze + 1));

                if (startY != heightOfMaze - 2 && startY % 2 != 0 && startY <= (heightOfMaze - 2))
                {
                    yStartingPosition = startY;
                    isDone = true;
                }
            }


        }
    }
}
