using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers
{
    public class GridSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public GridSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
