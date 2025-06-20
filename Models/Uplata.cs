using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Models
{
    public class Uplata
    {
        public int IdUplt { get; set; }
        public float? IznosUplt { get; set; }
        public string StatUplt { get; set; }
        public DateTime? DatUplt { get; set; }
        public int PijanistaMbr { get; set; }

        public Uplata() { }

        public Uplata(int idUplt, float? iznosUplt, string statUplt, DateTime? datUplt, int pijanistaMbr)
        {
            IdUplt = idUplt;
            IznosUplt = iznosUplt;
            StatUplt = statUplt;
            DatUplt = datUplt;
            PijanistaMbr = pijanistaMbr;
        }
    }
}
