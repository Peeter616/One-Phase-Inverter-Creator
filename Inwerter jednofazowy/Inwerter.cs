using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inverter_single_phase
{
    abstract class Inverter
    {
        protected double R;
        protected double X;
        protected double I;
        protected Source src;
        protected double timeOfPeriod = 0;

        public Inverter()
        {
            R = 100;             //rezystancja
            X = 0;               //reaktancj
            src = new Source(1);//wybieranie rodz zailania
            currentChange(R, X);//obliczanie pradu
        }
        public Inverter(double R, double X, int SourceType)
        {
            src = new Source(SourceType);//wybieranie rodz zailania
            this.R = R;                  //rezystancja
            this.X = X;                  //reaktancj
            I = currentCalculation(R, X);//obliczanie pradu
        }

        public double resistanceValue
        {
            set
            {
                if (value <= 0)
                    MessageBox.Show("Wartośc rezystancji musi być dodatnia!");
                else
                    R = value;
            }
            get
            {
                return R;
            }
        }
        public double reactanceValue
        {
            set
            {
                X = value * 0.001;
            }
            get
            {
                return X;
            }
        }
        public double currentValue
        {
            get
            {
                return I;
            }
        }
        protected double currentCalculation(double R, double X)
        {
            double current;
            current = src.voltageValue / (Math.Sqrt(R * R + X * 0.001 * X * 0.001));
            return current;
        }
        abstract public void info();

        public void setSource(int X) { src.setSource(X); }

        public void currentChange(double R, double X)
        {
            resistanceValue += R;
            reactanceValue += X;
            I = currentCalculation(resistanceValue, reactanceValue);
        }
        public double currentPeriod(double R, double X, double t)
        {
            double current;
            current = (src.voltageValue / (Math.Sqrt(R * R + X * 0.001 * X * 0.001))) * Math.Sin(2 * Math.PI * src.frequencyvalue * t);
            return current;//obliczanie pradu na podst rezystancji reaktancji oraz kąta przesuniecia
        }
        public void resetTimeOfPeriod()
        {
            timeOfPeriod = 0;//resetowanie okresu
        }

        public void drawCurrentChart(double R, double X, System.Windows.Forms.DataVisualization.Charting.Chart wykres)
        {
            wykres.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//wykres liniowy
            for (double i = timeOfPeriod; i < timeOfPeriod + 1 / src.frequencyvalue; i += Math.PI / 360 * 1 / src.frequencyvalue) 
                wykres.Series[0].Points.AddXY(i, currentPeriod(R,X,i));//rysowanie wykresu
            timeOfPeriod += 1 / src.frequencyvalue;//zwiekszanie czasu dla ktorego obliczany bedzie nastepny krok
        }

        public void clearChart(System.Windows.Forms.DataVisualization.Charting.Chart wykres)
        {
            wykres.Series[0].Points.Clear();//czyszczenie wykresu
        }
    }
}
