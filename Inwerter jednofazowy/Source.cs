using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inverter_single_phase
{
    class Source
    {
        double U;
        double f;
        public Source(int X)
        {
            switch (X)  //konstruktor przypisujący parametry w zależności od wybranejopcji
            {
                case 1:
                    U = 230;
                    f = 50;
                    break;
                case 2:
                    U = 230;
                    f = 100;
                    break;
                case 3:
                    U = 115;
                    f = 50;
                    break;
                default:
                    MessageBox.Show("Nie wybrano rodzaju zasilania, przypisane zostanie domyślne 230V/50Hz.");
                    U = 230;
                    f = 50;
                    break;
            }
        }
        public double voltageValue
        {
            set
            {
                if (value < 0)//jesli nap ujemne zadeklaruj wartość absolutną napięcia
                    value = -value;
                else if (value == 0)//jeśli napięcie równe 0
                    MessageBox.Show("Napięcie zasilania wynosi 0. Urządzenie nie jest zasilane!");
                U = value;
            }
            get
            {
                return U;
            }
        }
        public double frequencyvalue
        {
            set
            {
                if (value <= 0) //jeśi częstotliwość mniejsz bądź równa 0
                    MessageBox.Show("Częstotliwość zasilania musi być większa od zera!");
                f = value;
            }
            get
            {
                return f;
            }
        }
        public void setSource(int X)
        {
            switch (X)  //ustawianie wartości par zasilania w zależności od wybranej opcji
            {
                case 1:
                    voltageValue = 230;
                    frequencyvalue = 50;
                    break;
                case 2:
                    voltageValue = 230;
                    frequencyvalue = 100;
                    break;
                case 3:
                    voltageValue = 115;
                    frequencyvalue = 50;
                    break;
                default:
                    MessageBox.Show("Nie wybrano rodzaju zasilania, przypisane zostanie domyślne 230V/50Hz.");
                    voltageValue = 230;
                    frequencyvalue = 50;
                    break;
            }
        }
    }
}
