using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace RedeSocial.CoisasInicio
{
    /// <summary>
    /// Interação lógica para PageAmizadesInicio.xam
    /// </summary>
    public partial class PageAmizadesInicio : Page
    {
        Frame mainFrame;
        Home home;
        UserManager userManager = new UserManager();
        int codUsuario;
        int[] amigos = new int[5];
        public PageAmizadesInicio(int _codUsuario,Frame _mainFrame, Home _home)
        {
            InitializeComponent();
            codUsuario = _codUsuario;
            mainFrame = _mainFrame;
            home = _home;
            buscarPerfilAmigo(_codUsuario);
        }

        public void buscarPerfilAmigo(int cod)
        {
            int quantidadeAmigos = userManager.BuscarQuantidadeAmigos(cod);

            var amigosFotos = new[] { fotoUsuario1, fotoUsuario2, fotoUsuario3, fotoUsuario4, fotoUsuario5};
            var amigosNomes = new[] { labelNome1, labelNome2, labelNome3, labelNome4, labelNome5};

            for (int i = 0; i < quantidadeAmigos && i < amigosFotos.Length; i++)
            {
                int codAmigo = userManager.BuscarCodAmigo(cod, i);
                amigosFotos[i].Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(userManager.BuscarFoto(codAmigo))),
                    Stretch = Stretch.UniformToFill,
                };
                amigosNomes[i].Content = userManager.BuscarNome(codAmigo);
                amigos[i] = codAmigo;
            }

            if (quantidadeAmigos > 5)
            {
                labelVerTodos.Content = $"Ver todos ({quantidadeAmigos})";
                labelVerTodos.Visibility = Visibility.Visible;
            }
        }

        private void fotoUsuario_MouseLeftButtonUp(object sender, MouseButtonEventArgs e, int amigo)
        {
                mainFrame.Navigate(new PagePerfilOutros(codUsuario, amigo, mainFrame, home));
        }

        private void fotoUsuario1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fotoUsuario_MouseLeftButtonUp(sender, e, amigos[0]);
        }

        private void fotoUsuario2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fotoUsuario_MouseLeftButtonUp(sender, e, amigos[1]);
        }
        private void fotoUsuario3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fotoUsuario_MouseLeftButtonUp(sender, e, amigos[2]);
        }

        private void fotoUsuario4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fotoUsuario_MouseLeftButtonUp(sender, e, amigos[3]);
        }

        private void fotoUsuario5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fotoUsuario_MouseLeftButtonUp(sender, e, amigos[4]);
        }

        private void labelVerTodos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Navigate(new PageAmigos(codUsuario, mainFrame, home));
        }
    }
}
