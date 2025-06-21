using PijanistickiDogadjajApp.DAO;
using PijanistickiDogadjajApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Services
{
    public class ComplexQueryService
    {
        private readonly ComplexQueryDAO dao;
        private readonly TransactionDAO transactionDAO;

        public ComplexQueryService(string connectionString)
        {
            this.dao = new ComplexQueryDAO(connectionString);
        }

        public List<SkolaZaradaDTO> GetZarada(int godina)
        {
            return dao.GetZaradaPoSkolama(godina);
        }

        public List<NastupDTO> VratiTakmicarePoTakmicenju(int idTakmicenja)
        {
            
            return dao.GetTakmicariPoTakmicenju(idTakmicenja);
        }
    }
}
