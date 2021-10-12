using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvany
{
    class Allapot
    {
        public int allapot { get; private set; }
        private readonly int _betegNapszam; //eddig betegek az emberek
        private readonly int _gyogyuloNapszam; //eddig vannak gyógyuló félben

        public int aktualisBetegNapszam { get; private set; } //hány nap van még hátra a betegségéből
        public int aktualisGyogyuloNapszam { get; private set; }

        public Allapot(string allapot, int betegNap, int gyogyuloNap)
        {
            switch(allapot)
            {
                case "beteg":
                    this.allapot = 2;
                    break;
                case "gyogyulo":
                    this.allapot = 1;
                    break;
                case "egeszseges":
                    this.allapot = 0;
                    break;
            }
            _betegNapszam = betegNap;
            _gyogyuloNapszam = gyogyuloNap;
            aktualisBetegNapszam = betegNap;
            aktualisGyogyuloNapszam = gyogyuloNap;

        }

        public void allapotValtoztat()
        {
            switch(allapot)
            {
                case 2:
                    if (aktualisBetegNapszam == 0)
                        allapot = 1;
                    else
                        aktualisBetegNapszam--;
                    break;
                case 1:
                    if (aktualisGyogyuloNapszam == 0)
                        allapot = 0;
                    else
                        aktualisGyogyuloNapszam--;
                    break;
            }
        }

        public void fertozodott()
        {
            allapot = 2;
            aktualisBetegNapszam = _betegNapszam;
            aktualisGyogyuloNapszam = _gyogyuloNapszam;
        }
        
    }
}
