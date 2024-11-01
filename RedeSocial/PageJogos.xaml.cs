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
using RedeSocial.CoisasJogos;
using RedeSocial.Models;

namespace RedeSocial
{
    /// <summary>
    /// Interação lógica para PageJogos.xam
    /// </summary>
    public partial class PageJogos : Page
    {
        Home home;

        int codUsuario;
        public PageJogos(int _codUsuario, Home _home)
        {
            InitializeComponent();

            home = _home;
            codUsuario = _codUsuario;
        }

        private void BotaoNariz_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            home.MainFrame.Navigate(new PageTelaJogo(codUsuario, home));
        }
    }
}
