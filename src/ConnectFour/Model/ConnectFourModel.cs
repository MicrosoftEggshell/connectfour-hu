using EVAL.ConnectFour.Persistence;
using System;
using System.Diagnostics.CodeAnalysis;

using Timer = System.Timers.Timer;

using EVAL.ConnectFour.Common;

namespace EVAL.ConnectFour.Model
{
    /// <summary>
    /// Előredefiniált táblaméreteket tároló struktúra, Enumhoz hasonlatosan használható.
    /// </summary>
    public struct BoardSize : IEquatable<BoardSize>
    {
        /// <summary>
        /// Kicsi (10x10) táblaméret.
        /// </summary>
        public static readonly BoardSize Small = new BoardSize(10, 10);
        /// <summary>
        /// Közepes (20x20) táblaméret.
        /// </summary>
        public static readonly BoardSize Medium = new BoardSize(20, 20);
        /// <summary>
        /// Nagy (30x30) táblaméret.
        /// </summary>
        public static readonly BoardSize Large = new BoardSize(30, 30);
        /// <summary>
        /// Összes méret felsorolása
        /// </summary>
        public static readonly BoardSize[] Sizes = new BoardSize[3]{ Small, Medium, Large };

        /// <summary>
        /// Tábla szélessége.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Tábla magassága.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// <c>BoardSize</c> objektum létrehozása adott szélességgel és magassággal.
        /// </summary>
        /// <param name="width">Tábla szélessége.</param>
        /// <param name="height">Tábla magassága.</param>
        public BoardSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        #region Comparitors

        public bool Equals(BoardSize other) => Width == other.Width && Height == other.Height;

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is BoardSize b && Equals(b);
        }

        public static bool operator ==(BoardSize left, BoardSize right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BoardSize left, BoardSize right)
        {
            return !(left == right);
        }

