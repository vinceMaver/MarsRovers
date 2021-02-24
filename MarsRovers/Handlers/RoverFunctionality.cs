using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRovers
{
    public class RoverFunctionality : IRoverFunctionality
    {
        public Position Move(GridSize plateuSize, Position position)
        {
            var initialPosition = position;

            switch (position.Cardinality)
            {
                case Cardinality.N:
                    position.Y++;
                    break;

                case Cardinality.E:
                    position.X++;
                    break;

                case Cardinality.S:
                    position.Y--;
                    break;

                case Cardinality.W:
                    position.X--;
                    break;

                default:
                    throw new InvalidOperationException();
            }
            
            if (!IsRoverWithinPlateu(plateuSize, position))
            {
                //revert position change
                position.X = initialPosition.X;
                position.Y = initialPosition.Y;
                //log message
            }

            return position;
        }

        public Position TurnLeft(Position position)
        {
            position.Cardinality = (position.Cardinality - 1) < Cardinality.N ? 
                                                                Cardinality.W : 
                                                                position.Cardinality - 1;
            return position;
        }

        public Position TurnRight(Position position)
        {
            position.Cardinality = (position.Cardinality + 1) > Cardinality.W ? 
                                                               Cardinality.N : 
                                                               position.Cardinality + 1;

            return position;
        }

        private bool IsRoverWithinPlateu(GridSize  grid, Position position) //TODO: move to helper class
        {
            if (position.X > grid.Width ||
                position.X < 0 ||
                position.Y > grid.Height ||
                position.Y < 0)
            {
                return false;
            }

            return true;
        }
    }
}
