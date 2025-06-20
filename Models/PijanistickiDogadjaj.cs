using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Models
{
    public class PijanistickiDogadjaj
    {
        private int id { get; set; }
        private string tip { get; set; }
        private DateTime? datumPocetka { get; set; }

        public PijanistickiDogadjaj() { }

        public PijanistickiDogadjaj(int id, string tip, DateTime? datumPocetka)
        {
            this.id = id;
            this.tip = tip;
            this.datumPocetka = datumPocetka;
        }
    }
}
