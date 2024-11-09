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
    public partial class PageCartaoComunidade : Page
    {
        private ComunidadeManager comunidadeManager;
        private int codComunidade;
        private UserManager userManager;
        int codUsuario;
        Frame mainFrame;
        public PageCartaoComunidade(int _codComunidade, ComunidadeManager _comunidadeManager, UserManager _userManager, int _codUsuario, Frame _MainFrame)
        {
            InitializeComponent();
            this.comunidadeManager = _comunidadeManager;
            codComunidade = _codComunidade;
            userManager = _userManager;
            codUsuario = _codUsuario;
            mainFrame = _MainFrame;
            ExibirComunidade(_codComunidade);
        }

        private void ExibirComunidade(int codComunidade)
        {
            fotoDaComunidade.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(comunidadeManager.BuscarFotoComunidade(codComunidade))),
                Stretch = Stretch.UniformToFill,
            };
            nomeDaComunidade.Text = comunidadeManager.BuscarNomeComunidade(codComunidade);
        }

        private void fotoDaComunidade_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Navigate(new PageGrupo(comunidadeManager, userManager, codComunidade, codUsuario, mainFrame));
        }
    }
}
