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
using RedeSocial.Models;

namespace RedeSocial.CoisasInicio
{
    /// <summary>
    /// Interação lógica para PageComunidadesInicio.xam
    /// </summary>
    public partial class PageComunidadesInicio : Page
    {
        Frame mainFrame;
        Home home;
        UserManager userManager = new UserManager();
        private ComunidadeManager comunidadeManager;
        int codUsuario;
        private Frame frameComunidade;
        public PageComunidadesInicio(int _codUsuario, Frame _mainFrame, Home _home, ComunidadeManager comunidadeManager)
        {
            InitializeComponent();
            codUsuario = _codUsuario;
            mainFrame = _mainFrame;
            home = _home;
            this.comunidadeManager = comunidadeManager;
            buscarComunidadesDoUsuario(_codUsuario);
        }


        public void buscarComunidadesDoUsuario(int codUsuario)
        {
            
            var comunidades = comunidadeManager.BuscarComunidadesDoUsuario(codUsuario);
            // Cria arrays para armazenar os nomes e as fotos da comunidade do usuário, para cada comunidade que o usuário está exibe a foto e o nome da comunidade até 5.
            var comunidadeFotos = new[] { fotoComunidade1, fotoComunidade2, fotoComunidade3, fotoComunidade4, fotoComunidade5 };
            var comunidadeNomes = new[] { nomeComunidade1, nomeComunidade2, nomeComunidade3, nomeComunidade4, nomeComunidade5 };

            for (int i = 0; i < comunidades.Count && i < comunidadeFotos.Length; i++)
            {
                int codComunidade = comunidades[i];
                comunidadeFotos[i].Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(comunidadeManager.BuscarFotoComunidade(codComunidade))),
                    Stretch = Stretch.UniformToFill
                };
                comunidadeNomes[i].Text = comunidadeManager.BuscarNomeComunidade(codComunidade);
            }

            if (comunidades.Count > 5)
            {
                labelVerTodas.Content = $"Ver todas ({comunidades.Count})";
                labelVerTodas.Visibility = Visibility.Visible;
            }
        }

        private void fotoComunidade_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, int comunidade)
        {
            mainFrame.Navigate(new PageGrupo(comunidadeManager, userManager, comunidade, codUsuario, mainFrame));
        }
        private void fotoComunidade1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fotoComunidade_MouseLeftButtonDown(sender, e, 0);
        }

        private void fotoComunidade2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fotoComunidade_MouseLeftButtonDown(sender, e, 1);
        }

        private void fotoComunidade3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fotoComunidade_MouseLeftButtonDown(sender, e, 2);
        }

        private void fotoComunidade4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fotoComunidade_MouseLeftButtonDown(sender, e, 3);
        }

        private void fotoComunidade5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fotoComunidade_MouseLeftButtonDown(sender, e, 4);
        }

        private void labelVerTodas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Navigate(new PageTesteComunidade(comunidadeManager, userManager, codUsuario, mainFrame, home));
        }
    }
}
