using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVAL.ConnectFour.Persistence
{
    /// <summary>
    /// ConnectFour tábla adatkezelése közben keltett kivétel.
    /// </summary>
    public class ConnectFourDataException : Exception
    {
        /// <summary>
        /// <c>ConnectFour</c> adatelérés kivétel példányosítása.
        /// </summary>
        public ConnectFourDataException() { }

        /// <summary>
        /// <c>ConnectFour</c> adatelérés kivétel példányosítása.
        /// </summary>
        /// <param name="message">Üzenet.</param>
        public ConnectFourDataException(string message) : base(message) { }

        /// <summary>
        /// <c>ConnectFour</c> adatelérés kivétel példányosítása.
        /// </summary>
        /// <param name="message">Üzenet.</param>
        public ConnectFourDataException(string message, Exception innerException) : base(message, innerException) { }

    }
}
