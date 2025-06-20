using PijanistickiDogadjajApp.DAO;
using PijanistickiDogadjajApp.DTO;
using PijanistickiDogadjajApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.UI
{
    public class ComplexQueryUIHandler
    {
        private readonly ComplexQueryService service;

        public ComplexQueryUIHandler(ComplexQueryService service)
        {
            this.service = service;
        }

        public void PrikaziZaradu()
        {
            Console.Write("Unesite godinu: ");
            int godina = int.Parse(Console.ReadLine());

            var rezultati = service.GetZarada(godina);

            Console.WriteLine($"{"Muzicka Skola",-25} {"Ukupna Zarada",15} {"Zarada Humanitarnih",20} {"Broj Koncerata",15}");
            Console.WriteLine(new string('-', 80));

            foreach (var r in rezultati)
            {
                Console.WriteLine($"{r.NazivSkole,-25} {r.UkupnaZarada,15:C} {r.ZaradaHumanitarnih,20:C} {r.UkupanBrojKoncerata,15}");
            }
        }
    }
}
