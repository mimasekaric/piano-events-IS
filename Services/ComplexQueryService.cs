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
        private readonly MuzickaSkolaDAO dao;

        public ComplexQueryService(MuzickaSkolaDAO dao)
        {
            this.dao = dao;
        }

        public List<SkolaZaradaDTO> GetZarada(int godina)
        {
            return dao.GetZaradaPoSkolama(godina);
        }
    }
}
