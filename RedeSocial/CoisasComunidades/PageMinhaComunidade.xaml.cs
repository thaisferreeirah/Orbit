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
        private ComunidadeManager comunidadeManager;
        private UserManager userManager;
        private int codUsuario;
        private Frame frameComunidade;
        private Frame MainFrame;
        private int codComunidade;

        public PageMinhaComunidade(ComunidadeManager comunidadeManager, UserManager userManager, int codComunidade, int codUser, Frame frameComunidade, Frame MainFrame)
        {
            InitializeComponent();
            this.comunidadeManager = comunidadeManager;
            this.userManager = userManager;
            this.MainFrame = MainFrame;
            this.frameComunidade = frameComunidade;
            this.codUsuario = codUser;
            this.codComunidade = codComunidade;

            MessageBox.Show($"`{codComunidade}");
            ExibirMembro(codComunidade);
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


    }
}
