using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Models
{
    public class Notificacao
    {
        public int Remetente { get; set; }
        public string Tipo { get; set; }
        public int Codigo { get; set; }
        public bool Verificado { get; set; }

        public Notificacao(int remetente, string tipo, int codigo)
        {
            Remetente = remetente;
            Tipo = tipo;
            Codigo = codigo;
            Verificado = false;
        }
    }
}
