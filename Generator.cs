using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestForMaze4
{
    class Generator
    {
        public static void RunGenerator() //Körs från Manager
        {
            Information.RandomizeStartPosition();

            Information.CorrectUneven();
            Information.CreateCellArray();
            Information.CreateCells();

            // Information.PrintAllCreatedCellsInformaion(); //Finns endast för debugsyften

            if (Information.printAtGeneration)
            {
                Draw.DrawCells();
            }

            GenerateMaze();
        }

        static void GenerateMaze()
        {

            Information.FindCellAtPosition(Information.currentXPosition, Information.currentYPosition);

            List<Cell> neighbors = new List<Cell>(); //Alla grannar till den nuvarande cellen
            Information.currentCell = Information.FindCellAtPosition(Information.xStartingPosition, Information.yStartingPosition);
            Information.currentCell.current = true;

            Information.currentCell.wall = false;
            Information.currentCell.available = false;
            Information.currentCell.aisle = true;

            Cell currentAtBeggining; //Cell som är current vid början av cykeln
            Cell chosenCell; //Cell som valts för att bli nästa current
            Cell betweenCurrentAndNeighbour;

            //Hela labyrinten ska skapas här
            while (Information.CellsAvailable() == true)
            {
                currentAtBeggining = Information.currentCell;

                neighbors = Information.FindNeighbouringWalls(Information.currentCell);

                //Ifall det finns grannar letar den upp en och väljer den som nästa cell, gör även cellen mellan nuvarande och den valda grannen till gång
                if (neighbors.Count > 0)
                {
                    Information.MarkCellsInListConsidered(neighbors, currentAtBeggining);

                    if (Information.printAtGeneration)
                    {
                        Draw.DrawCells();
                    }

                    //Välja en granne som ska bli nästa cell
                    chosenCell = Information.RandomizeCellFromList(neighbors);

                    //Ta bort väggar och ändra variabler för vald granne och den cellen mellan nuvarande och vald granne
                    chosenCell.considered = false;

                    chosenCell.wall = false;
                    chosenCell.aisle = true;
                    chosenCell.considered = false;
                    chosenCell.available = false;


                    betweenCurrentAndNeighbour = Information.FindCellInbetween(currentAtBeggining, chosenCell);
                    betweenCurrentAndNeighbour.wall = false;
                    betweenCurrentAndNeighbour.aisle = true;

                    //Göra vald granne till nuvarande, både i cellen och i information.current och ta bort gamla current
                    currentAtBeggining.current = false;
                    chosenCell.current = true;
                    Information.currentCell = chosenCell;

                    Information.consideredCells.Remove(chosenCell);

                    if (Information.printAtGeneration)
                    {
                        Draw.DrawCells();
                    }
                }
                //Ifall det inte skulle finnas några grannar kollar den igenom vilka grannar som hittats tidigare men inte blivit valda och gör en av dem till current och utför samma sak som ifall current skulle haft en granne från början
                else
                {
                    chosenCell = Information.consideredCells[0];

                    chosenCell.considered = false;

                    chosenCell.wall = false;
                    chosenCell.aisle = true;
                    chosenCell.considered = false;
                    chosenCell.available = false;

                    chosenCell.markedConsideredBy.wall = false;
                    chosenCell.markedConsideredBy.aisle = true;
                    chosenCell.markedConsideredBy.considered = false;

                    betweenCurrentAndNeighbour = Information.FindCellInbetween(chosenCell.markedConsideredBy, chosenCell);
                    betweenCurrentAndNeighbour.wall = false;
                    betweenCurrentAndNeighbour.aisle = true;

                    //Göra vald granne till nuvarande, både i cellen och i information.current och ta bort gamla current
                    currentAtBeggining.current = false;
                    chosenCell.current = true;
                    Information.currentCell = chosenCell;

                    Information.consideredCells.RemoveAt(0);

                    /*Göra en cell från information.consideredcells -listan till nuvarande cell och ta bort väggen mellan considered-cellen och den cellen den blev 
                    markerad till det som(cellen som markerade den finns som "markedConsideredBy" i cellklassen, måste ta bort den ur considered listan!*/

                    if (Information.printAtGeneration)
                    {
                        Draw.DrawCells();
                    }
                }
                Information.currentTopMessage = "n. of considered: " + Information.consideredCells.Count.ToString();

            }

            Information.currentCell.current = false;

            //Nollställer så att ingen cell är current
            for (int i = 0; i < Information.allCells.Length; i++)
            {
                Information.allCells[i].current = false;
            }

            //Sätter cellen Längst ned till höger som exit
            Information.FindCellAtPosition(Information.widthOfMaze - 2, Information.heightOfMaze - 2).isExit = true;


            Information.currentTopMessage = "";

            if (Information.printAtGeneration)
            {
                Draw.DrawCells();
            }
        }
    }
}
