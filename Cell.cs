using System;
using System.Collections.Generic;
using System.Text;

namespace TestForMaze4
{
    class Cell
    {
        public bool current;
        public bool wall;
        public bool available; //Måste göras unavaible när den blir en vägg
        public bool aisle;
        //public bool visited;
        public bool considered;


        public bool isExit;
        public bool visitedBySolver = false;

        public int xPosition;
        public int yPosition;

        public ConsoleColor currentColor;

        public Cell markedConsideredBy;

        //Konstruktor som sätter alla värden som behövs
        public Cell(int _yPosition, int _xPosition)
        {
            xPosition = _xPosition;
            yPosition = _yPosition;
            
            current = false;
            wall = true;
            aisle = false;
            considered = false;

            if (xPosition % 2 != 0 && yPosition % 2 != 0)
            {
                available = true;
            } 
            else
            {
                available = false;
            }
        }
    }
}
