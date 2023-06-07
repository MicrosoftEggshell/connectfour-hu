using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVAL.ConnectFour.Persistence
{
    public interface IConnectFourDataAccess
    {
        /// <summary>
        /// Fájl betöltése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        /// <returns>A fájlból beolvasott játéktábla.</returns>
        Task<ConnectFourBoard> LoadAsync(String path);

        /// <summary>
        /// Fájl mentése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        /// <param name="board">A fájlba kiírandó játéktábla.</param>
        Task SaveAsync(String path, ConnectFourBoard board);
    }
}
