using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ispit.Model
{
    public class Ucenik
    {
        #region Svojstva
        protected string _ime;
        protected string _prezime;
        protected DateTime _datumRodjenja;
        protected double _prosjek;
        #endregion

        #region Getteri i Setteri
        public string Ime
        {
            get { return _ime; }
            set
            {
                if (value == null || value == "")
                {
                    throw new ArgumentException("Ime ne može biti prazno!");
                }
                _ime = value;
            }
        }

        public string Prezime
        {
            get { return _prezime; }
            set
            {
                if (value == null || value == "")
                {
                    throw new ArgumentException("Prezime ne može biti prazno!");
                }
                _prezime = value;
            }
        }

        public DateTime DatumRodjenja
        {
            get { return _datumRodjenja; }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("Datum rodjenja mora biti u proslosti!");
                }
                _datumRodjenja = value;
            }
        }

        public double Prosjek
        {
            get { return _prosjek; }
            set
            {
                if (!(value <= 5.00 && value >= 1.00))
                {
                    throw new ArgumentException("Prosjek mora biti od 1.0 do 5.0!");
                }
                _prosjek = value;
            }
        }
        #endregion

        #region Konstruktori
        public Ucenik(string ime, string prezime, DateTime datumRodjenja, double prosjek)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            Prosjek = prosjek;
        }
        #endregion

        #region Metode
        public int Starost()
        {
            int godine = DateTime.Now.Year - DatumRodjenja.Year;
            if (DateTime.Now.Month <= DatumRodjenja.Month && DateTime.Now.Day < DatumRodjenja.Day)
            {
                godine--;
            }
            return godine;
        }

        public string IspisProsjeka(bool print = true)
        {
            string rezultat = "Prosjek izvan dosega!";
            if (Prosjek >= 1.0 && Prosjek < 1.5)
            {
                rezultat = "medovoljan";
            }
            else if (Prosjek >= 1.5 && Prosjek < 2.5)
            {
                rezultat = "dovoljan";
            }
            else if (Prosjek >= 2.5 && Prosjek < 3.5)
            {
                rezultat = "dobar";
            }
            else if (Prosjek >= 3.5 && Prosjek < 4.5)
            {
                rezultat = "vrlo dobar";
            }
            else if (Prosjek >= 4.5 && Prosjek < 5.0)
            {
                rezultat = "odličan";
            }
            if (print)
            {
                Console.WriteLine(rezultat);
            }
            return rezultat;
        }

        override public string ToString()
        {
            return $"[{Ime} {Prezime}, {DatumRodjenja} ({this.Starost()} godina), prosjek: {Prosjek} ({this.IspisProsjeka(false)})]";
        }

        public static bool ProvjeraUnosa(string ime, string prezime, string datumRodjenja, string prosjek)
        {
            bool prihvatljivo = true;
            DateTime parsiranDatumRodjenja;
            double parsiranProsjek;
            if (ime == null || ime == "")
            {
                Console.WriteLine("Ime ne može biti prazno!");
                prihvatljivo = false;
            }
            if (prezime == null || prezime == "")
            {
                Console.WriteLine("Prezime ne može biti prazno!");
                prihvatljivo = false;
            }
            if (DateTime.TryParse(datumRodjenja, out parsiranDatumRodjenja))
            {
                if (parsiranDatumRodjenja > DateTime.Now)
                {
                    Console.WriteLine("Datum rodjenja mora biti u proslosti!");
                    prihvatljivo = false;
                }
            }
            else
            {
                Console.WriteLine("Datum rođenja se ne može parsirati!");
                prihvatljivo = false;
            }
            if (Double.TryParse(prosjek, NumberStyles.Any, CultureInfo.InvariantCulture, out parsiranProsjek))
            {
                if (!(parsiranProsjek <= 5.00 && parsiranProsjek >= 1.00))
                {
                    Console.WriteLine("Prosjek mora biti od 1.0 do 5.0!");
                    prihvatljivo = false;
                }
            }
            else
            {
                Console.WriteLine("Prosjek se ne može parsirati!");
                prihvatljivo = false;
            }
            return prihvatljivo;
        }
        #endregion
    }
}
