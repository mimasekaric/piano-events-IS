using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Models
{
    public class Sala
    {
        private int idSala { get; set; }
        private string nazivSale { get; set; }
        private int? kapacitet { get; set; }
        private int muzickaSkolaId { get; set; }

        public Sala() { }

        public Sala(int idSala, string nazivSale, int? kapacitet, int muzickaSkolaId)
        {
            this.idSala = idSala;
            this.nazivSale = nazivSale;
            this.kapacitet = kapacitet;
            this.muzickaSkolaId = muzickaSkolaId;
        }
    }
}
