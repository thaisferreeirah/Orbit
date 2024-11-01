using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RedeSocial
{
    public class Depoimento
    {
        public int Remetente { get; set; }
        public int Destinatario { get; set; }
        public string Conteudo { get; set; }
    }

    public class GerenciarDepoimento
    {
        private static List<Depoimento> depoimentos = new List<Depoimento>();
        public void ArmazenarDepoimento(int _remetente, int _destinatario, string _conteudo)
        {
            Depoimento novoDepoimento = new Depoimento()
            {
                Remetente = _remetente,
                Destinatario = _destinatario,
                Conteudo = _conteudo
            };

            depoimentos.Add(novoDepoimento);
        }

        public Boolean VerificarDepoimentoProprio(int i, int remetente)
        {
            if (depoimentos[i].Remetente == remetente)
            {
                return true;
            }
            return false;
        }

        public Boolean VerificarParaSi(int i, int destinatario)
        {
            if (depoimentos[i].Destinatario == i)
            {
                return true;
            }
            return false;
        }

        public int BuscarRemetente(int i)
        {
            return depoimentos[i].Remetente;
        }

        public int BuscarDestinatario(int i)
        {
            return depoimentos[i].Destinatario;
        }

        public string BuscarConteúdo(int i)
        {
            return depoimentos[i].Conteudo;
        }

        public int BuscarQuantidade()
        {
            return depoimentos.Count;
        }

        public List<Depoimento> BuscarDepoimentos()
        {
            return depoimentos;
        }

    }
}
