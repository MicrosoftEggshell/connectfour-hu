using EVAL.ConnectFour.Common;

namespace EVAL.ConnectFour.Persistence
{
    /// <summary>
    /// ConnectFour játéktábla.
    /// </summary>
    public class ConnectFourBoard
    {
        #region Subclasses

        /// <summary>
        /// Az utolsó WinningMove ellenőrzés eredményének az elmentése arra az esetre, ha a lépés valóban végremegy.
        /// </summary>
        private class WinCache {
            private ConnectFourBoard _parent;
            private PlayerColour _player;
            private int _x;
            private int _y;
            public bool isWin;

            public WinCache(ConnectFourBoard parent, int x, int y, PlayerColour player, bool isWin)
            {
                _parent = parent;
                _player = player;
                _x = x;
                _y = y;
                this.isWin = isWin;
            }

            public bool isHit(int x, PlayerColour player)
            {
                return _x == x && _y == _parent.ColumnHeight(x) && player == _player;
            }
        }

        /// <summary>
        /// Egy oszlopba dobott "korongok" értékei, alulról felfelé.
        /// </summary>
        public class Column
        {

            #region Fields

            private int _next;
            private readonly ConnectFourBoard _parent;
            private readonly PlayerColour[] _values;

            #endregion

            #region Properties

            public bool IsFull { get => _next >= _parent.Height; }

            public int Height { get => _next; }

            public PlayerColour this[int i]
            {
                get
                {
                    //if (i >= _parent.Height || i < 0)
                    //{
                    //    throw new IndexOutOfRangeException();
                    //}
                    if (i >= _next)
                    {
                        return PlayerColour.NONE;
                    }
                    return _values[i];
                }
            }

            #endregion

            #region Constructors

            public Column(ConnectFourBoard parent)
            {
                _next = 0;
                _parent = parent;
                _values = new PlayerColour[_parent.Height];
            }

            #endregion

            #region Public methods

            public void Insert(PlayerColour player)
            {
                if (IsFull)
                {
                    throw new ArgumentException("Column full.");
                }
                _values[_next++] = player;
            }

            #endregion

        }

        #endregion

        #region Fields

        private readonly int _width;
        private readonly int _height;
        private readonly Column[] _columns; // mezők értékei
        private List<int> _moves;
        private bool _isOver;
        private Dictionary<PlayerColour, TimeSpan> _playerTime;
        private WinCache _winCache;

        #endregion

        #region Properties

        /// <summary>
        /// Tábla magassága.
        /// </summary>
        public int Height => _height;
        /// <summary>
        /// Tábla szélessége.
        /// </summary>
        public int Width => _width;

        /// <summary>
        /// Van-e szabad mező a táblán.
        /// </summary>
        public bool IsFull
        {
            get
            {
                foreach (Column c in _columns)
                    if (!c.IsFull)
                        return false;
                return true;
            }
        }

        public bool IsOver => _isOver;

        ///// <summary>
        ///// Mező értékének lekérdezése.
        ///// </summary>
        ///// <param name="x">Vízszintes koordináta.</param>
        ///// <param name="y">Függőleges koordináta (alulról felfelé indexelve).</param>
        ///// <returns>Mező értéke.</returns>
        // public PlayerColour this[int x, int y] { get => _columns[x][y]; }

        /// <summary>
        /// A játék folyamán megtett lépések listája időrendi sorrendben (oszlopindexekként). 
        /// </summary>
        protected internal List<int> Moves => _moves;

        public Dictionary<PlayerColour, TimeSpan> PlayerTime => _playerTime;

        #endregion

        #region Constructors

        /// <summary>
        /// ConncetFour játéktábla példányosítása.
        /// </summary>
        public ConnectFourBoard() : this(7, 6) { } // Klasszikus játék pályamérete

        /// <summary>
        /// ConncetFour játéktábla példányosítása.
        /// </summary>
        /// <param name="width">Játéktábla mérete.</param>
        /// <param name="height">Ház mérete.</param>
        public ConnectFourBoard(int width, int height)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Negative table width.");
            if (height < 0)
                throw new ArgumentOutOfRangeException(nameof(height), "Negative table height.");
            if (width < 4 && height < 4)
                throw new ArgumentOutOfRangeException($"Table width or height must be at least 4. (width: {width}, height: {height})");

