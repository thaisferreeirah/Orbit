using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Models
{
    internal class Nose
    {
        public double Nariz { get; set; }
        public double NarizPorClique { get; set; }
        public double NarizPorSegundo { get; set; }
        public string Acessorio { get; set; }
        public int Upgrade1 { get; set; }
        public int Upgrade2 { get; set; }
        public int Upgrade3 { get; set; }
        public int Upgrade4 { get; set; }
        public int Upgrade5 { get; set; }
        public int Upgrade6 { get; set; }
    }

    public class NarizManager
    {
        private static List<Nose> narizes = new List<Nose>();

        public void AdicionarUsuario()
        {
            Nose nariz = new Nose()
            {
                Nariz = 0,
                NarizPorClique = 1,
                NarizPorSegundo = 0,
                Acessorio = "",
                Upgrade1 = 0,
                Upgrade2 = 0,
                Upgrade3 = 0,
                Upgrade4 = 0,
                Upgrade5 = 0,
                Upgrade6 = 0
            };

            narizes.Add(nariz);
        }

        public void SetNariz(int userCod, double nariz)
        {
            narizes[userCod].Nariz = nariz;
        }

        public double GetNariz(int userCod)
        {
            return narizes[userCod].Nariz;
        }

        public void SetNarizPorClique(int userCod, double narizPorClique)
        {
            narizes[userCod].NarizPorClique = narizPorClique;
        }

        public double GetNarizPorClique(int userCod)
        {
            return narizes[userCod].NarizPorClique;
        }

        public void SetNarizPorSegundo(int userCod, double narizPorSegundo)
        {
            narizes[userCod].NarizPorSegundo = narizPorSegundo;
        }

        public double GetNarizPorSegundo(int userCod)
        {
            return narizes[userCod].NarizPorSegundo;
        }

        public void SetAcessorio(int userCod, string acessorio)
        {
            narizes[userCod].Acessorio = acessorio;
        }

        public string GetAcessorio(int userCod)
        {
            return narizes[userCod].Acessorio;
        }

        public void Upgrade1LvUp(int userCod)
        {
            narizes[userCod].Upgrade1++;
        }

        public int GetUpgrade1(int userCod)
        {
            return narizes[userCod].Upgrade1;
        }

        public void Upgrade2LvUp(int userCod)
        {
            narizes[userCod].Upgrade2++;
        }

        public int GetUpgrade2(int userCod)
        {
            return narizes[userCod].Upgrade2;
        }

        public void Upgrade3LvUp(int userCod)
        {
            narizes[userCod].Upgrade3++;
        }

        public int GetUpgrade3(int userCod)
        {
            return narizes[userCod].Upgrade3;
        }

        public void Upgrade4LvUp(int userCod)
        {
            narizes[userCod].Upgrade4++;
        }

        public int GetUpgrade4(int userCod)
        {
            return narizes[userCod].Upgrade4;
        }

        public void Upgrade5LvUp(int userCod)
        {
            narizes[userCod].Upgrade5++;
        }

        public int GetUpgrade5(int userCod)
        {
            return narizes[userCod].Upgrade5;
        }

        public void Upgrade6LvUp(int userCod)
        {
            narizes[userCod].Upgrade6++;
        }

        public int GetUpgrade6(int userCod)
        {
            return narizes[userCod].Upgrade6;
        }
    }
}
