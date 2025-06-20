using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.DTO
{
    public class TakmicenjeDTO
    {
        public int IdDog { get; set; }
        public DateTime Datum { get; set; }
        public string TipTakmicenja { get; set; }
        public string NazivSkole { get; set; }
    }
}
