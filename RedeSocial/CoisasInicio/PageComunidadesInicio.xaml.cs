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
        int codUsuario;
        public PageComunidadesInicio(int _codUsuario, Frame _mainFrame, Home _home)
        {
            InitializeComponent();
            codUsuario = _codUsuario;
            mainFrame = _mainFrame;
            home = _home;
        }
    }
}