        #endregion
    }

    public class ConnectFourModel
    {

        #region Fields

        private bool _isPaused;
        private ConnectFourBoard _board;
        private PlayerColour _lastPlayer;
        private readonly Timer _timer;

        #endregion

        #region Properties

        /// <summary>
        /// Oszlopok száma a model táblájában, vagyis a tábla szélessége.
        /// </summary>
        public int Columns => _board.Width;
        /// <summary>
        /// Sorok száma a model táblájában, vagyis a tábla magassága.
        /// </summary>
        public int Rows => _board.Height;
        /// <summary>
        /// Soron következő játékos.
        /// </summary>
        public PlayerColour LastPlayer => _lastPlayer;

        public PlayerColour NextPlayer
        {
            get
            {
                if (_lastPlayer == PlayerColour.O)
                {
                    return PlayerColour.X;
                }
                return PlayerColour.O;
            }
        }
        /// <summary>
        /// Folyamatban van-e a játék, azaz lehet-e lépést tenni.
        /// </summary>
        public bool IsOngoing => !_isPaused && !_board.IsOver;

        public bool IsPaused => _isPaused;

        #endregion

        #region Events

        public event EventHandler<ConnectFourEventArgs>? GameEvent;
        public event EventHandler<ConnectFourEventArgs>? TimeAdvanced;

        #endregion

        #region Constructors

        /// <summary>
        /// Adott méretű <c>ConnectFourModel</c> példányosítása.
        /// </summary>
        /// <param name="width">A tábla szélessége.</param>
        /// <param name="height">A tábla magassága.</param>
        public ConnectFourModel(int width, int height) : this(new ConnectFourBoard(width, height)) { }

        /// <summary>
        /// Adott méretű <c>ConnectFourModel</c> példányosítása.
        /// </summary>
        /// <param name="size">A tábla méretei.</param>
        public ConnectFourModel(in BoardSize size) : this(new ConnectFourBoard(size.Width, size.Height)) { }

        /// <summary>
        /// <c>ConnectFourModel</c> példányosítása adott táblából.
        /// </summary>
        /// <param name="board">Lépéseket tartalmazó (vagy üres) tábla.</param>
        public ConnectFourModel(ConnectFourBoard board)
        {
            _board = board;
            _isPaused = board.IsOver;
            _timer = new Timer(100);
            _timer.Elapsed += AdvanceTime;
            _timer.Start();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adott oszlop magassága, vagyis adott oszlopban elvégzett mozdulatok száma.
        /// </summary>
        /// <param name="colIndex">Oszlop sorszáma.</param>
        /// <returns>Oszlop magassága.</returns>
        public int ColumnHeight(int colIndex) => _board.ColumnHeight(colIndex);

        /// <summary>
        /// Következő lépés végrehajtása adott oszlopban.
        /// </summary>
        /// <param name="colIndex">Oszop sorszáma.</param>
        public void Place(int colIndex)
        {
            // Csak folyamatban lévő játékban, nem teli oszlopba lehet lépni
            if(!IsOngoing || !_board.CanInsert(colIndex))
            {
                return;
            }
            // Itt már biztosan végre lesz hajtva a lépés
            OnPlace(colIndex);
            _lastPlayer = NextPlayer;
            Position[]? win = _board.WinningMove(colIndex, NextPlayer); // Beillesztés előtt ellenőrzi, hogy nyerni fog-e
            _board.Insert(colIndex, NextPlayer); // Következő játékosra léptetés előtt illeszt be
            if (_board.IsOver)
            {
                OnGameOver(colIndex, win);
            }
        }

        public void Replay()
        {
            ConnectFourBoard oldBoard = _board;
            _board = new ConnectFourBoard(_board.Width, _board.Height);
            _lastPlayer = PlayerColour.O;
            UnPause();
            foreach (int move in oldBoard.Moves)
            {
                Place(move);
            }

            _board.PlayerTime[_lastPlayer] = oldBoard.PlayerTime[_lastPlayer];
            _lastPlayer = NextPlayer;
            _board.PlayerTime[_lastPlayer] = oldBoard.PlayerTime[_lastPlayer];
            TimeAdvanced?.Invoke(this, new ConnectFourEventArgs(
                ConnectFourEvent.TIME_ELAPSED,
                _board.PlayerTime[NextPlayer]
                ));
            _lastPlayer = NextPlayer;
            TimeAdvanced?.Invoke(this, new ConnectFourEventArgs(
                ConnectFourEvent.TIME_ELAPSED,
                _board.PlayerTime[NextPlayer]
                ));
        }

        public async Task SaveAsync(string path, IConnectFourDataAccess dataAccess)
        {
            try
            {
                await dataAccess.SaveAsync(path, _board);
            }
            catch (ConnectFourDataException e)
            {
                throw e;
            }
        }

        public static async Task<ConnectFourBoard> LoadBoardAsync(string path, IConnectFourDataAccess dataAccess)
        {
            try
            {
                return await dataAccess.LoadAsync(path);
            }
            catch (ConnectFourDataException e)
            {
                throw e;
            }
        }

        #endregion

        #region Private methods

        private void OnPlace(int colIndex)
        {
            GameEvent?.Invoke(this, new ConnectFourEventArgs(
                ConnectFourEvent.PLACEMENT,
                _board.PlayerTime[_lastPlayer],
                new Position(colIndex, _board.ColumnHeight(colIndex)))
                );
        }

        private void OnGameOver(int colIndex, Position[]? winning)
        {
            if(IsOngoing) {
                throw new InvalidOperationException("Nincs vége a játéknak!");
            }
            _isPaused = true;
            _timer.Stop();
            if (winning is null)
            {
                GameEvent?.Invoke(this, new ConnectFourEventArgs(
                    ConnectFourEvent.DRAW,
                    _board.PlayerTime[_lastPlayer],
                    new Position(colIndex, _board.ColumnHeight(colIndex)))
                    );
            }
            else
            {
                GameEvent?.Invoke(this, new ConnectFourEventArgs(
                    ConnectFourEvent.WIN,
                    _board.PlayerTime[_lastPlayer],
                    new Position(colIndex, _board.ColumnHeight(colIndex)),
                    winning)
                    );
            }
        }

        private void OnAdvance()
        {
            TimeAdvanced?.Invoke(this, new ConnectFourEventArgs(
                ConnectFourEvent.TIME_ELAPSED,
                _board.PlayerTime[NextPlayer]
                ));
        }
        private void AdvanceTime(Object? sender, EventArgs e)
        {
            _board.PlayerTime[NextPlayer] += TimeSpan.FromSeconds(0.1);
            OnAdvance();
        }

        public void Pause()
        {
            _isPaused = true;
            _timer.Stop();
        }

        /// <summary>
        /// Játék folytatása szünetelésből, amennyiben még nem ért véget.
        /// </summary>
        public void UnPause()
        {
            if(_board.IsOver)
            {
                return;
            }
            _isPaused = false;
            _timer.Start();
        }

        #endregion

    }
}