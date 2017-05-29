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
        public InputScore()
        {
            InitializeComponent();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if(textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "Please enter your name."); 
            }
            else
            {
                errorProvider1.SetError(textBox1, "");
            }
        }
    }
}
