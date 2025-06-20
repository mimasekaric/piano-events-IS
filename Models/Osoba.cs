using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Models
{
    public class Osoba
    {
        public int Mbr { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int Godine { get; set; }
        public string Uloga { get; set; }

        public Osoba() { }

        public Osoba(int mbr, string ime, string prezime, int godine, string uloga)
        {
            Mbr = mbr;
            Ime = ime;
            Prezime = prezime;
            Godine = godine;
            Uloga = uloga;
        }

        public override string ToString()
        {
            return $"Osoba [Mbr={Mbr}, Ime={Ime}, Prezime={Prezime}, Godine={Godine}, Uloga={Uloga}]";
        }
    }
}
