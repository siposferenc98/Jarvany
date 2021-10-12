using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvany
{
    class Ember : Allapot
    {
        public string nev;
        private List<string> utvonal;
        private Queue<string> utvonalQue = new Queue<string>();
        public int _eltoltottNap;
        public bool valtozott;

        public Ember(string nev, string allapot, List<string> utvonal, int betegNap, int gyogyuloNap):base(allapot,betegNap,gyogyuloNap)
        {
            this.nev = nev;
            this.utvonal = utvonal;
            valtozott = false;

            generateNap();
            utvonal.ForEach(x => utvonalQue.Enqueue(x));
        }

        public string getVaros()
        {
            checkQue();
            return utvonalQue.Peek();       
        }
        private void checkQue()
        {
            if(_eltoltottNap == 0 && utvonalQue.Any())
            {
                utvonalQue.Dequeue();
                generateNap();
                if(!utvonalQue.Any())
                    utvonal.ForEach(x => utvonalQue.Enqueue(x));
            }
        }

        private void generateNap()
        {
            Random rnd = new Random();
            _eltoltottNap = rnd.Next(1, 5);
        }

        public void elteltNap()
        {
            _eltoltottNap--;
        }
        public override string ToString()
        {
            switch (this.allapot)
            {
                case 2:
                    return "beteg";
                case 1:
                    return "gyogyulo";
                default:
                    return "egeszsges";
            }
        }


    }

}
