using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVAL.ConnectFour.Common
{
    /// <summary>
    /// Cella értéke - két játékos egyike, vagy üres. <c>char</c>-ba alakítható.
    /// </summary>
    public enum PlayerColour
    {
        NONE = '\0',
        X = 'X',
        O = 'O'
    }

    /// <summary>
    /// Cella pozíciója.
    /// </summary>
    public class Position : Tuple<int, int>
    {
        /// <summary>
        /// X koordináta.
        /// </summary>
        public int X { get => base.Item1; }
        /// <summary>
        /// Y koordináta.
        /// </summary>
        public int Y { get => base.Item2; }
        public Position(int x, int y) : base(x, y) { }

        // ...
        // Tuple szolgáltat ToString, Equals és GetHashCode metódusokat
    }
}
