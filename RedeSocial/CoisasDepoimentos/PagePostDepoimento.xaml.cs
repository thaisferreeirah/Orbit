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

namespace RedeSocial
{
    /// <summary>
    /// Interação lógica para PagePostDepoimento.xam
    /// </summary>
    public partial class PagePostDepoimento : Page
    {
        UserManager userManager = new UserManager();
        public PagePostDepoimento(int _remetente, int _destinatario, string _conteudo)
        {
            InitializeComponent();
            ExibirDepoimento(_remetente, _destinatario, _conteudo);
        }

        private void ExibirDepoimento(int remetente, int destinatario, string conteudo)
        {
            string nomeRem =userManager.BuscarNome(remetente);
            string nomeDest = userManager.BuscarNome(destinatario);
            fotoRemetente.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(userManager.BuscarFoto(remetente))),
                Stretch = Stretch.UniformToFill,
            };
            labelDestRem.Content = $"De {nomeRem} para {nomeDest} ";
            TextRange textRange = new TextRange(rtbConteudo.Document.ContentStart, rtbConteudo.Document.ContentEnd);
            using (var memoryStream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(conteudo)))
            {
                textRange.Load(memoryStream, DataFormats.Xaml);
            }
        }

        private void rtbConteudo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
