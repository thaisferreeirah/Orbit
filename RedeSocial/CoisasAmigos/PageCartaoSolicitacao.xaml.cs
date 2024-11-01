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

namespace RedeSocial
{
    /// <summary>
    /// Interação lógica para PageCartaoSolicitacao.xam
    /// </summary>
    public partial class PageCartaoSolicitacao : Page
    {
        UserManager userManager = new UserManager();
        int codPerfil;
        int codUser;
        Frame mainFrame;
        Home mainWindow;


        public PageCartaoSolicitacao(int _codUser, int _codPerfil, Frame _mainFrame)
        {
            InitializeComponent();
            mainFrame = _mainFrame; 
            codPerfil = _codPerfil;
            codUser = _codUser;
            buscarUsuario(codPerfil);

        }
        private void buscarUsuario(int codPerfil)
        {
            foto.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(userManager.BuscarFoto(codPerfil))),
                Stretch = Stretch.UniformToFill,
            };
            labelNome.Content = userManager.BuscarNome(codPerfil);
        }

        private void botaoAceitar_Click(object sender, RoutedEventArgs e)
        {
            userManager.AceitarSolicitacao(codUser, codPerfil);
            botaoAceitar.Visibility = Visibility.Hidden;
            botaoRecusar.Content = "Solicitação aceita";
            botaoRecusar.IsEnabled = false;

            //na segunda vez que clicar no botao amigos esse cartao tem que estar como amigo 
            
        }

        private void botaoRecusar_Click(object sender, RoutedEventArgs e)
        {
            userManager.RecusarSolicitacao(codUser, codPerfil);
            botaoAceitar.Visibility = Visibility.Hidden;
            botaoRecusar.Content = "Solicitação recusada";
            botaoRecusar.IsEnabled = false;
            //se recusar o cartao precisa sumir do frame amigos

        }

        private void foto_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PagePerfilOutros pagePerfilOutros = new PagePerfilOutros(codUser, codPerfil, mainFrame, mainWindow);
            mainFrame.Navigate(pagePerfilOutros);
        }
    }

    //<Ellipse x:Name="foto"   Grid.Row="0" VerticalAlignment="Center" HorizontalAlignment="Center" Height="120" Width="120" Stretch="Fill" Margin="10,10,10,5" Stroke="#37376E" MouseLeftButtonUp="foto_MouseLeftButtonUp"/>
}
