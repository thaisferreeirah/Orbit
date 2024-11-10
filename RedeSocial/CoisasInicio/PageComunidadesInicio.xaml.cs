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
        private Frame mainFrame;
        private Home home;
        private UserManager userManager;
        private ComunidadeManager comunidadeManager;
        private int codUsuario;
        private Frame frameComunidade;
        private int[] comunidadesArray;
        private List<int> comunidades;

        public PageComunidadesInicio(UserManager userManager, int _codUsuario, Frame _mainFrame, Home _home, ComunidadeManager comunidadeManager)
        {
            InitializeComponent();
            this.codUsuario = _codUsuario;
            mainFrame = _mainFrame;
            home = _home;
            this.comunidadeManager = comunidadeManager;
            this.userManager = userManager;

            buscarComunidadesDoUsuario(_codUsuario);
            
        }


        public void buscarComunidadesDoUsuario(int codUsuario)
        {

            comunidades = comunidadeManager.BuscarComunidadesDoUsuario(codUsuario);

            // Arrays para armazenar os controles de exibição de fotos e nomes
            var comunidadeFotos = new[] { fotoComunidade1, fotoComunidade2, fotoComunidade3, fotoComunidade4, fotoComunidade5 };
            var comunidadeNomes = new[] { nomeComunidade1, nomeComunidade2, nomeComunidade3, nomeComunidade4, nomeComunidade5 };

            for (int i = 0; i < comunidades.Count && i < comunidadeFotos.Length; i++)
            {
                int codComunidade = comunidades[i];

                // Obtém o nome e a foto da comunidade
                string nomeComunidade = comunidadeManager.BuscarNomeComunidade(codComunidade);
                string caminhoFoto = comunidadeManager.BuscarFotoComunidade(codComunidade);

                // Configura o nome da comunidade no TextBlock correspondente
                comunidadeNomes[i].Text = nomeComunidade;

                // Tenta carregar a imagem da comunidade
                try
                {
                    comunidadeFotos[i].Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(caminhoFoto, UriKind.RelativeOrAbsolute)),
                        Stretch = Stretch.UniformToFill
                    };
                }
                catch (Exception)
                {
                    // Define uma imagem padrão caso ocorra um erro ao carregar a imagem
                    comunidadeFotos[i].Fill = new SolidColorBrush(Colors.Gray); // Cor de fallback ou imagem padrão
                    MessageBox.Show($"Erro ao carregar a imagem para a comunidade: {nomeComunidade}");
                }
            }

            // Mostra uma label com o total de comunidades caso o usuário tenha mais de 5
            if (comunidades.Count > 5)
            {
                labelVerTodas.Content = $"Ver todas ({comunidades.Count})";
                labelVerTodas.Visibility = Visibility.Visible;
            }
            else
            {
                labelVerTodas.Visibility = Visibility.Hidden;
            }

            /*
            var comunidades = comunidadeManager.BuscarComunidadesDoUsuario(codUsuario);
            comunidadesArray = new int[comunidades.Count];
            // Cria arrays para armazenar os nomes e as fotos da comunidade do usuário, para cada comunidade que o usuário está exibe a foto e o nome da comunidade até 5.
            var comunidadeFotos = new[] { fotoComunidade1, fotoComunidade2, fotoComunidade3, fotoComunidade4, fotoComunidade5 };
            var comunidadeNomes = new[] { nomeComunidade1, nomeComunidade2, nomeComunidade3, nomeComunidade4, nomeComunidade5 };

            for (int i = 0; i < comunidades.Count && i < comunidadeFotos.Length; i++)
            {
                int codComunidade = comunidades[i];
                comunidadesArray[i] = codComunidade;
                
                
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
            */
        }

        private void fotoComunidade_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, int comunidade)
        {
            mainFrame.Navigate(new PageGrupo(comunidadeManager, userManager, comunidade, codUsuario, mainFrame, frameComunidade, home));
        }
        private void fotoComunidade1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (comunidades.Count > 0)
                fotoComunidade_MouseLeftButtonDown(sender, e, comunidades[0]);
        }

        private void fotoComunidade2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (comunidades.Count > 1)
                fotoComunidade_MouseLeftButtonDown(sender, e, comunidades[1]);
        }

        private void fotoComunidade3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (comunidades.Count > 2)
                fotoComunidade_MouseLeftButtonDown(sender, e, comunidades[2]);
        }

        private void fotoComunidade4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (comunidades.Count > 3)
                fotoComunidade_MouseLeftButtonDown(sender, e, comunidades[3]);
        }

        private void fotoComunidade5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (comunidades.Count > 4)
                fotoComunidade_MouseLeftButtonDown(sender, e, comunidades[4]);
        }

        private void labelVerTodas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Navigate(new PageTesteComunidade(comunidadeManager, userManager, codUsuario, mainFrame, home));
        }
    }
}
