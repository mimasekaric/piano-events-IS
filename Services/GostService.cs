using PijanistickiDogadjajApp.DAO;
using PijanistickiDogadjajApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.Services
{
    public class GostService
    {
      
        
            private readonly GostDAO gostDAO;

            public GostService(GostDAO gostDAO)
            {
                this.gostDAO = gostDAO;
            }

            public List<GostKartaDTO> VratiStatistikuKarti()
            {
                return gostDAO.VratiStatistikuKartiPoGostima();
            }
        
    }
}
