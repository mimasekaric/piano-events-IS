using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Models
{
    public class MuzickaSkola
    {
        private int id { get; set; }
        private string naziv { get; set; }
        private string adresa { get; set; }

        public MuzickaSkola() { }

        public MuzickaSkola(int id, string naziv, string adresa)
        {
            this.id = id;
            this.naziv = naziv;
            this.adresa = adresa;
        }

    }
}
