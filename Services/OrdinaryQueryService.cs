using PijanistickiDogadjajApp.DAO;
using PijanistickiDogadjajApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Services
{
    public class OrdinaryQueryService
    {
      
        
            private readonly OrdinaryQueryDAO gostDAO;

            public OrdinaryQueryService(string connectionString)
            {
                this.gostDAO = new OrdinaryQueryDAO(connectionString);
            }

            public List<GostKartaDTO> VratiStatistikuKarti()
            {
                return gostDAO.VratiStatistikuKartiPoGostima();
            }
        
    }
}
