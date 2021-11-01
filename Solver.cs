using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestForMaze4
{
    class Solver
    {
        //Löser labyrinten rekursivt
        public static void RecursiveSolver(Cell currentCell, Cell lastCell)
        {
            if (Information.solved == true) //BASFALL Ifall labyrinten är löst stoppas alla ytterligare anrop
            {
                return;
            }

            currentCell.current = true; 
            currentCell.visitedBySolver = true; 

            Information.FindCellInbetween(currentCell, lastCell).visitedBySolver = true; //Gör så att cellen mellan den nuvarande och den förra blir markerad besökt

            List<Cell> nextCells;

            if (Information.printAtSolving == true) //Ifall användaren valt att konsollfönsret ska uppdateras vid varje lösningscykel görs det
            {
                Draw.DrawCells();
            }

            nextCells = Information.FindAllNeibouringUnvisited(currentCell); //Hittar alla grannar som inte beskökts av lösaren och lägger de i en lista

            currentCell.current = false;

            //Ifall det finns grannar kallas metoden en gång för varje granne och den förra cellen blir den 
            if (nextCells.Count > 0 && currentCell.isExit == false)
            {
                for (int i = 0; i < nextCells.Count; i++)
                {
                    RecursiveSolver(nextCells[i], currentCell);
                }
            }
            else 
            {
                if (currentCell.isExit == true) //Ifall current är utgången är labyrinten löst.
                {
                    Information.solved = true;
                }
            }
        }

        public static void SolveWithRightWallFollower() //Ska lägga till solvingDirection som parameter för att ta reda på ifall den ska lösa åt höger eller vänster
        {
            string direction; //Åt vilket håll cellen är riktad
            bool solved = false;
            Cell currentCell = Information.FindCellAtPosition(Information.xStartingPosition, Information.yStartingPosition); //Hittar startcellen
            Cell newCurrentCell;

            currentCell.current = true;

            if (Information.printAtSolving)
            {
                Draw.DrawCells();
            }

            while (solved == false)
            {
                direction = Information.FindStartingDirection(currentCell); //Hämtar startriktning
                currentCell.visitedBySolver = true;
                while (solved == false)
                {
                    switch (direction) //Beroende på vilket håll cellen är riktad åt kommer den ha olika preferenser för vart den vill röra sig och hur gärna. Riktningarna gör så att den hela tiden försöker "hålla högerhanden i väggen"
                    {
                        case "up": //Ifall riktningen var uppåt kommer den försöka gå åt höger först
                            if (Information.CheckAisleRight(currentCell)) //Kollar ifall cellen till höger är ledig
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition + 2, currentCell.yPosition); //Nästa cell blir den till höger
                                newCurrentCell.visitedBySolver = true; //Nästa cell markeras som besökt av lösaren
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true; //Markerar cellen mellan den nya och nuvarande som besökt
                                direction = "right";

                                currentCell.current = false; //Tar bort markeringen som säger att den nuvarande cellen är nuvarande

                                currentCell = newCurrentCell; //Sätter nästa cell till den nästa

                                currentCell.current = true; //Nästa(nuvarande nu) blir satt till nuvarande
                                //Samma sak händer som reten av riktningarna

                                //måste fylla på med "ny" direction på alla och byta currentcell, ska även markera den nya cellen som visitedBySolver på cellobjektet
                            }
                            else if (Information.CheckAisleUp(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition - 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "up";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleLeft(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition - 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "left";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleDown(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition + 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "down";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }

                            break;

                        case "down":
                            if (Information.CheckAisleLeft(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition - 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "left";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;

                            }
                            else if (Information.CheckAisleDown(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition + 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "down";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleRight(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition + 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "right";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleUp(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition - 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "up";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;

                            }

                            break;

                        case "left":
                            if (Information.CheckAisleUp(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition - 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "up";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;

                            }
                            else if (Information.CheckAisleLeft(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition - 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "left";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleDown(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition + 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "down";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleRight(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition + 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "right";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }

                            break;

                        case "right":
                            if (Information.CheckAisleDown(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition + 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "down";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleRight(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition + 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "right";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleUp(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition - 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "up";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleLeft(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition - 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "left";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }

                            break;
                    }

                    Information.currentTopMessage = direction;

                    if (Information.printAtSolving)
                    {
                        Draw.DrawCells();
                    }

                    if (currentCell.isExit == true)
                    {
                        solved = true;
                    }
                }
            }

            if (Information.printAtSolving)
            {
                Draw.DrawCells();
            }
        }

        //Samma sak som högerlösingen, fast omvänd
        public static void SolveWithLeftWallFollower() //Ska lägga till solvingDirection som parameter för att ta reda på ifall den ska lösa åt höger eller vänster
        {
            string direction;
            bool solved = false;
            Cell currentCell = Information.FindCellAtPosition(Information.xStartingPosition, Information.yStartingPosition);
            Cell newCurrentCell;

            currentCell.current = true;


            if (Information.printAtSolving)
            {
                Draw.DrawCells();
            }


            while (solved == false)
            {

                direction = Information.FindStartingDirection(currentCell);
                currentCell.visitedBySolver = true;
                while (solved == false)
                {
                    switch (direction)
                    {
                        case "up":
                            if (Information.CheckAisleLeft(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition - 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "left";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleUp(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition - 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "up";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleRight(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition + 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "right";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleDown(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition + 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "down";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;

                            }

                            break;

                        case "down":
                            if (Information.CheckAisleRight(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition + 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "right";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleDown(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition + 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "down";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleLeft(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition - 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "left";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleUp(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition - 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "up";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;

                            }

                            break;

                        case "left":
                            if (Information.CheckAisleDown(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition + 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "down";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleLeft(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition - 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "left";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleUp(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition - 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "up";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleRight(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition + 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "right";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }

                            break;

                        case "right":
                            if (Information.CheckAisleUp(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition - 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "up";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleRight(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition + 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "right";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleDown(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition, currentCell.yPosition + 2);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "down";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }
                            else if (Information.CheckAisleLeft(currentCell))
                            {
                                newCurrentCell = Information.FindCellAtPosition(currentCell.xPosition - 2, currentCell.yPosition);
                                newCurrentCell.visitedBySolver = true;
                                Information.FindCellInbetween(currentCell, newCurrentCell).visitedBySolver = true;
                                direction = "left";

                                currentCell.current = false;

                                currentCell = newCurrentCell;

                                currentCell.current = true;
                            }

                            break;
                    }

                    Information.currentTopMessage = direction;

                    if (Information.printAtSolving)
                    {
                        Draw.DrawCells();
                    }

                    if (currentCell.isExit == true)
                    {
                        solved = true;
                    }
                }
            }

            if (Information.printAtSolving)
            {
                Draw.DrawCells();
            }
        }
    }

}
