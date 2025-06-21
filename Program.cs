using Npgsql;
using PijanistickiDogadjajApp.DAO;
using PijanistickiDogadjajApp.DTO;
using PijanistickiDogadjajApp.Services;
using PijanistickiDogadjajApp.UI;

namespace PijanistickiDogadjajApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string connectionString = "Host=localhost;Port=5432;Database=pijanisticki;Username=postgres;Password=super";
            //var gostDAO = new GostDAO(connectionString);
            //var gostService = new GostService(gostDAO);
            //var gostUI = new GostUIHandler(gostService);
            //gostUI.PrikaziStatistiku();


            //var dao = new MuzickaSkolaDAO(connectionString);
            //var service = new ComplexQueryService(dao);
            //var ui = new ComplexQueryUIHandler(service);

            /*ui.PrikaziZaradu()*/;
            var transactiondDao = new TransactionDAO(connectionString);
            var transactionService = new TransactionService(transactiondDao);
            //var transactionUI = new TransactionUIHandler(transactionService);
            //transactionUI.PokreniTransakciju();


            var complexQueryDao = new ComplexQueryDAO(connectionString);
            var complexQueryService = new ComplexQueryService(complexQueryDao);
            var complexQueryUI = new ComplexQueryUIHandler(complexQueryService, transactionService);
            complexQueryUI.PrikaziTakmicare();
        }
    }
}
