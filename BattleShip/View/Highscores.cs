using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BattleShip.Model;
namespace BattleShip.View
{
    public partial class Highscores : Form
    {
        public Highscores()
        {
            InitializeComponent();
        }

        private void Highscores_Load(object sender, EventArgs e)
        {
           

        }
        public void readFile()
        {
            var fs = File.OpenRead(Application.StartupPath + "\\highscores.txt");
          //  MessageBox.Show("Reading!");
            var reader = new StreamReader(fs);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                int p;
                int.TryParse(values[1], out p);
                //MessageBox.Show(values[0]);
                Score s = new Score(values[0], p);
                lbHighscores.Items.Add(s);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
           // readFile();
        }

        private void Highscores_Shown(object sender, EventArgs e)
        {
            readFile();
        }
    }
}
