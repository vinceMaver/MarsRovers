using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarsRovers
{
    public class Rover
    {
        public Position position { get; set; }
        public GridSize plateuSize { get; set; }
        public IList<Instruction> instructions { get; set; }
        private IRoverFunctionality roverFunctionality;

        public Rover() { }
        public Rover( GridSize plateuSize, Position position, IList<Instruction> instructions, IRoverFunctionality roverFunctionality)
        {
            this.position = position;
            this.plateuSize = plateuSize;
            this.instructions = instructions;
            this.roverFunctionality = roverFunctionality;
        }

        private Position ExecuteInstruction(Position position, Instruction instruction)
        {
            switch (instruction)
            {
                case Instruction.L:
                    position = roverFunctionality.TurnLeft(position);
                    break;

                case Instruction.R:
                    position = roverFunctionality.TurnRight(position);
                    break;

                case Instruction.M:
                    position = roverFunctionality.Move(plateuSize,position);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return position;
        }

        public void ExecuteInstructions()
        {
            foreach (var operation in this.instructions)
            {
                this.position = ExecuteInstruction(this.position, operation);
            }
        }
        
    }
}
