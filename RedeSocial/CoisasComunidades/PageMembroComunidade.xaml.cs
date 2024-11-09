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

namespace RedeSocial.CoisasComunidades
{
    /// <summary>
    /// Interação lógica para PageMembroComunidade.xam
    /// </summary>
    public partial class PageMembroComunidade : Page
    {
        int codMembro;
        UserManager userManager = new UserManager();
        public PageMembroComunidade(int _codMembro)
        {
            InitializeComponent();
            codMembro = _codMembro;
            ExibirMembro(_codMembro);
        }

        private void ExibirMembro(int codMembro)
        {
            string nomeMembro = userManager.BuscarNome(codMembro);
            fotoMembro.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(userManager.BuscarFoto(codMembro))),
                Stretch = Stretch.UniformToFill,
            };
            labelNomeMembro.Content = nomeMembro;
        }

        private void fotoMembro_MouseDown(object sender, MouseButtonEventArgs e)
        {
                //if (codMembro == amigo)
                //{
                //    mainFrame.Navigate(new PagePerfil(codUsuario, mainWindow, mainFrame));
                //}
                //else
                //{
                //    mainFrame.Navigate(new PagePerfilOutros(codUsuario, amigo, mainFrame, mainWindow));
                //}
        }
    }
}
