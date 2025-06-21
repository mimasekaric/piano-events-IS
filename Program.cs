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
            MainUIHandler mainUIHandler = new MainUIHandler(connectionString);
            mainUIHandler.handleMenu();
        }
    }
}
