using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleShip.Controller;

namespace BattleShip
{
    public partial class Form1 : Form
    {
        ShipsController controller;
        public Form1()
        {
            InitializeComponent();
            controller = new ShipsController();
        }
    }
}