            _isOver = false;
            _height = height;
            _width = width;
            _columns = new Column[width];
            _moves = new List<int>();
            for (int i = 0; i < width; ++i)
            {
                _columns[i] = new Column(this);
            }
            _playerTime = new Dictionary<PlayerColour, TimeSpan>
                {
                    { PlayerColour.X, new TimeSpan() },
                    { PlayerColour.O, new TimeSpan() }
                };
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adott sorszámú oszlopban szabad hely ellenőrzése.
        /// </summary>
        /// <see cref="Column.IsFull"/>
        /// <param name="colIndex">Oszlop sorszáma.</param>
        /// <returns>Van-e szabad hely adott oszlopban.</returns>
        public bool CanInsert(int colIndex) {
            if(_isOver || colIndex < 0 || colIndex >= _columns.Length)
                return false;
            return !_columns[colIndex].IsFull;
        }

        /// <summary>
        /// Korong dobása adott sorszámú oszlopba.
        /// </summary>
        /// <remarks>Nem megfelelő index esetén <c>IndexOutOfRangeException</c>-t vált ki.</remarks>
        /// <param name="colIndex">Oszlop sorszáma.</param>
        /// <param name="player">Játékos.</param>
        public void Insert(int colIndex, PlayerColour player)
        {
            if (_isOver) return;
            if (_winCache?.isHit(colIndex, player) ?? false)
            {
                _isOver = _winCache.isWin;
            } else
            {
                _isOver = WinningMove(colIndex, player) is not null;
            }
            _columns[colIndex].Insert(player);
            _moves.Add(colIndex);
            _isOver |= IsFull;
        }

        public int ColumnHeight(int colIndex) => _columns[colIndex].Height;

        /// <summary>
        /// Adott lépés megnyeri-e a játékot.
        /// </summary>
        /// <param name="colIndex">Megjátszott oszlop sorszáma.</param>
        /// <param name="player">Játékos színe.</param>
        /// <returns>Nyertes korongok pozícióinak listája. <c>null</c>, ha nem létezik.</returns>
        /// <exception cref="ArgumentException"></exception>
        public Position[]? WinningMove(int colIndex, PlayerColour player)
        {
            if (player == PlayerColour.NONE)
            {
                throw new ArgumentException("Invalid player value.");
            }
            if (_columns[colIndex].IsFull) // hagyjuk, hogy IndexOutOfRangeException-t váltson ki szükség esetén
                throw new ArgumentException($"Column {colIndex} is full.");

            Position[]? win;

            // eredmény elmentése arra az esetre, ha utána közvetlen beillesztésre kerülne
            _winCache = new WinCache(this, colIndex, ColumnHeight(colIndex), player, true);
            
            win = WinInDirection(colIndex, player, 1, 0);  // vízszíntesen
            if (win is not null) return win;
            win = WinInDirection(colIndex, player, 1, 1);  // jobbfent-ballent átlón
            if (win is not null) return win;
            win = WinInDirection(colIndex, player, 0, 1);  // függőlegesen
            if (win is not null) return win;
            win = WinInDirection(colIndex, player, -1, 1);  // balfent-jobblent átlón
            if (win is not null) return win;

            _winCache.isWin = false;

            return null;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Adott irányban (🡺 🡽 🡹) összegyűlne-e a 4 azonos szín egy lépés után.
        /// <c>colIndex</c> és <c>player</c> paraméterekről feltételezzük, hogy helyesek!
        /// </summary>
        /// <param name="colIndex">Megjátszott oszlop sorszáma.</param>
        /// <param name="player">Játékos színe.</param>
        /// <param name="horizDir">Vízszíntes irány. (-1: bal, 0: 0, 1: jobb)</param>
        /// <param name="vertDir">Függőleges irány. (-1: le, 0: 0, 1: fel)</param>
        /// <returns>Nyertes korongok pozícióinak listája. <c>null</c>, ha nem létezik.</returns>
        private Position[]? WinInDirection(int colIndex, PlayerColour player, int horizDir, int vertDir)
        {
            if(horizDir < -1 || horizDir > 1 || vertDir < -1 || vertDir > 1)
            {
                throw new ArgumentOutOfRangeException(
                    $"Directions must be between -1 and 1. ({nameof(horizDir)}: {horizDir}, {nameof(vertDir)}: {vertDir})"
                );
            }
            if (vertDir == 0 && horizDir == 0)
            {
                return null;
            }

            Position[] ret = new Position[4];
            for (int offset = 0; offset < 4; ++offset) // új korong pozíciója a vizsgált négyesben (0..3)
            {
                for (int i = 0; i < 4; ++i) // épp vizsgált korong pozíciója a négyesben (0..3)
                {
                    int x = colIndex + horizDir * (i - offset);
                    int y = ColumnHeight(colIndex) + vertDir * (i - offset);
                    try
                    {
                        if (_columns[x][y] != player && i != offset) // ha i == offset, az új korongról van szó
                        {
                            break;
                        }
                        ret[i] = new Position(x, y);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        break;
                    }
                }
                if (ret[3] is not null) { 
                    return ret;
                }
            }
            return null;
        }

        #endregion
    }
}
