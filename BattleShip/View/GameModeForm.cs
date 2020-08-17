using BattleShip.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip.View
{
    public partial class GameModeForm : Form
    {

        public GameModeForm()
        {
            InitializeComponent();
        }

        // I created this accidentally in the designer and now it's dangerous to delete
        private void button1_Click(object sender, EventArgs e)
        {
            Game game = new Game();
            this.Hide();
            DialogResult result = game.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string fileName = path + "/game.bs";
                using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, game.state);
                }
                if(Game.isFinished)
                {
                    File.Delete(fileName);
                }
                this.Show();
            }
        }

        private void PlayerVsAiButton_Click(object sender, EventArgs e)
        {
        }
    }
}
