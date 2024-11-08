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
        ComunidadeManager comunidadeManager = new ComunidadeManager();

        public PageCartaoComunidade(int codComunidade)
        {
            InitializeComponent();
            ExibirComunidade(codComunidade);
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
    }
}
