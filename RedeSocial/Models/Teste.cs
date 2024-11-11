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

        string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

        public void AdicionarUsuario()
        {
            usuarioManager.AdicionarUsuario("jojo@email.com", "jojo", "123123", "Johnny Bravo", new DateOnly(2000, 12, 13), projectPath + "\\Fotos\\Pukki.jpg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("gojojo@email.com", "gojojo", "123123", "Satoru Gojo", new DateOnly(1996, 12, 07), projectPath + "\\Fotos\\Gojo.jpg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("jeje@email.com", "jeje", "123123", "Jennifer Lawrence", new DateOnly(1990, 08, 15), projectPath + "\\Fotos\\Jennifer.jpeg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("gigi@email.com", "gigi", "123123", "Luigi", new DateOnly(1981, 10, 11), projectPath + "\\Fotos\\Luigi.png", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
            narizManager.AdicionarUsuario();
            usuarioManager.AdicionarUsuario("ddddohn@gmail.com", "jaja", "123123", "Janete Jaeger", new DateOnly(2001, 02, 13), projectPath + "\\Fotos\\Janete.jpeg", projectPath + "\\Fundo\\fundoPadraoPreto.jpg");
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
            narizManager.SetNariz(4, 2000);
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
            postManager.ArmazenarPost(0, "", FormatarTextoPost("Qual é mais pesado, 1kg de algodão ou 1kg de chumbo?"), "", "22/09/2024 11:13");
            postManager.ArmazenarPost(1, "", FormatarTextoPost("Deixa o Like!"), "", "23/09/2024 15:33");
            postManager.ArmazenarPost(3, "", FormatarTextoPost("Que Mario?"), "", "23/09/2024 19:27");
            postManager.ArmazenarPost(2, "", FormatarTextoPost("Estou com fome"), "", "24/09/2024 01:11");
            postManager.ArmazenarPost(5, "", FormatarTextoPost("Falem bem ou falem mal, falem Jimin"), projectPath + "\\Fotos\\Jimin.jpeg", "25/09/2024 11:11");
        }

        public void AdicionarLike()
        {
            postManager.AdicionarLike(0, 2);
            postManager.AdicionarLike(0, 3);
            postManager.AdicionarLike(0, 4);
            postManager.AdicionarLike(1, 4);
            postManager.AdicionarLike(4, 2);
            postManager.AdicionarLike(6, 1);
            postManager.AdicionarLike(6, 2);
            postManager.AdicionarLike(6, 3);
            postManager.AdicionarLike(6, 5);
            postManager.AdicionarLike(6, 6);
            postManager.AdicionarLike(6, 7);
            postManager.AdicionarLike(6, 8);
            postManager.AdicionarLike(6, 9);
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

            //Pizza order
            chatList.AdicionarMensagem(4, 0, "olá, boa noite", "02/11/2024", "19:42");
            chatList.AdicionarMensagem(4, 0, "gostaria de pedir uma pizza meia calabresa e meia quatro queijo", "02/11/2024", "19:42");
            chatList.AdicionarMensagem(0, 4, "boa noite", "02/11/2024", "19:43");
            chatList.AdicionarMensagem(0, 4, "fica 65 reais", "02/11/2024", "19:43");
            chatList.AdicionarMensagem(0, 4, "qual o endereço?", "02/11/2024", "19:43");
            chatList.AdicionarMensagem(0, 4, "e qual a forma de pagamento? Aceitamos em dinheiro vivo, morto, crédito, débito e pix", "02/11/2024", "19:44");
            chatList.AdicionarMensagem(4, 0, "rua dos vida loka, 7210", "02/11/2024", "19:45");
            chatList.AdicionarMensagem(4, 0, "vou pagar no pixel", "02/11/2024", "19:45");
            chatList.AdicionarMensagem(4, 0, "quanto tempo demora?", "02/11/2024", "19:45");
            chatList.AdicionarMensagem(0, 4, "leva cerca de 30 min", "02/11/2024", "19:46");
            chatList.AdicionarMensagem(0, 4, "a chave pix é essa: 89fu32t0nu908e8e23uwu98f", "02/11/2024", "19:46");
            chatList.AdicionarMensagem(0, 4, "vou precisar do comprovante do pagamento, por favor", "02/11/2024", "19:46");
            chatList.AdicionarMensagem(4, 0, "não tem como enviar foto nesse chat", "02/11/2024", "19:49");
            chatList.AdicionarMensagem(0, 4, "q coisa, vou contatar os desenvolvedores para que melhorarem isso", "02/11/2024", "19:50");
            chatList.AdicionarMensagem(0, 4, "então poderia me informar o nome de pixelador?", "02/11/2024", "19:50");
            chatList.AdicionarMensagem(4, 0, "Godofredo Cintilante", "02/11/2024", "19:51");
            chatList.AdicionarMensagem(0, 4, "pagamento confirmado. A pizza chega em até 30 min", "02/11/2024", "19:51");
            chatList.AdicionarMensagem(0, 4, "obrigado pela preferência", "02/11/2024", "19:52");
        }

        public void AdicionarComunidade(ComunidadeManager comunidadeManager)
        {
            comunidadeManager.AdicionarComunidade(0, "Amo K-pop", projectPath + "\\Fotos\\Jimin.jpeg", "Aserehe ra de re, aqui tem do bom e do melhor dos Australianos. Bonapetí", 0);
            comunidadeManager.AdicionarComunidade(1, "É bolacha!", projectPath + "\\Imagens\\Bolacha.jpeg", "É BOLACHA E NÃO BISCOITO!!!!!!!!!!!!!!!!", 0);
            comunidadeManager.AdicionarComunidade(2, "Como ficar rico com 100 reais", projectPath + "\\Imagens\\Dinheiro.jpeg", "Quer saber como ganhar dinheiro fácil e rápido? Assine esse curso >>> AQUI!!!", 0);
            comunidadeManager.AdicionarComunidade(3, "Desenhos Atuais", projectPath + "\\Imagens\\JohnnyBravo.jpeg", "Vamos falar apenas sobre os desenhos mais recentes, nada de coisa antiga, plz.", 0);
            comunidadeManager.AdicionarComunidade(4, "Você já viu essa mulher?", projectPath + "\\Imagens\\Chuuringa.jpg", "Já sonhou com esta mulher? TODAS AS NOITES EM TODO O MUNDO CENTENAS DE PESSOAS SONHAM COM ESSA CARA.", 12);
        }

        public void AdicionarTeste(ChatList chatList, ComunidadeManager comunidadeManager)
        {
                AdicionarUsuario();
                AdicionarAmigo();
                AdicionarSolicitacaoAmizade();
                AdicionarPost();
                AdicionarLike();
                AdicionarComentario();
                AdicionarRecomendar();
                AdicionarMensagem(chatList);
                AdicionarComunidade(comunidadeManager);
        }
    }
}
