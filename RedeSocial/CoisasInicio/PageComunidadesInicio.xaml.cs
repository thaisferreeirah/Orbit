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
        ComunidadeManager comunidadeManager = new ComunidadeManager();
        int codUsuario;
        public PageComunidadesInicio(int _codUsuario, Frame _mainFrame, Home _home)
        {
            InitializeComponent();
            codUsuario = _codUsuario;
            mainFrame = _mainFrame;
            home = _home;
            buscarComunidadesDoUsuario(_codUsuario);
        }
        //var amigosFotos = new[] { fotoComunidade1, fotoComunidade2, fotoComunidade3, fotoComunidade4, fotoComunidade5 };
        //var amigosNomes = new[] { nomeComunidade1, nomeComunidade2, nomeComunidade3, nomeComunidade4, nomeComunidade5 };

        public void buscarComunidadesDoUsuario(int codUsuario)
        {
            var comunidades = comunidadeManager.BuscarComunidadesDoUsuario(codUsuario);

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

    }
}
