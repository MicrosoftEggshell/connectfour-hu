using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EVAL.ConnectFour.Common;

namespace EVAL.ConnectFour.Persistence
{
    public class ConnectFourTextFileDataAccess : IConnectFourDataAccess
    {
        private static readonly string VersionNumber = "1.0";

        private static async Task<ConnectFourBoard> LoadAsyncV1_0(StreamReader reader)
        {
            string? line = await reader.ReadLineAsync() ?? string.Empty;
            // táblaméret beolvasása
            string[] numbers = line.Split(' ');
            int w = int.Parse(numbers[0]);
            int h = int.Parse(numbers[1]);
            line = await reader.ReadLineAsync() ?? string.Empty;
            ConnectFourBoard board = new ConnectFourBoard(w, h);

            // eltelt idők beolvasása
            string[] times = line.Split(' ');
            int xtime = int.Parse(times[0]);
            int ytime = int.Parse(times[1]);
            board.PlayerTime[PlayerColour.X] = TimeSpan.FromMilliseconds(xtime);
            board.PlayerTime[PlayerColour.O] = TimeSpan.FromMilliseconds(ytime);

            line = await reader.ReadLineAsync();
            for (int i = 0; line != null; i++)
            {
                board.Insert(int.Parse(line), i % 2 == 0 ? PlayerColour.X : PlayerColour.O);
                line = await reader.ReadLineAsync();
            }

            return board;
        }

        /// <summary>
        /// Fájl betöltése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        /// <returns>A fájlból beolvasott játéktábla.</returns>
        public async Task<ConnectFourBoard> LoadAsync(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path)) // fájl megnyitása
                {
                    string version = await reader.ReadLineAsync() ?? string.Empty;
                    return version switch
                    {
                        "1.0" => await LoadAsyncV1_0(reader),
                        _ => throw new NotImplementedException("A program ezen verziója nem tudja az adott mentést betölteni, ellenőrizd, hogy érvényes-e!")
                    };
                }
            }
            catch
            {
                throw new ConnectFourDataException("Sikertelen betöltés!");
            }
        }

        /// <summary>
        /// Fájl mentése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        /// <param name="board">A fájlba kiírandó játéktábla.</param>
        public async Task SaveAsync(string path, ConnectFourBoard board)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása
                {
                    await writer.WriteLineAsync(VersionNumber);
                    await writer.WriteLineAsync($"{board.Width} {board.Height}"); // kiírjuk a méreteket
                    await writer.WriteLineAsync($"{(int)board.PlayerTime[PlayerColour.X].TotalMilliseconds} {(int)board.PlayerTime[PlayerColour.O].TotalMilliseconds}"); // eltelt idők
                    foreach (int m in board.Moves)
                    {
                        await writer.WriteLineAsync(m.ToString()); // kiírjuk az értékeket
                    }
                }
            }
            catch (ConnectFourDataException e)
            {
                throw e;
            }
        }
    }
}
