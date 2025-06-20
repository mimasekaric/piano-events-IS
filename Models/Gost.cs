using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Models
{
    public class Gost : Osoba
    {

        public string TipGost { get; set; }

        public Gost() : base() { }

        public Gost(int mbr, string ime, string prezime, int godine, string uloga, string tipGost)
            : base(mbr, ime, prezime, godine, uloga)
        {
            TipGost = tipGost;
        }

        public override string ToString()
        {
            return $"Gost [Mbr={Mbr}, Ime={Ime}, Prezime={Prezime}, Godine={Godine}, Uloga={Uloga}, TipGost={TipGost}]";
        }
    }
}
