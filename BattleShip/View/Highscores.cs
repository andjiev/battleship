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
        List<Label> lblList;
        Point p;
        public Highscores()
        {
            p = new Point { X = 0, Y = 15 };
            lblList = new List<Label>();
            InitializeComponent();
            readFile();
            
          //  MessageBox.Show(lblList.Count.ToString());

        }

        private void Highscores_Load(object sender, EventArgs e)
        {
            foreach (Label l in lblList)
            {
            //    MessageBox.Show(l.ToString());
                l.Location = p;
                l.Font = new Font("Arial", 10);
               
                this.Controls.Add(l);
                p = new Point { X = p.X, Y = p.Y+30 };
                
             }

        }
        public void readFile()
        {
            var fs = File.OpenRead(Application.StartupPath + "\\highscores.txt");
          //  MessageBox.Show("Reading!");
            var reader = new StreamReader(fs);
            int I = 0;
            while (I<5&&reader.ReadLine()!=null)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                int p;
                int.TryParse(values[1], out p);
                //MessageBox.Show(values[0]);
                Score s = new Score(values[0], p);
                Label l = new Label();
                l.Text = (I+1)+"."+s.ToString();
                lblList.Add(l);
                I++;
            }
            reader.Dispose();

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
