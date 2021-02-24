using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace MarsRovers
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup DI
            var serviceProvider = new ServiceCollection()
                                     .AddScoped<IRoverFunctionality, RoverFunctionality>()
                                     .BuildServiceProvider();

            var roverFunctionality = serviceProvider.GetService<IRoverFunctionality>();

            var deployedRovers = new List<Rover>();
            var inputOperation = new Input();
            
            Console.WriteLine("Plateau surface size :");

            var gridInput = Console.ReadLine();

            while (true)
            {
                Console.WriteLine("Rover position :");
                string roverPositionInput = Console.ReadLine();

                Console.WriteLine("Rover commands :");
                var roverInstructionsInput = Console.ReadLine();

                //TODO: Validate input
                var deployedRover = inputOperation.ParseRover(new List<string>() {gridInput,
                                                                                  roverPositionInput,
                                                                                  roverInstructionsInput
                                                                }, roverFunctionality);


                
                deployedRovers.Add(deployedRover);

                deployedRover.ExecuteInstructions();

                Console.WriteLine("Do you want to add another rover ? (Y/N)");
                var addRoverInput = Console.ReadLine();

                if (!addRoverInput.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }
            }

            //display Output

            Console.WriteLine("Output :");

            foreach (var rover in deployedRovers)
            {
                Console.WriteLine($"{rover.position.X} {rover.position.Y} {rover.position.Cardinality.ToString()}");
            }

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
