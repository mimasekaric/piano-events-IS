using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.DTO
{
    public class GostKartaDTO
    {
        public int Mbr { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int BrojKarata { get; set; }
        public double ProsjecnaCijena { get; set; }

        public override string ToString()
        {
            return $"{Ime} {Prezime} (MBR: {Mbr}) - Broj karata: {BrojKarata}, Prosječna cijena: {ProsjecnaCijena:F2}";
        }
    }
}
