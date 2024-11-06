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
        int codUsuario;
        UserManager userManager = new UserManager();
        public PageMembroComunidade(int _codUsuario)
        {
            InitializeComponent();
            codUsuario = _codUsuario;
            ExibirMembro(_codUsuario);
        }

        private void ExibirMembro(int codUsuario)
        {
            string nomeMembro = userManager.BuscarNome(codUsuario);
            fotoMembro.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(userManager.BuscarFoto(codUsuario))),
                Stretch = Stretch.UniformToFill,
            };
            labelNomeMembro.Content = nomeMembro;
        }

        private void fotoMembro_MouseDown(object sender, MouseButtonEventArgs e)
        {
                //if (codUsuario == amigo)
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
