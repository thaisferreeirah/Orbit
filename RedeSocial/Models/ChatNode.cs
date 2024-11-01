using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RedeSocial.Models
{
    public class ChatNode
    {
        private ChatNode Prox;
        private int Remetente;
        private int Destinatario;
        private string Conteudo;
        private string Data;
        private string Horario;

        public ChatNode(int remetente, int destinatario, string conteudo, string data, string horario)
        {
            Remetente = remetente;
            Destinatario = destinatario;
            Conteudo = conteudo;
            Data = data;
            Horario = horario;
            Prox = null;
        }

        public void setProx(ChatNode prox)
        {
            Prox = prox;
        }

        public ChatNode getProx()
        {
            return Prox;
        }

        public int getRemetente()
        {
            return Remetente;
        }

        public int getDestinatario()
        {
            return Destinatario;
        }

        public string getConteudo()
        {
            return Conteudo;
        }

        public string getData()
        {
            return Data;
        }

        public string getHorario()
        {
            return Horario;
        }
    }
}
