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
    /// Interação lógica para PageCartaoChatSelecionado.xam
    /// </summary>
    public partial class PageCartaoChatSelecionado : Page
    {
        UserManager userManager = new UserManager();
        int codPerfil;
        public PageCartaoChatSelecionado(int _codPerfil)
        {
            InitializeComponent();
            codPerfil = _codPerfil;
            buscarUsuario();
        }

        private void buscarUsuario()
        {
            foto.Fill = new ImageBrush(new BitmapImage(new Uri(userManager.BuscarFoto(codPerfil))));
            textNome.Text = userManager.BuscarNome(codPerfil);
        }
    }
}
