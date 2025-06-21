using PijanistickiDogadjajApp.DAO;
using PijanistickiDogadjajApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.UI
{
    public class MainUIHandler
    {
       
        private readonly OrdinaryQueryService _ordinaryQueryService;
        private readonly ComplexQueryService _complexQueryService;
        private readonly TransactionService _transactionService;

        public MainUIHandler(string connectionString) 
        {
        
            this._ordinaryQueryService = new OrdinaryQueryService(connectionString);
            this._complexQueryService = new ComplexQueryService(connectionString);
            this._transactionService = new TransactionService(connectionString);
        
        }


        public void handleMenu()
        {
            bool izlaz = false;

            while (!izlaz)
            {
                Console.Clear();
                Console.WriteLine("  █ █ █   █ █ █");
                Console.WriteLine(" | | | | | | | |");
                Console.WriteLine(" | | | | | | | |");
                Console.WriteLine(" |_|_|_|_|_|_|_|");
                Console.WriteLine("Izaberite opciju:");
                Console.WriteLine("1. Prosti upit");
                Console.WriteLine("2. Kompleksni upiti");
                Console.WriteLine("3. Transakcija");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unesite izbor: ");

                string unos = Console.ReadLine();

                switch (unos)
                {
                    case "1":
                        ProstiUpit();
                        break;
                    case "2":
                        KompleksniUpiti();
                        break;
                    case "3":
                        Transakcija();
                        break;
                    case "0":
                        izlaz = true;
                        Console.WriteLine("Izlaz iz programa...");
                        break;
                    default:
                        Console.WriteLine("Nepoznata opcija. Pokušajte ponovo.");
                        break;
                }

                if (!izlaz)
                {
                    Console.WriteLine("\nPritisnite bilo koji taster za povratak u meni...");
                    Console.ReadKey();
                }
            }




        }


        private void ProstiUpit()
        {
            OrdinaryQueryUIHandler ordinaryQueryUIHandler = new OrdinaryQueryUIHandler(_ordinaryQueryService);
            ordinaryQueryUIHandler.PrikaziStatistiku();
        }

        private void KompleksniUpiti()
        {
           ComplexQueryUIHandler complexQueryUIHandler = new ComplexQueryUIHandler(_complexQueryService, _transactionService);
            complexQueryUIHandler.handleComplexUIMenu();
           
        }

        private void Transakcija()
        {
           TransactionUIHandler transactionUIHandler = new TransactionUIHandler(_transactionService);
            transactionUIHandler.PokreniTransakciju();
        }


    }
}
