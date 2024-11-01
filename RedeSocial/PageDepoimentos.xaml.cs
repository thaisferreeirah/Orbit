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
    /// Interação lógica para PageDepoimentos.xam
    /// </summary>
    public partial class PageDepoimentos : Page
    {

        UserManager userManager = new UserManager();
        private GerenciarDepoimento gerenciarDepoimento;
        int codAmigo;
        int codUser;
        public PageDepoimentos(int _codUser)
        {
            InitializeComponent();
            codUser = _codUser;
            gerenciarDepoimento = new GerenciarDepoimento();
            CarregarAmigos(_codUser);
            CarregarDepoimentosExistentes();
        }

        private void CarregarAmigos(int codUsuario)
        {
            var amigos = userManager.BuscarListaAmigos(codUsuario);
            campoDestinatario.Items.Clear();
            for (int i = 0; i < userManager.BuscarQuantidadeAmigos(codUsuario); i++)
            {
                codAmigo = userManager.BuscarCodAmigo(codUsuario, i);
                if (codAmigo != codUsuario) // Garante que o próprio usuário não aparece na lista
                {
                    string nomeAmigo = userManager.BuscarNome(codAmigo); // Supondo que você tenha um método para buscar nome por código
                    campoDestinatario.Items.Add(new ComboBoxItem { Content = nomeAmigo, Tag = codAmigo });
                }
            }
        }

        private void botaoPostar_Click(object sender, RoutedEventArgs e)
        {
            if (campoDestinatario.SelectedItem == null)
            {
                MessageBox.Show("Selecione um amigo para enviar o depoimento.");
                return;
            }
            var amigoSelecionado = (ComboBoxItem)campoDestinatario.SelectedItem;

            if (amigoSelecionado.Tag == null)
            {
                MessageBox.Show("Erro: Amigo selecionado inválido.");
                return;
            }

            int destinatario = (int)amigoSelecionado.Tag;
            TextRange textRange = new TextRange(campoDepoimento.Document.ContentStart, campoDepoimento.Document.ContentEnd);
            string conteudo;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                textRange.Save(memoryStream, DataFormats.Xaml);
                conteudo = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }


            gerenciarDepoimento.ArmazenarDepoimento(codUser, destinatario, conteudo);
            CarregarDepoimentosExistentes();

        }


        private void CarregarDepoimentosExistentes()
        {
            gridDepoimentos.Children.Clear();
            int quantidadeDepoimentos = gerenciarDepoimento.BuscarQuantidade();
            //MessageBox.Show($"Quantidade de depoimentos: {quantidadeDepoimentos}");

            for (int i = quantidadeDepoimentos - 1; i >= 0; i--)
            {
                int remetente = gerenciarDepoimento.BuscarRemetente(i);
                int destinatario = gerenciarDepoimento.BuscarDestinatario(i);
                string conteudo = gerenciarDepoimento.BuscarConteúdo(i);
                Frame frame = new Frame
                {
                    Height = 225,
                    Width = 630
                };
                PagePostDepoimento postDepoimento = new PagePostDepoimento(remetente, destinatario, conteudo);
                frame.Navigate(postDepoimento);

                gridDepoimentos.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(frame, gridDepoimentos.RowDefinitions.Count - 1);
                gridDepoimentos.Children.Add(frame);
            }
        }

        private void FiltrarDepoimentosEnviados()
        {
            gridDepoimentos.Children.Clear();
            int quantidadeDepoimentos = gerenciarDepoimento.BuscarQuantidade();
            //MessageBox.Show($"Quantidade de depoimentos: {quantidadeDepoimentos}");

            for (int i = quantidadeDepoimentos - 1; i >= 0; i--)
            {
                var remetente = gerenciarDepoimento.BuscarRemetente(i);
                var destinatario = gerenciarDepoimento.BuscarDestinatario(i);
                var conteudo = gerenciarDepoimento.BuscarConteúdo(i);
                if (codUser == remetente)
                {
                    Frame frame = new Frame
                    {
                        Height = 225,
                        Width = 630
                    };
                    PagePostDepoimento postDepoimento = new PagePostDepoimento(remetente, destinatario, conteudo);
                    frame.Navigate(postDepoimento);

                    gridDepoimentos.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(frame, gridDepoimentos.RowDefinitions.Count - 1);
                    gridDepoimentos.Children.Add(frame);
                }
            }
        }

        private void FiltrarDepoimentosRecebidos()
        {
            gridDepoimentos.Children.Clear();
            int quantidadeDepoimentos = gerenciarDepoimento.BuscarQuantidade();
            //MessageBox.Show($"Quantidade de depoimentos: {quantidadeDepoimentos}");

            for (int i = quantidadeDepoimentos - 1; i >= 0; i--)
            {
                var remetente = gerenciarDepoimento.BuscarRemetente(i);
                var destinatario = gerenciarDepoimento.BuscarDestinatario(i);
                var conteudo = gerenciarDepoimento.BuscarConteúdo(i);
                if (codUser == destinatario)
                {
                    Frame frame = new Frame
                    {
                        Height = 225,
                        Width = 630
                    };
                    PagePostDepoimento postDepoimento = new PagePostDepoimento(remetente, destinatario, conteudo);
                    frame.Navigate(postDepoimento);

                    gridDepoimentos.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(frame, gridDepoimentos.RowDefinitions.Count - 1);
                    gridDepoimentos.Children.Add(frame);
                }
            }
        }

        private void botaoNegrito_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = campoDepoimento.Selection;
            if (!selectedText.IsEmpty)
            {
                if (selectedText.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold))
                {
                    selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                }
                else
                {
                    selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                };
            }
            campoDepoimento.Focus();
        }

        private void botaoItalico_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = campoDepoimento.Selection;
            if (!selectedText.IsEmpty)
            {
                if (selectedText.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic))
                {
                    selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                }
                else
                {
                    selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                }
            }
            campoDepoimento.Focus();
        }

        private void botaoSublinhado_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = campoDepoimento.Selection;
            if (!selectedText.IsEmpty)
            {
                TextDecorationCollection textDecorations = (TextDecorationCollection)selectedText.GetPropertyValue(Inline.TextDecorationsProperty);
                if (textDecorations == TextDecorations.Underline)
                {
                    selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }
                else
                {
                    selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                }
            }
            campoDepoimento.Focus();
        }

        private void botaoEnviados_Click(object sender, RoutedEventArgs e)
        {
            FiltrarDepoimentosEnviados();
        }

        private void botaoRecebidos_Click(object sender, RoutedEventArgs e)
        {
            FiltrarDepoimentosRecebidos();
        }
    }
}
