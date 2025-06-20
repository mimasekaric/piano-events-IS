using Npgsql;
using PijanistickiDogadjajApp.DAO;
using PijanistickiDogadjajApp.Services;
using PijanistickiDogadjajApp.UI;

namespace PijanistickiDogadjajApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string connectionString = "Host=localhost;Port=5432;Database=pijanisticki;Username=postgres;Password=super";
            var gostDAO = new GostDAO(connectionString);
            var gostService = new GostService(gostDAO);
            var gostUI = new GostUIHandler(gostService);

            gostUI.PrikaziStatistiku();
            Console.WriteLine("Hello, World!");
        }
    }
}
