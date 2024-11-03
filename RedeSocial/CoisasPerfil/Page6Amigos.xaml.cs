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
    /// Interação lógica para Page6Amigos.xam
    /// </summary>
    public partial class Page6Amigos : Page
    {
        UserManager userManager = new UserManager();

        Frame mainFrame;
        Home mainWindow;


        int codUsuario;
        int codAmigo;
        int posAmigo;
        int codPerfil;

        int[] amigos = new int[6];
        public Page6Amigos(int _codUsuario, Frame _mainFrame, Home _mainWindow, int? _codPerfil = null)
        {
            InitializeComponent();
            mainFrame = _mainFrame;
            codUsuario = _codUsuario;
            mainWindow = _mainWindow;
            if (_codPerfil.HasValue)
            {
                codPerfil = _codPerfil.Value;
            }

            if (_codPerfil != null)
            {
                buscarPerfilAmigo(codPerfil);
            }
            else
            {
                buscarPerfilAmigo(codUsuario);
            }
        }
        public void buscarPerfilAmigo(int cod)
        {
            int quantidadeAmigos = userManager.BuscarQuantidadeAmigos(cod);

            var amigosFotos = new[] { fotoUsuario1, fotoUsuario2, fotoUsuario3, fotoUsuario4, fotoUsuario5, fotoUsuario6 };
            var amigosNomes = new[] { nomeUsuario1, nomeUsuario2, nomeUsuario3, nomeUsuario4, nomeUsuario5, nomeUsuario6 };

            for (int i = 0; i < quantidadeAmigos && i < amigosFotos.Length; i++)
            {
                int codAmigo = userManager.BuscarCodAmigo(cod, i);
                amigosFotos[i].Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(userManager.BuscarFoto(codAmigo))),
                    Stretch = Stretch.UniformToFill,
                };
                amigosNomes[i].Text = userManager.BuscarNome(codAmigo);
                amigos[i] = codAmigo;
            }
            if(codPerfil == codUsuario)
            {
                fotoUsuario6.Visibility = Visibility.Collapsed;
                nomeUsuario6.Visibility= Visibility.Collapsed;
                labelVerTodos.Content = $"Ver todos ({quantidadeAmigos})";
                labelVerTodos.Visibility = Visibility.Visible;
            }
        }

        private void fotoUsuario_MouseLeftButtonUp(object sender, MouseButtonEventArgs e, int amigo)
        {
            if (codUsuario == amigo)
            {
                mainFrame.Navigate(new PagePerfil(codUsuario, mainWindow, mainFrame));
            }
            else
            {
                mainFrame.Navigate(new PagePerfilOutros(codUsuario, amigo, mainFrame, mainWindow));
            }
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
        private void fotoUsuario6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fotoUsuario_MouseLeftButtonUp(sender, e, amigos[4]);
        }

        private void labelVerTodos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Navigate(new PageAmigos(codUsuario, mainFrame, mainWindow));
        }
    }
}
