using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RedeSocial.Models
{
    public class ChatList
    {
        private ChatNode inicio, fim;

        public ChatList()
        {
            inicio = fim = null;
        }

        public void AdicionarMensagem(int remetente, int destinatario, string conteudo, string data, string horario)
        {
            if (inicio != null)
            {
                fim.setProx(new ChatNode(remetente, destinatario, conteudo, data, horario));
                fim = fim.getProx();
            }
            else
            {
                inicio = fim = new ChatNode(remetente, destinatario, conteudo, data, horario);
            }
        }

        public bool VerificarMensagemRemetente(int i, int remetente, int destinatario)
        {
            ChatNode aux = inicio;
            int contador = 0;

            while (aux != null)
            {
                if (contador == i)
                {
                    if (aux.getRemetente() == remetente && aux.getDestinatario() == destinatario)
                    {
                        return true;
                    }
                }
                aux = aux.getProx();
                contador++;
            }
            return false;
        }

        public bool VerificarMensagemDestinatario(int i, int remetente, int destinatario)
        {
            ChatNode aux = inicio;
            int contador = 0;

            while (aux != null)
            {
                if (contador == i)
                {
                    if (aux.getRemetente() == destinatario && aux.getDestinatario() == remetente)
                    {
                        return true;
                    }
                }
                aux = aux.getProx();
                contador++;
            }
            return false;
        }

        public string BuscarConteudo(int i)
        {
            ChatNode aux = inicio;
            int contador = 0;

            while (aux != null)
            {
                if (contador == i)
                {
                    return aux.getConteudo();
                }
                aux = aux.getProx();
                contador++;
            }
            return null;
        }

        public string BuscarData(int i)
        {
            ChatNode aux = inicio;
            int contador = 0;

            while (aux != null)
            {
                if (contador == i)
                {
                    return aux.getData();
                }
                aux = aux.getProx();
                contador++;
            }
            return null;
        }

        public string BuscarHorario(int i)
        {
            ChatNode aux = inicio;
            int contador = 0;

            while (aux != null)
            {
                if (contador == i)
                {
                    return aux.getHorario();
                }
                aux = aux.getProx();
                contador++;
            }
            return null;
        }

        public int BuscarQuantidade()
        {
            ChatNode aux = inicio;
            int contador = 0;

            while (aux != null)
            {
                aux = aux.getProx();
                contador++;
            }
            return contador;
        }
    }
}
