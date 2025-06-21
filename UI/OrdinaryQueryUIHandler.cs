using PijanistickiDogadjajApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PijanistickiDogadjajApp.UI
{
    public class OrdinaryQueryUIHandler
    {

        private readonly OrdinaryQueryService gostService;

        public OrdinaryQueryUIHandler(OrdinaryQueryService gostService)
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
