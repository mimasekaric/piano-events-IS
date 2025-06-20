using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Models
{
    public class Karta
    {
        public int RbrKrt { get; set; }
        public DateTime? DatumKupovine { get; set; }
        public double CijenaKarte { get; set; }
        public string KategorijaKarte { get; set; }
        public int? GostMbr { get; set; }

        // Prazan konstruktor
        public Karta() { }

        // Konstruktor sa svim poljima
        public Karta(int rbrKrt, DateTime? datumKupovine, double cijenaKarte, string kategorijaKarte, int? gostMbr)
        {
            RbrKrt = rbrKrt;
            DatumKupovine = datumKupovine;
            CijenaKarte = cijenaKarte;
            KategorijaKarte = kategorijaKarte;
            GostMbr = gostMbr;
        }

        public override string ToString()
        {
            return $"Karta [RbrKrt={RbrKrt}, DatumKupovine={DatumKupovine}, CijenaKarte={CijenaKarte}, KategorijaKarte={KategorijaKarte}, GostMbr={GostMbr}]";
        }
    }
}
