using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Models
{
    public class Koncert
    {
        private int idDogadjaja { get; set; }
        private string vrsta { get; set; }
        private string status { get; set; }
        private int kartaRbr { get; set; }

        public Koncert() { }

        public Koncert(int idDogadjaja, string vrsta, string status, int kartaRbr)
        {
            this.idDogadjaja = idDogadjaja;
            this.vrsta = vrsta;
            this.status = status;
            this.kartaRbr = kartaRbr;
        }
    }
}
