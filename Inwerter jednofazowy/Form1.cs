using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inverter_single_phase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//Tworzenie obiektu
        {
            Inverter Inw1 = new Inverter1ph();
            Inw1.info();
        }

        private void button2_Click(object sender, EventArgs e)//Tworzenie obiektu(konstruktor przeciążony)
        {
            Inverter1ph Inw1 = new Inverter1ph(Convert.ToDouble(numR.Value), Convert.ToDouble(numX.Value));
            Inw1.info();
        }

        private void button3_Click(object sender, EventArgs e)//Otwieranie okna do edycji włąsciwej części programu
        {
            Form form = new Editor_Inverter_single_phase();
            form.ShowDialog();
        }
    }
}
