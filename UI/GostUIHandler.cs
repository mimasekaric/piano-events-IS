using PijanistickiDogadjajApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.UI
{
    public class GostUIHandler
    {

        private readonly GostService gostService;

        public GostUIHandler(GostService gostService)
        {
            this.gostService = gostService;
        }

        public void PrikaziStatistiku()
        {
            var statistike = gostService.VratiStatistikuKarti();

            Console.WriteLine("Statistika kupljenih karata po gostima:");
            foreach (var stat in statistike)
            {
                Console.WriteLine(stat);
            }
        }
    
    }
}
