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
        List<Score> hiScores;
        Point p;
        public Highscores()
        {
            p = new Point { X = 0, Y = 15 };
            lblList = new List<Label>();
            InitializeComponent();
            hiScores = new List<Score>();
            readFile();
            
          //  MessageBox.Show(lblList.Count.ToString());

        }

        private void Highscores_Load(object sender, EventArgs e)
        {
            foreach (Label l in lblList)
            {
            //    MessageBox.Show(l.ToString());
                l.Location = p;
                l.Font = new Font("Arial", 9);
               
                this.Controls.Add(l);
                p = new Point { X = p.X, Y = p.Y+25 };
                
             }

        }
        public void readFile()
        {
            var fs = File.OpenRead(Application.StartupPath + "\\highscores.txt");
          //  MessageBox.Show("Reading!");
            var reader = new StreamReader(fs);
            var line="";
            while (( line=reader.ReadLine())!=null)
            {
               

              //  MessageBox.Show(line);
                if (line == "")
                {
                    break;
                }
                var values = line.Split(';');
                int p;
                int.TryParse(values[1], out p);
                //MessageBox.Show(values[0]);
                Score s = new Score(values[0], p);
               // Label l = new Label();
                hiScores.Add(s);
                //l.Text = (I+1)+"."+s.ToString();
                //lblList.Add(l);

               // I++;
            }
            Sort();
            reader.Dispose();

        }
        private void Sort() {
            for(int i = 0; i < hiScores.Count; i++)
            {
                for(int j = 0; j < hiScores.Count - 1; j++)
                {
                    if (hiScores[j].Hiscore > hiScores[j + 1].Hiscore) {
                        Score tmp = hiScores[j];
                        hiScores[j] = hiScores[j + 1];
                        hiScores[j + 1] = tmp;
                            }
                }
            }
            for(int i = 0; i < 5; i++) 
            {
                Label l = new Label();
                l.Text = (i + 1) + "." + hiScores[i].ToString();
                lblList.Add(l);
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
