using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvany
{
    class Jarvany
    {
        private List<Ember> emberek = new List<Ember>();
        private int _napok;

        public void szimulacio(string fn, int betegNap, int gyogyuloNap)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            Beolvas(fn,betegNap,gyogyuloNap);
            for(int i = 0; i < _napok+20; i++)
            {
                kiir(i);
                Kivalogat(i);
                switch(vege())
                {
                    case 2:
                        vegeKiir(i, true);
                        return;
                    case 1:
                        vegeKiir(i, false);
                        return;
                }
            }
            List<Ember> fertozottek = emberek.Where(x => x.allapot == 1 || x.allapot == 2).ToList().OrderBy(x=>x.nev).ToList();
            Console.WriteLine($"Fertőzöttek száma: {fertozottek.Count()}");
            fertozottek.ForEach(x=>Console.WriteLine($"Fertőzött: {x.nev}"));
            stopwatch.Stop();
            Console.WriteLine("Miliseconds = " + stopwatch.ElapsedMilliseconds);

        }
        private void Beolvas(string fn,int betegNap, int gyogyuloNap)
        {
            StreamReader sr = new StreamReader("./"+fn);
            List<int> adatok = sr.ReadLine().Split(" ").Select(Int32.Parse).ToList();
            _napok = adatok[1];
            for(int i = 0; i < adatok[0]; i++)
            {
                List<string> emberAdatok = sr.ReadLine().Split(" ").ToList();
                string nev = emberAdatok[0];
                string allapot = emberAdatok[1];
                List<string> utvonal = emberAdatok[2].Split("-").ToList();
                Ember ember = new Ember(nev, allapot, utvonal, betegNap, gyogyuloNap);
                this.emberek.Add(ember);
            }
        }

        private void Kivalogat(int nap)
        {
            foreach (Ember ember in emberek)
            {
                List<Ember> egyvarosban = new List<Ember>();
                string varos = "";
                if (ember.valtozott == false)
                {
                    varos = ember.getVaros();
                    egyvarosban.AddRange(emberek.Where(x => x.getVaros() == varos));
                    if (egyvarosban.Any(x => x.allapot == 2))
                        egyvarosban.Where(x => x.allapot == 0).ToList().ForEach(x => { x.fertozodott(); x.valtozott = true; Console.WriteLine("Fertőzött: "+x.nev); });

                }
                foreach (Ember emberValtoztat in egyvarosban.Where(x => x.valtozott == false))
                {
                    emberValtoztat.allapotValtoztat();
                    emberValtoztat.valtozott = true;
                }
            }
            //kövinap
            foreach(Ember ember in emberek)
            {
                ember.valtozott = false;
                ember.elteltNap();
            }

        }

        private void kiir(int nap)
        {
            Console.WriteLine("------------------");
            Console.WriteLine(nap + " nap");
            Console.WriteLine("------------------");
            foreach (Ember a in emberek)
            {
                Console.WriteLine($"{a.nev} --- {a.ToString()} --- {a.getVaros()} --- B:{a.aktualisBetegNapszam} --- GY:{a.aktualisGyogyuloNapszam} ---- RandomNap:{a._eltoltottNap}");
            }
        }

        private int vege()
        {
            if (emberek.All(x => x.allapot == 2))
                return 2;
            else if (emberek.All(x => x.allapot == 0))
                return 1;
            else
                return 0;
        }

        private void vegeKiir(int nap, bool fertozott)
        {
            Console.WriteLine($"{nap} nap után mindenki {(fertozott ? "fertőzött lett." : "meggyógyult.")}");
        }

    }

}
