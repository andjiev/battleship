using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip.View
{
    public partial class InputScore : Form
    {
        public string winnerName;

        public InputScore()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == (int)Keys.Enter && textBox1.Text.Count() > 0)
            {
                winnerName = textBox1.Text;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
