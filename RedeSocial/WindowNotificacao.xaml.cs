using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RedeSocial.CoisasNotificacao;

namespace RedeSocial
{
    /// <summary>
    /// Lógica interna para WindowNotificacao.xaml
    /// </summary>
    public partial class WindowNotificacao : Window
    {
        UserManager usuarioManager = new UserManager();
        int codUsuario;
        public WindowNotificacao(int codUsuario)
        {
            InitializeComponent();

            this.codUsuario = codUsuario;

            CarregarTodasNotificacoes();
        }

        private void CarregarUmaNotificacao(int i)
        {
            PageCartaoNotificacaoAmizade pageCartaoNotificacaoAmizade = new PageCartaoNotificacaoAmizade(codUsuario, usuarioManager.BuscarRemetenteNotificacao(codUsuario, i));

            Frame frame = new Frame()
            {
                Height = 115,
                Width = 300
            };
            frame.Navigate(pageCartaoNotificacaoAmizade);

            StackPanelNotificacoes.Children.Insert(1, frame);

            usuarioManager.AlterarNotificacaoVerificado(codUsuario, i);
        }

        public void CarregarTodasNotificacoes()
        {
            for (int i = 0; i < usuarioManager.BuscarQuantidadeNotificacao(codUsuario); i++)
            {
                CarregarUmaNotificacao(i);
            }
        }
    }
}
