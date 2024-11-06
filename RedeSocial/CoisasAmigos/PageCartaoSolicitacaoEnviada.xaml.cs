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
    /// Interação lógica para PageCartaoSolicitacaoEnviada.xam
    /// </summary>
    public partial class PageCartaoSolicitacaoEnviada : Page
    {
        UserManager userManager = new UserManager();
        int codPerfil;
        int codUser;
        Frame mainFrame;
        Home mainWindow;
        public PageCartaoSolicitacaoEnviada(int _codUser, int _codPerfil, Frame _mainFrame, Home _mainWindow)
        {
            InitializeComponent();
            mainFrame = _mainFrame;
            mainWindow = _mainWindow;
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
            userManager.RecusarSolicitacao(codPerfil, codUser);
            botaoCancelarSolicitacaoEnviada.Content = "Solicitação Cancelada";
        }

        private void foto_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PagePerfilOutros pagePerfilOutros = new PagePerfilOutros(codUser, codPerfil, mainFrame, mainWindow);
            mainFrame.Navigate(pagePerfilOutros);
        }
    }
}
