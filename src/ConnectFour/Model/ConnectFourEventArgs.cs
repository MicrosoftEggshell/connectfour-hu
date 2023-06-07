using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EVAL.ConnectFour.Common;
using EVAL.ConnectFour.Persistence;

namespace EVAL.ConnectFour.Model
{
    /// <summary>
    /// <c>ConnectFourEventArgs</c> eseményargumentumokban a játék állapotának jelzésére használt felsorolástípus.
    /// </summary>
    public enum ConnectFourEvent
    {
        /// <summary>
        /// Hibás állapot. Program helyes működése során nem áll elő.
        /// </summary>
        ERROR = -1,
        /// <summary>
        /// Ismeretlen állapot. Program helyes működése során nem áll elő.
        /// </summary>
        UNKNOWN,
        /// <summary>
        /// Idő eltelte
        /// </summary>
        TIME_ELAPSED,
        /// <summary>
        /// Új pozíció elfogalása a táblán.
        /// </summary>
        PLACEMENT,
        /// <summary>
        /// Befejezetlen, szünetelt állapotban lévő játék.
        /// </summary>
        PAUSE,
        /// <summary>
        /// Játék győzelemmel való befejeződése.
        /// </summary>
        WIN,
        /// <summary>
        /// Játék döntetlen eredménnyel (betelt tábla) való befejeződése.
        /// </summary>
        DRAW
    }
    /// <summary>
    /// ConnectFour eseményargumentumainak típusa.
    /// </summary>
    public class ConnectFourEventArgs : EventArgs
    {
        private readonly TimeSpan _time;
        private readonly Position[]? _winner;
        private readonly Position? _position;
        private readonly ConnectFourEvent _type;

        /// <summary>
        /// Eltelt játékidő.
        /// </summary>
        public TimeSpan GameTime => _time;

        /// <summary>
        /// Játék állapota.
        /// </summary>
        public ConnectFourEvent EventType => _type;

        public Position? Position { get => _position; }

        public Position[]? Winner { get => _winner; }

        /// <summary>
        /// ConnectFour események eseményargumentuma.
        /// </summary>
        /// <param name="eventType">Játékállapot.</param>
        /// <param name="gameTime">Játékidős.</param>
        public ConnectFourEventArgs(
            ConnectFourEvent eventType,
            TimeSpan gameTime,
            Position? position = null,
            Position[]? winner = null
            )
        {
            _type = eventType;
            _time = gameTime;
            _position = position;
            _winner = winner;
        }
    }
}
