using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {

            var simulation = new Simulation(25, 50);

            Console.CancelKeyPress += (sender, args) =>
            {
                simulation.Stop();
                Console.WriteLine("\n Ending the simulation.");
            };

            Console.Clear();
            Console.WriteLine("Starting the simulation");
            simulation.Start();
        }
    }

    public enum CellStatus
    {
        Alive,
        Dead
    }
}