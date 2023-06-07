using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EVAL.ConnectFour.Model;

namespace EVAL.ConnectFour.View
{
    public partial class ConnectFourSettings : Form
    {
        private static Dictionary<string, BoardSize> _sizes = new Dictionary<string, BoardSize>() {
            { "Kicsi (10x10)", BoardSize.Small },
            { "Közepes (20x20)", BoardSize.Medium },
            { "Nagy (30x30)", BoardSize.Large }
        };

        private ConnectFourOptions _options;
        private ConnectFourOptions _optionsTemp;
        public ConnectFourSettings(ConnectFourOptions options)
        {
            _options = options;
            _optionsTemp = new ConnectFourOptions(options);
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            buttonP1Colour.Text = _optionsTemp.P1Colour.Name;
            buttonP2Colour.Text = _optionsTemp.P2Colour.Name;
            comboBoxBoardSize.Items.AddRange(_sizes.Keys.ToArray());
            comboBoxBoardSize.SelectedIndex = _sizes.Values.ToList().IndexOf(_options.BoardSize);
        }

        private void buttonP1Colour_Click(object sender, EventArgs e)
        {
            colorDialog.FullOpen = _options.P1Colour.IsSystemColor;
            colorDialog.Color = _optionsTemp.P1Colour;
            DialogResult res = colorDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                _optionsTemp.P1Colour = colorDialog.Color;
            }
            buttonP1Colour.Text = _optionsTemp.P1Colour.Name;
        }

        private void buttonP2Colour_Click(object sender, EventArgs e)
        {
            colorDialog.FullOpen = _options.P2Colour.IsSystemColor;
            colorDialog.Color = _optionsTemp.P2Colour;
            DialogResult res = colorDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                _optionsTemp.P2Colour = colorDialog.Color;
            }
            buttonP2Colour.Text = _optionsTemp.P2Colour.Name;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _options.SetClone(_optionsTemp);
            Close();
        }

        private void comboBoxBoardSize_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _optionsTemp.BoardSize = _sizes[(string)comboBoxBoardSize.SelectedItem];
        }
    }
}
