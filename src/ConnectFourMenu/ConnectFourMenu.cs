using EVAL.ConnectFour.Model;
using EVAL.ConnectFour.Persistence;

namespace EVAL.ConnectFour.View
{
    public partial class ConnectFourMenu : Form
    {

        #region Fields

        private ConnectFourOptions _options;

        #endregion

        #region Constructors

        public ConnectFourMenu()
        {
            _options = new ConnectFourOptions();
            InitializeComponent();
        }

        #endregion

        #region Private methods

        #region Event handlers

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            ConnectFourGame game = new ConnectFourGame(_options);
            game.FormClosed += (object? o, FormClosedEventArgs e) => Show(); //game.Disposed??
            Hide();
            game.Show();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            ConnectFourSettings settings = new ConnectFourSettings(_options);
            settings.FormClosed += (object? o, FormClosedEventArgs e) => Show(); //game.Disposed??
            Hide();
            settings.Show();
        }

        private async void buttonLoadGame_Click(object sender, EventArgs e)
        {
            string defaultText = buttonLoadGame.Text;
            buttonLoadGame.Text = "Betöltés...";
            buttonLoadGame.Enabled = false;
            ConnectFourGame? game = await LoadSaveAsync();
            buttonLoadGame.Enabled = true;
            buttonLoadGame.Text = defaultText;

            if (game is not null)
            {
                game.FormClosed += (object? o, FormClosedEventArgs e) => Show(); //game.Disposed??
                Hide();
                game.Show();
            }
        }

        #endregion

        #region Other private methods

        private async Task<ConnectFourGame?> LoadSaveAsync()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Játék betöltése";
            dialog.Filter = "Potyogós Amőba fájlformátum (*.cfs)|*.cfs|Minden fájl (*.*)|*.*";
            try
            {
                DialogResult res = dialog.ShowDialog();
                if (res == DialogResult.OK)
                {
                    return new ConnectFourGame(
                        new ConnectFourModel(
                            await ConnectFourModel.LoadBoardAsync(dialog.FileName, new ConnectFourTextFileDataAccess())
                            ),
                        _options
                        );
                }
            }
            catch (ConnectFourDataException)
            {
                new ErrorPopup($"Sikertelen betöltés! (Fájlnév: {dialog.FileName})").ShowDialog();
            }
            return null;
        }

        #endregion

        #endregion
    }
}
