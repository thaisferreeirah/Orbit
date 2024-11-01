using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Models
{
    public class Comentario
    {
        public Comentario(int remetente, string conteudo)
        {
            Remetente = remetente;
            Conteudo = conteudo;
        }

        public int Remetente { get; set; }
        public string Conteudo { get; set; }
    }
}
