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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RedeSocial.CoisasNotificacao
{
    /// <summary>
    /// Interação lógica para PageCartaoNotificacaoAmizade.xam
    /// </summary>
    public partial class PageCartaoNotificacaoAmizade : Page
    {
        UserManager userManager = new UserManager();

        int codUsuario;
        int codPerfil;
        public PageCartaoNotificacaoAmizade(int codUsuario, int codPerfil)
        {
            InitializeComponent();

            this.codUsuario = codUsuario;
            this.codPerfil = codPerfil;

            BuscarUsuario();
            AtualizarBotoes();
        }

        private void BotaoRecusar_Click_1(object sender, RoutedEventArgs e)
        {
            userManager.RecusarSolicitacao(codUsuario, codPerfil);
            BotaoAceitar.Content = "Recusado";
            BotaoAceitar.IsEnabled = false;
            BotaoAceitar.Background = new SolidColorBrush(Colors.Gray);
            BotaoRecusar.Visibility = Visibility.Collapsed;
        }

        private void BotaoAceitar_Click_1(object sender, RoutedEventArgs e)
        {
            userManager.AceitarSolicitacao(codUsuario, codPerfil);
            BotaoAceitar.Content = "Aceito";
            BotaoAceitar.IsEnabled = false;
            BotaoAceitar.Background = new SolidColorBrush(Colors.Gray);
            BotaoRecusar.Visibility = Visibility.Collapsed;
        }

        private void BuscarUsuario()
        {
            FotoPerfil.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(userManager.BuscarFoto(codPerfil))),
                Stretch = Stretch.UniformToFill,
            };
            LabelNome.Content = userManager.BuscarNome(codPerfil);
        }

        private void AtualizarBotoes()
        {
            if (userManager.VerificarCodAmigo(codUsuario, codPerfil))
            {
                BotaoAceitar.Content = "Aceito";
                BotaoAceitar.IsEnabled = false;
                BotaoAceitar.Background = new SolidColorBrush(Colors.Gray);
                BotaoRecusar.Visibility = Visibility.Collapsed;
            }
            else if (!userManager.VerificarSolicitacao(codPerfil, codUsuario))
            {
                BotaoAceitar.Content = "Recusado";
                BotaoAceitar.IsEnabled = false;
                BotaoAceitar.Background = new SolidColorBrush(Colors.Gray);
                BotaoRecusar.Visibility = Visibility.Collapsed;
            }
        }
    }
}
