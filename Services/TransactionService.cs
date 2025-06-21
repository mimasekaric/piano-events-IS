using PijanistickiDogadjajApp.DAO;
using PijanistickiDogadjajApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Services
{
    public class TransactionService
    {

        public TransactionService() { }


        private readonly TransactionDAO transactionDAO;

        public TransactionService(string connectionString)
        {
            this.transactionDAO = new TransactionDAO(connectionString);
        }

        public List<TakmicenjeDTO> GetSvaTakmicenja()
        {
            return transactionDAO.GetSvaTakmicenja();
        }

        public bool PokreniTransakciju(long mbr, int idTakmicenja, DateTime izabranoTakmicenjeDatum)
        {
            return transactionDAO.UplataINastup(mbr, idTakmicenja, izabranoTakmicenjeDatum);   
        }
    }
}
