using EVAL.ConnectFour.Common;
using EVAL.ConnectFour.Model;
using EVAL.ConnectFour.Persistence;
using System.Windows.Forms;

namespace EVAL.ConnectFour.View
{
    public partial class ConnectFourGame : Form
    {
        #region Subclasses

        protected class ConnectFourButton : Button
        {

            #region Fields

            private readonly ConnectFourGame _parent;
            private readonly int _x;
            private readonly int _y;

            #endregion

            #region Properties

            /// <summary>
            /// Gomb oszlopában az első üres mező.
            /// </summary>
            // Egy oszlopon belül bárhova kattintva ugyanannak kell történnie
            private ConnectFourButton Target { get => _parent._buttons[_x][_parent._model.ColumnHeight(_x)]; }
            /// <summary>
            /// Egy gombra rajzolt szöveg alapértelmezett betűtípusa.
            /// </summary>
            new static Font DefaultFont { get => new Font(FontFamily.GenericSansSerif, 12); }

            #endregion

            #region Constructors

            public ConnectFourButton(ConnectFourGame parent, int x, int y) : base()
            {
                _x = x;
                _y = y;
                _parent = parent;
                this.Font = DefaultFont;
                MouseEnter += ConnectFourButton_MouseEnter;
                MouseLeave += ConnectFourButton_MouseLeave;
                MouseClick += ConnectFourButton_MouseClick;
            }

            #endregion

            #region Private methods

            #region Mouse event handlers

            private void ConnectFourButton_MouseClick(object? sender, MouseEventArgs e)
            {
                if (
                    e.Button != MouseButtons.Left ||                          // csak balegérgomb-nyomásra történik akció
                    !_parent._model.IsOngoing ||                              // csak folyamatban lévő játékban történik akció
                    _parent._model.ColumnHeight(_x) >= _parent._model.Rows    // csak szabad oszlopba rakhatunk
                    )
                {
                    return;
                }

                _parent._model.Place(_x);
                ConnectFourButton_MouseLeave(sender, e);
                ConnectFourButton_MouseEnter(sender, e);
            }

            private void ConnectFourButton_MouseEnter(object? sender, EventArgs e)
            {
                if (!_parent._model.IsOngoing) return;
                if (_parent._model.ColumnHeight(_x) >= _parent._model.Rows) return;

                Target.ForeColor = Color.Gray;
                Target.Text = ((char)_parent._model.NextPlayer).ToString(); // lehetne okosabban, de nincs rá szükség
            }

            private void ConnectFourButton_MouseLeave(object? sender, EventArgs e)
            {
                if (!_parent._model.IsOngoing) return;
                if (_parent._model.ColumnHeight(_x) >= _parent._model.Rows) return;

                Target.ForeColor = SystemColors.ControlText;
                Target.Text = "";
            }

            #endregion

            #endregion
        }

        #endregion

        #region Fields

        private readonly ConnectFourModel _model;
        private ConnectFourButton[][] _buttons;
        private TableLayoutPanel _buttonContainer;
        private readonly Size _cellSize = new Size(40, 40);
        private readonly Dictionary<PlayerColour, Color> _playerColors;

        #endregion

        #region Constructors

        public ConnectFourGame() : this(new ConnectFourOptions(BoardSize.Small, Color.Blue, Color.Red)) { }
        public ConnectFourGame(ConnectFourOptions options) : this(new ConnectFourModel(options.BoardSize), options) { }

        public ConnectFourGame(ConnectFourModel model, ConnectFourOptions options)
        {
            _playerColors = new Dictionary<PlayerColour, Color>() {
                { PlayerColour.X, options.P1Colour },
                { PlayerColour.O, options.P2Colour },
            };
            _model = model;
            _model.GameEvent += UniversalGameEventHandler;
            _model.TimeAdvanced += UniversalGameEventHandler;
            // itt valamiért fontos a sorrend 🤨
            InitializeButtonContainer();
            InitializeComponent();
            ClientSize = new Size(_cellSize.Width * _model.Columns, 27 + 27 + 1 + _cellSize.Height * _model.Rows); // 27: toolstrip, 27: menustrip, 1: margin
            GenerateBoard();

            _model.Replay();
        }

        #endregion

        #region Private methods

        #region Initialization methods

