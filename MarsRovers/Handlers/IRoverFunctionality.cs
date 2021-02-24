using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers
{
    public interface IRoverFunctionality
    {
        Position TurnLeft(Position position);
        Position TurnRight(Position position);
        Position Move(GridSize plateuSize, Position position);
    }
}
