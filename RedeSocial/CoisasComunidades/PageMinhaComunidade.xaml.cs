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

namespace RedeSocial.CoisasComunidades
{
    /// <summary>
    /// Interação lógica para PageCartaoComunidade.xam
    /// </summary>
    public partial class PageMinhaComunidade : Page
    {
        private UserManager userManager;
        private ComunidadeManager comunidadeManager;
        private int codComunidade;
        private int codUsuario;
        private Frame frameComunidade;
        private Frame MainFrame;

        public PageMinhaComunidade(ComunidadeManager comunidadeManager, int _codComunidade, UserManager _userManager, int _codUsuario, Frame frameComunidade, Frame _mainFrame)
        {
            InitializeComponent();
            this.comunidadeManager = comunidadeManager;

            ExibirMembro(_codComunidade);
        }
        private void ExibirMembro(int codComunidade)
        {
            string nomeComunidade = comunidadeManager.BuscarNomeComunidade(codComunidade);
            fotoComunidade.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(comunidadeManager.BuscarFotoComunidade(codComunidade))),
                Stretch = Stretch.UniformToFill,
            };
            labelNomeComunidade.Content = nomeComunidade;
        }

        private void AbrirComunidade(object sender, MouseButtonEventArgs e)
        {
            //MainFrame.Navigate(new PageGrupo(comunidadeManager, userManager, codComunidade, codUsuario, frameComunidade, MainFrame));
        }
    }
}
