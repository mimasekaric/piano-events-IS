using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Models
{
    public class Nastup
    {
        public int IdNast { get; set; }
        public DateTime? DatNast { get; set; }
        public string KategNast { get; set; }
        public int DiplomaIdDipl { get; set; }
        public int TakmicenjeIdDog { get; set; }

        public Nastup() { }
        public Nastup(int idNast, DateTime? datNast, string kategNast, int diplomaIdDipl, int takmicenjeIdDog)
        {
            IdNast = idNast;
            DatNast = datNast;
            KategNast = kategNast;
            DiplomaIdDipl = diplomaIdDipl;
            TakmicenjeIdDog = takmicenjeIdDog;
        }
    
    }
}
