using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.DTO
{
    public class SkolaZaradaDTO
    {
        public string NazivSkole { get; set; }
        public decimal UkupnaZarada { get; set; }
        public decimal ZaradaHumanitarnih { get; set; }
        public int UkupanBrojKoncerata { get; set; }
    }
}
