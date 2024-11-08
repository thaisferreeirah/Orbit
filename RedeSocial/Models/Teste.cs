using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace RedeSocial.Models
{
    public class Teste
    {
        UserManager usuarioManager = new UserManager();
        PostManager postManager = new PostManager();
        NarizManager narizManager = new NarizManager();
        RichTextBox richTextBox;

        public void AdicionarUsuario()
        {
            string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            usuarioManager.AdicionarUsuario("jojo@email.com", "jojo", "123123", "Johnny Bravo", new DateOnly(2000, 12, 13), projectPath + "\\Fotos\\Pukki.jpg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("gojojo@email.com", "gojojo", "123123", "Satoru Gojo", new DateOnly(1996, 12, 07), projectPath + "\\Fotos\\Gojo.jpg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("jeje@email.com", "jeje", "123123", "Jennifer Lawrence", new DateOnly(1990, 08, 15), projectPath + "\\Fotos\\Jennifer.jpeg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("gigi@email.com", "gigi", "123123", "Luigi", new DateOnly(1981, 10, 11), projectPath + "\\Fotos\\Luigi.png", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("jaja@email.com", "jaja", "123123", "Janete Jaeger", new DateOnly(2001, 02, 13), projectPath + "\\Fotos\\Janete.jpeg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("jiji@email.com", "jiji", "123123", "Jimin Jinovo", new DateOnly(2001, 02, 13), projectPath + "\\Fotos\\Jimin.jpeg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("juju@email.com", "juju", "123123", "Juliana Paes", new DateOnly(2001, 02, 13), projectPath + "\\Fotos\\Juliana.jpeg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("dada@email.com", "dada", "123123", "Danilo Damente", new DateOnly(2001, 02, 13), projectPath + "\\Fotos\\Danilo.jpeg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("dede@email.com", "dede", "123123", "Débora Denovo", new DateOnly(2001, 02, 13), projectPath + "\\Fotos\\Debora.jpeg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("didi@email.com", "didi", "123123", "Doutor Renato", new DateOnly(2001, 02, 13), projectPath + "\\Fotos\\Renato.jpeg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("dodo@email.com", "dodo", "123123", "Dora Dominic", new DateOnly(2001, 02, 13), projectPath + "\\Fotos\\Dora.jpg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("dudu@email.com", "dudu", "123123", "Durval Dumau", new DateOnly(2001, 02, 13), projectPath + "\\Fotos\\Durval.jpeg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            //usuarioManager.AdicionarUsuario("lucileny6@gmail.com", "lulu", "123456", "Lucileny Xavier", new DateOnly(2001, 02, 13), projectPath + "\\Fotos\\Janete.jpeg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            //narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("seeun@gmail.com", "sese", "123123", "Yoon Se Eun", new DateOnly(2003, 06, 14), projectPath + "\\Fotos\\Seboke.jpg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();

            narizManager.SetNariz(0, 5000);
        }

        public void AdicionarAmigo()
        {
            usuarioManager.AdicionarAmigo(0, 2);
            usuarioManager.AdicionarAmigo(2, 0);
            usuarioManager.AdicionarAmigo(0, 4);
            usuarioManager.AdicionarAmigo(4, 0);
            usuarioManager.AdicionarAmigo(0, 1);
            usuarioManager.AdicionarAmigo(1, 0);
        }

        public void AdicionarSolicitacaoAmizade()
        {
            usuarioManager.AdicionarSolicitacao(5, 0);
            usuarioManager.AdicionarSolicitacao(6, 0);
        }

        public void AdicionarPost()
        {
            postManager.ArmazenarPost(0, "El Pudinho", FormatarTextoPost("Fiz um pudim muito bom!"), "", "22/09/2024 10:10");
            postManager.ArmazenarPost(1, "", FormatarTextoPost("Alguém sabe onde eu coloquei minha carteira?"), "", "22/09/2024 10:54");
            postManager.ArmazenarPost(0, "", FormatarTextoPost("Olha esse elefante gigante"), "", "22/09/2024 11:13");
            postManager.ArmazenarPost(1, "", FormatarTextoPost("Deixa o Like!"), "", "23/09/2024 15:33");
            postManager.ArmazenarPost(3, "", FormatarTextoPost("Que Mario?"), "", "23/09/2024 19:27");
            postManager.ArmazenarPost(2, "", FormatarTextoPost("Estou com fome"), "", "24/09/2024 01:11");
        }

        public void AdicionarLike()
        {
            postManager.AdicionarLike(0, 2);
            postManager.AdicionarLike(0, 3);
            postManager.AdicionarLike(0, 4);
            postManager.AdicionarLike(1, 4);
            postManager.AdicionarLike(4, 2);
        }

        public void AdicionarComentario()
        {
            postManager.AdicionarComentario(2, 1, "Comentário legal!");
            postManager.AdicionarComentario(2, 2, "Comentário legal 2!");
            postManager.AdicionarComentario(5, 0, "UHUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
        }

        public void AdicionarRecomendar()
        {
            postManager.AdicionarRecomendador(4, 2);
        }

        public string FormatarTextoPost(string texto)
        {
            richTextBox = new RichTextBox()
            {
                FontSize = 16
            };

            richTextBox.AppendText(texto);
            TextRange tempTextRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            string TextoFormatado;
            using (MemoryStream stream = new MemoryStream())
            {
                tempTextRange.Save(stream, DataFormats.Xaml);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    TextoFormatado = reader.ReadToEnd();
                }
            }
            return TextoFormatado;
        }

        public void AdicionarMensagem(ChatList chatList)
        {
            chatList.AdicionarMensagem(0, 1, "Oi", "01/10/2024", "20:22");
            chatList.AdicionarMensagem(1, 0, "Vamos comer?", "02/10/2024", "20:25");
            chatList.AdicionarMensagem(0, 1, "Vamos", "02/10/2024", "20:30");
            chatList.AdicionarMensagem(2, 0, "Elefante", "03/10/2024", "06:45");
            chatList.AdicionarMensagem(0, 1, "Marmota", "04/10/2024", "10:45");
        }

        public void AdicionarTeste(ChatList chatList)
        {
            AdicionarUsuario();
            AdicionarAmigo();
            AdicionarSolicitacaoAmizade();
            AdicionarPost();
            AdicionarLike();
            AdicionarComentario();
            AdicionarRecomendar();
            AdicionarMensagem(chatList);
        }
    }
}
