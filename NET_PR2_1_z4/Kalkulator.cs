using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_PR2_1_z4
{
    class Kalkulator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string wynik = "0";
        private string operacja = null;
        private double? operandLewy = null;
        private double? operandPrawy = null;

        public string Wynik
        {
            get => wynik;
            set
            {
                wynik = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Wynik"));
            }
        }

        public string Działanie
        {
            get
            {
                if (operandLewy == null)
                    return "";
                else if (operandPrawy == null)
                    return $"{operandLewy} {operacja}";
                else
                    return $"{operandLewy} {operacja} {operandPrawy} = ";
            }
        }

        internal void WprowadźCyfrę(string cyfra)
        {
            if (wynik == "0")
            {
                if (cyfra == "0")
                    return;
                else
                    Wynik = cyfra;
            }
            else
            {
                Wynik += cyfra;
            }
        }

        internal void WprowadźPrzecinek()
        {
            if (wynik.Contains(','))
                return;
            else
                Wynik += ',';
        }

        internal void ZmieńZnak()
        {
            if (wynik == "0")
                return;
            else if (wynik[0] == '-')
                Wynik = wynik.Substring(1);
            else
                Wynik = '-' + wynik;
        }

        internal void KasujZnak()
        {
            if (wynik == "0")
                return;
            else if (wynik.Length == 1 || (wynik.Length == 2 && wynik[0] == '-') || wynik == "-0,")
                Wynik = "0";
            else
                Wynik = wynik.Substring(0, wynik.Length - 1);
        }

        internal void CzyśćWszystko()
        {
            CzyśćWynik();
            operacja = null;
            operandLewy = operandPrawy = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Działanie"));
        }

        internal void CzyśćWynik()
        {
            Wynik = "0";
        }

        internal void WprowadźOperacja(string operacja)
        {
            if (this.operacja != null)
            {
                WykonajDziałanie();
                this.operacja = operacja;
            }
            else
            {
                operandLewy = Convert.ToDouble(wynik);
                this.operacja = operacja;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Działanie"));
            }

            wynik = "0";
        }

        internal void WykonajDziałanie()
        {
            if (operandPrawy == null)
            {
                if (wynik == "0")
                    operandPrawy = operandLewy;
                else
                    operandPrawy = Convert.ToDouble(wynik);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Działanie"));

            if (operacja == "+")
                Wynik = (operandLewy + operandPrawy).ToString();
            if (operacja == "-")
                Wynik = (operandLewy - operandPrawy).ToString();
            if (operacja == "*")
                Wynik = (operandLewy * operandPrawy).ToString();
            if (operacja == "/")
                Wynik = (operandLewy / operandPrawy).ToString();
            if (operacja == "x^y")
                Wynik = (Math.Pow(operandLewy.Value, operandPrawy.Value)).ToString();
            if (operacja == "x%y")
                Wynik = (operandLewy % operandPrawy).ToString();

            operandLewy = Convert.ToDouble(wynik);
        }

        internal void Pierwiastkuj()
        {
            Wynik = Math.Sqrt(Convert.ToDouble(wynik)).ToString();
        }

        internal void Odrotnosc()
        {
            Wynik = (1 / Convert.ToDouble(wynik)).ToString();
        }

        internal void Silnia()
        {
            int res = 1;
            for(int i = 1; i <= Convert.ToInt32(Math.Round(Convert.ToDouble(wynik))); i++)
            {
                res *= i;
            }
            Wynik = res.ToString();
        }

        internal void Logarytm()
        {
            Wynik = Math.Log10(Convert.ToDouble(wynik)).ToString();
        }

        internal void DoKwadratu()
        {
            Wynik = (Math.Pow(Convert.ToDouble(wynik), 2)).ToString();
        }

        internal void ZaokraglenieDol()
        {
            Wynik = Math.Floor(Convert.ToDouble(wynik)).ToString();
        }

        internal void ZaokraglenieGora()
        {
            Wynik = Math.Ceiling(Convert.ToDouble(wynik)).ToString();
        }
    }
}