        private void InitializeButtonContainer()
        {
            _buttonContainer = new TableLayoutPanel()
            {
                Name = "buttonContainer",
                ColumnCount = _model.Columns,
                RowCount = _model.Rows,
                Dock = DockStyle.Fill,
            };
            for (int i = 0; i < _model.Columns; ++i)
            {
                _buttonContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _cellSize.Width));
            }
            for (int i = 0; i < _model.Rows; ++i)
            {
                _buttonContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellSize.Height));
            }
            Controls.Add(_buttonContainer);
        }

        private void GenerateBoard()
        {
            _buttons = new ConnectFourButton[_model.Columns][];

            for (int i = 0; i < _model.Columns; ++i)
            {
                _buttons[i] = new ConnectFourButton[_model.Rows];
                for (int j = 0; j < _model.Rows; ++j)
                {
                    ConnectFourButton b = new ConnectFourButton(this, i, j)
                    {
                        Text = "",
                        Size = _cellSize,
                        BackColor = SystemColors.ControlLightLight,
                        FlatStyle = FlatStyle.Flat,
                        Dock = DockStyle.Fill,
                        //TabIndex = i,
                        //Enabled = j == 0,
                        Margin = new Padding(0),
                        Padding = new Padding(0)
                    };
                    _buttonContainer.Controls.Add(b);
                    _buttonContainer.SetCellPosition(b, new TableLayoutPanelCellPosition(i, j));
                    _buttons[i][_model.Rows - j - 1] = b;
                }
            }

        }

        #endregion

        #region Event handlers

        private void UniversalGameEventHandler(object? sender, ConnectFourEventArgs e)
        {
            if (
            sender is not ConnectFourModel model ||
            model != _model
                )
            {
                return;
            }
            switch (e.EventType)
            {
                case ConnectFourEvent.TIME_ELAPSED:
                    UpdateTimer(e.GameTime);
                    break;
                case ConnectFourEvent.PLACEMENT:
                    DrawPlace(e.Position);
                    break;
                case ConnectFourEvent.WIN:
                    DrawWin(e.Winner);
                    break;
                case ConnectFourEvent.DRAW:
                    DrawDraw();
                    break;
                default:
                    throw new ArgumentException($"Ismeretlen eseménytípus: \"${e.EventType}\".");
            }
        }

        private void DrawDraw()
        {
            toolStripLabelStatus.Text = "Döntetlen!";
            for (int i = 0; i < _model.Columns; ++i)
            {
                for (int j = 0; j < _model.Rows; ++j)
                {
                    _buttons[i][j].ForeColor = SystemColors.ControlText;
                    _buttons[i][j].Enabled = false;
                }
            }

        }

        private void DrawWin(Position[]? winner)
        {
            PauseUnPause();
            toolStripLabelStatus.Text = ((char)_model.LastPlayer).ToString() + " nyert!";
            if (winner is null) return;
            for (int i = 0; i < _model.Columns; ++i)
            {
                for (int j = 0; j < _model.Rows; ++j)
                {
                    _buttons[i][j].ForeColor = SystemColors.ControlText;
                    _buttons[i][j].Enabled = false;
                }
            }
            foreach (var p in winner)
            {
                ButtonAt(p).ForeColor = _playerColors[_model.LastPlayer];
                ButtonAt(p).Enabled = true;
                ButtonAt(p).Focus();
            }
        }

        private void DrawPlace(Position? position)
        {
            if (position is null) return;

            ConnectFourButton target = ButtonAt(position);

            target.ForeColor = _playerColors[_model.NextPlayer];
            target.Font = new Font(DefaultFont, FontStyle.Bold);
            target.Text = ((char)_model.NextPlayer).ToString();
        }

        private void UpdateTimer(TimeSpan newTime)
        {
            ToolStripLabel timerLabel = _model.NextPlayer switch
            {
                PlayerColour.X => toolStripLabelP1Timer,
                PlayerColour.O => toolStripLabelP2Timer,
                _ => throw new InvalidOperationException()
            };
            timerLabel.Text = $"{(char)_model.NextPlayer}: {newTime.ToString("mm':'ss")}";
        }

        private async void saveExitToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            if(await SaveGameAsync())
                Close();
        }

        private async void saveToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            await SaveGameAsync();
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            PauseUnPause();
        }

        #endregion

        #region Other private methods

        /// <summary>
        /// Adott pozícióban elhelyezkedő gomb lekérdezése a játékablakról.
        /// </summary>
        /// <param name="pos">Pozíció.</param>
        /// <returns>Gomb.</returns>
        private ConnectFourButton ButtonAt(Position pos)
        {
            if (pos is null) return null!;
            return _buttons[pos.X][pos.Y];

        }

        /// <summary>
        /// Futó játék esetén szünetelteti, szünetelő játék esetén folytatja azt. Ha már véget ért a játék, nem csinál semmit.
        /// </summary>
        private void PauseUnPause()
        {
            if (_model.IsOngoing)
            {
                _model.Pause();
            }
            else
            {
                _model.UnPause();
            }

            if (_model.IsPaused)
            {
                toolStripButtonPause.ToolTipText = "Folytatás";
                toolStripButtonPause.Text = "▷";
            }
            else
            {
                toolStripButtonPause.ToolTipText = "Szüneteltetés";
                toolStripButtonPause.Text = "| |";
            }
        }

        /// <summary>
        /// Aktív játék mentése. Mentés közben fellépő hiba esetén felugró ablakot jelenít meg a hibáról.
        /// </summary>
        /// <returns>Mentés sikeressége.</returns>
        private async Task<bool> SaveGameAsync()
        {
            // mentés közben leállítjuk a játékot
            bool paused = _model.IsPaused;
            bool success = true;
            if (!paused)
            {
                PauseUnPause();    
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Játék mentése";
            dialog.Filter = "Potyogós Amőba fájlformátum (*.cfs)|*.cfs|Minden fájl (*.*)|*.*";
            try
            {
                DialogResult res = dialog.ShowDialog();
                if (res == DialogResult.OK)
                {
                    await _model.SaveAsync(dialog.FileName, new ConnectFourTextFileDataAccess());
                }
                else
                {
                    success = false;
                }
            }
            catch (ConnectFourDataException)
            {
                new ErrorPopup($"Sikertelen mentés! (Fájlnév: {dialog.FileName})").ShowDialog();
                success = false;
            }

            // szükség esetén folytatjuk a játékot
            if(!paused)
            {
                PauseUnPause();
            }
            return success;
        }

        #endregion

        #endregion
    }

    public class ErrorPopup : Form
    {
        /// <summary>
        /// Hibaüzenetet tartalmazó felugró ablak generálása.
        /// </summary>
        /// <param name="message">Hibaüzenet.</param>
        /// <returns><c>Form</c> a hibaüzenet szövegével.</returns>
        public ErrorPopup(string message) : base()
        {
            Text = "HIBA";
            MaximizeBox = false;
            MinimizeBox = false;
            Size = new Size(400, 150);
            StartPosition = FormStartPosition.CenterParent;
            Label label = new Label()
            {
                Dock = DockStyle.Fill,
                Text = message
            };
            Controls.Add(label);
        }
    }
}
