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
        private readonly TransactionService transactionService;

        public ComplexQueryUIHandler(ComplexQueryService service, TransactionService transactionService)
        {
            this.service = service;
            this.transactionService = transactionService;
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

        public void PrikaziTakmicare()
        {

            List<TakmicenjeDTO> takmicenja = transactionService.GetSvaTakmicenja();

            if (takmicenja.Count == 0)
            {
                Console.WriteLine("Nema dostupnih takmičenja.");
                return;
            }

            Console.WriteLine("=== Lista takmičenja ===");
            for (int i = 0; i < takmicenja.Count; i++)
            {
                var t = takmicenja[i];
                Console.WriteLine($"{i + 1}. Datum: {t.Datum:dd.MM.yyyy}, Tip: {t.TipTakmicenja}, Škola: {t.NazivSkole}");
            }

            Console.Write("Izaberite takmičenje (unesite broj): ");
            if (!int.TryParse(Console.ReadLine(), out int izbor) || izbor < 1 || izbor > takmicenja.Count)
            {
                Console.WriteLine("Neispravan unos!");
                return;
            }

            // ✔️ Dohvatanje ID-ja iz izbora korisnika
            int idTakmicenja = takmicenja[izbor - 1].IdDog;


            var takmicari = service.VratiTakmicarePoTakmicenju(idTakmicenja);

            Console.WriteLine($"Takmicari za takmicenje ID: {idTakmicenja}\n");
            Console.WriteLine("Ime\tPrezime\tGodRodj\tTip diplome\tBodovi\tTrajanje(min)");

            foreach (var t in takmicari)
            {
                Console.WriteLine($"{t.Ime}\t{t.Prezime}\t{t.GodRodjenja}\t{t.TipDiplome}\t{t.Bodovi}\t{t.UkupnoTrajanjeMin}");
            }
        }
    }
}
