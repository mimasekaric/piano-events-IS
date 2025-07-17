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
    public class TransactionUIHandler
    {
        private readonly TransactionService transactionService;
   
        public TransactionUIHandler(TransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public void PokreniTransakciju()
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
            int izabranoTakmicenjeId = takmicenja[izbor - 1].IdDog;
            DateTime izabranoTakmicenjeDatum = takmicenja[izbor - 1].Datum;

            Console.WriteLine($"Izabrali ste takmičenje ID: {izabranoTakmicenjeId}");

            // Nastavak logike: unos JMBG, kreiranje uplate, nastupa, transakcija itd.

            Console.Write("Unesite matični broj pijaniste: ");
            long mbr = long.Parse(Console.ReadLine());

           
            bool uspjesno = transactionService.PokreniTransakciju(mbr, izabranoTakmicenjeId, izabranoTakmicenjeDatum);

            if (uspjesno)
                Console.WriteLine("Transakcija uspešno izvršena (uplata + nastup).");
            else
                Console.WriteLine("Greška u transakciji. Sve promjene su poništene.");
        }
    }
}
