using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        const int Rows = 25;
        const int Columns = 50;

        private static bool runSimulation = true;
        
        static void Main(string[] args)
        {

            var simulation = new Simulation(25, 50);

            Console.CancelKeyPress += (sender, args) =>
            {
                simulation.Stop();
                Console.WriteLine("\n👋 Ending simulation.");
            };
            
            Console.Clear();
            Console.WriteLine("Starting simulation");
            simulation.Start();
            
            
            
            // var grid = new CellStatus[Rows, Columns];
            //
            // for (var row = 0; row < Rows; row++)
            // {
            //     for (var column = 0; column < Columns; column++)
            //     {
            //         grid[row, column] = (CellStatus)RandomNumberGenerator.GetInt32(0, 2);
            //     }
            // }
            

            
            

            // while (runSimulation)
            // {
            //     PrintGrid(grid);
            //     grid = NextGeneration(grid);    
            // }

        }

        static CellStatus[,] NextGeneration(CellStatus[,] currentGrid)
        {
            var nextGenerationGrid = currentGrid;
            
            for (var row = 1; row < Rows - 1; row++)
            for (var column = 1; column < Columns - 1; column++)    
            {
                var aliveNeighbours = 0;
                for (var i = -1; i <= 1; i++)
                {
                    for(var j = -1; j <= 1; j++)
                    {
                        aliveNeighbours += currentGrid[row + i, column + j] == CellStatus.Alive ? 1 : 0;
                    }
                }

                var currentCell = currentGrid[row, column];

                if (currentCell == CellStatus.Alive && aliveNeighbours < 2)
                {
                    nextGenerationGrid[row, column] = CellStatus.Dead;
                }
                else if (currentCell == CellStatus.Alive && aliveNeighbours > 3)
                {
                    nextGenerationGrid[row, column] = CellStatus.Dead;
                }
                else if (currentCell == CellStatus.Dead && aliveNeighbours == 3)
                {
                    nextGenerationGrid[row, column] = CellStatus.Alive;
                }
                else
                {
                    nextGenerationGrid[row, column] = currentCell;
                }
            }

            return nextGenerationGrid;
        }

        static void PrintGrid(CellStatus[,] grid, int timeout=500)
        {
            var stringBuilder = new StringBuilder();

            for (var rowIndex = 0; rowIndex < Rows; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < Columns; columnIndex++)
                {
                    var cell = grid[rowIndex, columnIndex];
                    stringBuilder.Append(cell == CellStatus.Alive ? "👨🏻" : "🧟‍♂️");
                }

                stringBuilder.Append("\n");
            }
            
            Console.SetCursorPosition(0,0);
            Console.WriteLine(stringBuilder.ToString());
            Thread.Sleep(timeout);
        }
    }

    public enum CellStatus
    {
        Alive,
        Dead
    }
}