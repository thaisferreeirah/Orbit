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

namespace RedeSocial.CoisasJogos
{
    /// <summary>
    /// Interação lógica para PageTelaJogo.xam
    /// </summary>
    public partial class PageTelaJogo : Page
    {
        Home home;
        int codUsuario;
        public PageTelaJogo(int _codUsuario, Home _home)
        {
            InitializeComponent();

            home = _home;
            codUsuario = _codUsuario;
            FrameJogo.Navigate(new PageNariz(codUsuario));
        }

        private void BotaoVoltar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            home.MainFrame.Navigate(new PageJogos(codUsuario, home));
        }

        private void BotaoVoltar_MouseEnter(object sender, MouseEventArgs e)
        {
            ImageVoltar.Height = 32;
            ImageVoltar.Width = 32;
            LabelVoltar.FontWeight = FontWeights.Bold;
        }

        private void BotaoVoltar_MouseLeave(object sender, MouseEventArgs e)
        {
            ImageVoltar.Height = 30;
            ImageVoltar.Width = 30;
            LabelVoltar.FontWeight = FontWeights.Regular;
        }
    }
}
