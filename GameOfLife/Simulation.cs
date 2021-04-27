using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    public class Simulation
    {
        private readonly int _rows;
        private readonly int _columns;
        private bool _runSimulation;


        public Simulation(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
        }

        public void Start()
        {
            _runSimulation = true;
            
            var grid = InitGrid();
            
            while(_runSimulation)
            {
                PrintGrid(grid);
                grid = NextGeneration(grid);
            }
        }

        public void Stop()
        {
            _runSimulation = false;
        }

        private CellStatus[,] InitGrid()
        { 
            var grid = new CellStatus[_rows, _columns];

            for (var row = 0; row < _rows; row++)
            {
                for (var column = 0; column < _columns; column++)
                {
                    grid[row, column] = (CellStatus) RandomNumberGenerator.GetInt32(0, 2);
                }
            }

            return grid;
        }

        private CellStatus[,] NextGeneration(CellStatus[,] currentGrid)
        {
            var nextGenerationGrid = currentGrid;
            
            for (var row = 1; row < _rows - 1; row++)
            for (var column = 1; column < _columns - 1; column++)    
            {
                var aliveNeighbours = CalculateAliveNeighbours(row, column, currentGrid);

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
        
        private void PrintGrid(CellStatus[,] grid, int timeout=500)
        {
            var stringBuilder = new StringBuilder();

            for (var rowIndex = 0; rowIndex < _rows; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < _columns; columnIndex++)
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
        
        
        private int CalculateAliveNeighbours(int rowIndex, int columnIndex, CellStatus[,] grid)
        {
            var aliveNeighbours = 0;
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    aliveNeighbours += grid[rowIndex + i, columnIndex + j] == CellStatus.Alive ? 1 : 0;
                }
            }

            return aliveNeighbours;
        }
    }
}