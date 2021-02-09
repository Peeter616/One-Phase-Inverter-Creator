using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inverter_single_phase
{
    class Inverter1ph : Inverter
    {
        static uint quantity = 0;
        protected uint nr;
        public static uint Quantity
        {
            get { return quantity; }
        }

        public Inverter1ph():base()
        {
            quantity += 1;
            nr = quantity;
        }
        public Inverter1ph(double R, double X, int SourceType = 1) : base(R, X, SourceType)
        {
            quantity += 1;
            nr = quantity;
        }
        override public void info()
        {
            MessageBox.Show("Parametry inwertera jednofazowego: " + "\r\n" +
                            "Rezystancja obciążenia: " + resistanceValue.ToString() + "\r\n" +
                            "Reaktancja obciążenia: " + reactanceValue.ToString() + "\r\n" +
                            "Napięcie zasilania: " + src.voltageValue.ToString() + ", przy częstotliwości zasilania: " + src.frequencyvalue.ToString() + "\r\n" +
                            "Prąd: " + currentValue.ToString() + "\r\n" +
                            "Jest to " + nr.ToString() + " stworzony inwerter jednofazowy" + "\r\n");
        }
    }
}
