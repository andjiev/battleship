using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            this.Cursor = LoadCursorFromResource();
        }

        public static Cursor LoadCursorFromResource()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/curs.cur";
            File.WriteAllBytes(path, Properties.Resources.AOM_Titans_Cursor);
            Cursor result = new Cursor(LoadCursorFromFile(path));
            File.Delete(path);

            return result;
        }
        [DllImport("User32.dll", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        private static extern IntPtr LoadCursorFromFile(String str);

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
