using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EVAL.ConnectFour.Model;

namespace EVAL.ConnectFour.View
{
    public class ConnectFourOptions
    {
        public BoardSize BoardSize { get; set; } = BoardSize.Small;
        public Color P1Colour { get; set; } = Color.Red;
        public Color P2Colour { get; set; } = Color.Blue;

        public ConnectFourOptions() { }

        public ConnectFourOptions(BoardSize boardSize, Color p1Colour, Color p2Colour)
        {
            BoardSize = boardSize;
            P1Colour = p1Colour;
            P2Colour = p2Colour;
        }

        public ConnectFourOptions(ConnectFourOptions other) => SetClone(other);

        public void SetClone(ConnectFourOptions other)
        {
            BoardSize = other.BoardSize;
            P1Colour = other.P1Colour;
            P2Colour = other.P2Colour;
        }
    }
}
