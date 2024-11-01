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

namespace RedeSocial.CoisasChat
{
    /// <summary>
    /// Interação lógica para PageCartaoChat.xam
    /// </summary>
    public partial class PageCartaoChat : Page
    {
        UserManager userManager = new UserManager();
        PageChat pageChat;
        int codPerfil;
        public PageCartaoChat(int _codPerfil, PageChat _pageChat)
        {
            InitializeComponent();
            pageChat = _pageChat;
            codPerfil = _codPerfil;
            buscarUsuario();
        }

        private void buscarUsuario()
        {
            foto.Fill = new ImageBrush(new BitmapImage(new Uri(userManager.BuscarFoto(codPerfil))));
            textNome.Text = userManager.BuscarNome(codPerfil);
        }

        private void gridCartao_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pageChat.enviarCodPerfil(codPerfil, gridCartao, textNome);
            pageChat.campoMensagem.IsEnabled = true;
            pageChat.campoMensagem.Clear();
            pageChat.campoMensagem.Focus();
            gridCartao.Background = new SolidColorBrush(Color.FromRgb(75, 75, 130));
            textNome.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }
    }
}
