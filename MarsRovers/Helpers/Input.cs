using CommonServiceLocator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRovers
{
    public class Input
    {
        #region properties
        GridSize Platue { get; set; }
        Position RoverPosition { get; set; }
        IList<Instruction> Instructions { get; set; }
        bool IsValid { get; set; }

        #endregion

        #region constructors
        public Input() { }

        public Input(bool isValid, GridSize plateu, Position roverPosition, IList<Instruction> instructions)
        {
            this.IsValid = isValid;
            this.Platue = plateu;
            this.RoverPosition = roverPosition;
            this.Instructions = instructions;
        }

        #endregion

        #region methods
        private Input ParseInput(IList<string> input)
        {
            var validInput   = new Input() { IsValid = true};
            var invalidInput = new Input() { IsValid = false };

            if (!input.Any() || input.Count != 3)
                return invalidInput;

            //get grid size
            var plateu = ParsePlateu(input[0]);

            if (plateu == null)
                return invalidInput;
            else
                validInput.Platue = plateu;

            //get starting position
            var startingPosition = ParsePosition(input[1]);

            if (startingPosition == null)
                return invalidInput;
            else
                validInput.RoverPosition = startingPosition;

            //get instructions
            var instructions = ParseInstructions(input[2]);

            if (instructions == null)
                return invalidInput;
            else
                validInput.Instructions = instructions;

            return validInput;

        }

        public Rover ParseRover(IList<string> input, IRoverFunctionality roverFunctionality)
        {
            var parsedInput = ParseInput(input);

            if (parsedInput.IsValid)
                return new Rover(parsedInput.Platue, parsedInput.RoverPosition, parsedInput.Instructions, roverFunctionality);
            else
                return null;
        }

        private GridSize ParsePlateu(string input)
        {
            var grid = input.Split(' ');

            if (grid.Length != 2)
                return null;

            if (int.TryParse(grid[0], out var xWidth) && int.TryParse(grid[1], out var yHeight))
                return new GridSize(xWidth, yHeight);
            else
                return null;
        }

        private Position ParsePosition(string input) //TODO: move to parse helper class
        {
            var roverPosition = input.Split(' ');
            var cardinalArray = new string[] { "N", "E", "S", "W" };

            if (roverPosition.Length == 3 &&
                int.TryParse(roverPosition[0], out var xPosition) &&
                int.TryParse(roverPosition[1], out var yPosition)
                )
            {
                var cardinal = roverPosition[2];

                if (cardinalArray.Contains(cardinal, StringComparer.InvariantCultureIgnoreCase))
                {
                    var position = new Position()
                    {
                        X = xPosition,
                        Y = yPosition,
                        Cardinality = (Cardinality)Enum.Parse(typeof(Cardinality), cardinal)
                    };

                    return position;
                }
            }

            return null;
        }

        private IList<Instruction> ParseInstructions(string input)
        {
            var result = new List<Instruction>();
            var instructions = input.ToArray().Select(c => c.ToString()).ToArray();


            foreach (var instruction in instructions)
            {
                Instruction command;

                if (Enum.TryParse(instruction, true, out command))
                    result.Add(command);
                else
                    return null;
            }

            return result;

        }

        #endregion
    }
}
