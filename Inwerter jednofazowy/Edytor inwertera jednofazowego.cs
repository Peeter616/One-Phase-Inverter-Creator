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
    public partial class Editor_Inverter_single_phase : Form
    {
        public Editor_Inverter_single_phase()
        {
            InitializeComponent();
        }
        Inverter inw1;
        private void newR_KeyPress(object sender, KeyPressEventArgs e) //pozwala wpisywać jedynie cyfry oraz tylko jeden przecinek (w tym przypadku kropkę)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void newX_KeyPress(object sender, KeyPressEventArgs e) //pozwala wpisywać jedynie cyfry oraz tylko jeden przecinek (w tym przypadku kropkę) oraz jedynie jeden znak wartości ujemnej
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            if (e.KeyChar == '-' && (sender as TextBox).Text.Length > 0)
            {
                e.Handled = true;
            }
        }
        bool reset;
        private void button1_Click(object sender, EventArgs e) //dodawanie wartości rezystancji oraz reaktancji
        {
            double nR = Convert.ToDouble(newR.Text);
            double nX = Convert.ToDouble(newX.Text);
            double aR = Convert.ToDouble(actualR.Text);
            double aX = Convert.ToDouble(actualX.Text);
            if(reset == true)
            {
                reset = false;
                button2.Visible = true;
                button5.Visible = true;
                aR += nR;
                aX += nX;
                inw1.currentChange(aR, aX);
            }
            else
            {
                aR += nR;
                aX += nX;
                inw1.currentChange(nR, nX);
            }
            actualR.Text = Convert.ToString(aR);
            actualX.Text = Convert.ToString(aX);
        }

        private void button2_Click(object sender, EventArgs e) //wyzerowywanie zaprogramowanego obciążenia
        {
            actualR.Text = "0";
            actualX.Text = "0";
            reset = true;
            button2.Visible = false;
            button5.Visible = false;
            inw1.clearChart(chart1);
            inw1.resetTimeOfPeriod();
        }

        private void button3_Click(object sender, EventArgs e)//Tworzenie obiektu
        {
            if((string.IsNullOrWhiteSpace(newR.Text)) || (string.IsNullOrWhiteSpace(newX.Text)))
                MessageBox.Show("Pola określające wartości rezystancji i reaktancji nie mogą pozostać puste!");
            else
            {
                double nR = Convert.ToDouble(newR.Text);
                double nX = Convert.ToDouble(newX.Text);
                actualR.Text = newR.Text;
                actualX.Text = newX.Text;
                int PST = 1;
                if (radioButton1.Checked) PST = 1;
                else if (radioButton2.Checked) PST = 2;
                else if (radioButton3.Checked) PST = 3;
                else
                {
                    MessageBox.Show("Nie wybrano rodzaju zasilania. Przypisane zostało domyślne, 230V/50Hz.");
                    PST = 1;
                }

                inw1 = new Inverter1ph(nR, nX, PST);
                button3.Visible = false;
                button1.Visible = true;
                button2.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                chart1.Visible = true;
            }
        }

        private void button7_Click(object sender, EventArgs e) //Zmiana zasilania przy zachowaniu poprzednich przebiegów na wykresie
        {
            int x;
            if (radioButton1.Checked) x = 1;
            else if (radioButton2.Checked) x = 2;
            else if (radioButton3.Checked) x = 3;
            else
            {
                MessageBox.Show("Nie wybrano rodzaju zasilania. Przypisane zostało domyślne, 230V/50Hz.");
                x = 1;
            }
            inw1.setSource(x);
        }

        private void button5_Click(object sender, EventArgs e)//Funkcja rysująca wykres
        {
            inw1.drawCurrentChart(inw1.resistanceValue, inw1.reactanceValue, chart1);
        }

        private void button6_Click(object sender, EventArgs e)//odejmowanie wartości rezystancji oraz reaktancji
        {
            double nR = Convert.ToDouble(newR.Text);
            double nX = Convert.ToDouble(newX.Text);
            double aR = Convert.ToDouble(actualR.Text);
            double aX = Convert.ToDouble(actualX.Text);
            if (reset == true)
            {
                if (aR - nR > 0) 
                {
                    reset = false;
                    button2.Visible = true;
                    button5.Visible = true;
                    aR -= nR;
                    aX -= nX;
                    actualR.Text = Convert.ToString(aR);
                    actualX.Text = Convert.ToString(aX);
                    inw1.currentChange(-aR, -aX);
                }
                else
                {
                    MessageBox.Show("Wartość rezystancji musi być dodatnia!");
                }
            }
            else
            {
                if (aR - nR > 0)
                {
                    aR -= nR;
                    aX -= nX;
                    actualR.Text = Convert.ToString(aR);
                    actualX.Text = Convert.ToString(aX);
                    inw1.currentChange(-nR, -nX);
                }
                else
                {
                    MessageBox.Show("Wartość rezystancji musi być dodatnia!");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)//resetowanie wykresu
        {
            inw1.clearChart(chart1);
            inw1.resetTimeOfPeriod();
        }
    }
}
