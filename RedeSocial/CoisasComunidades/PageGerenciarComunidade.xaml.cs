using RedeSocial.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows.Media;

namespace RedeSocial.CoisasComunidades
{
    public partial class PageGerenciarComunidade : Page
    {
        private int codComunidade;
        private ComunidadeManager comunidadeManager;
        private int codUsuario;
        private UserManager userManager;
        private Frame frameComunidade;
        private Frame MainFrame;
        private Home Home;


        private string novaFotoTemp;

        public PageGerenciarComunidade(int codComunidade, ComunidadeManager comunidadeManager, UserManager userManager, int codUsuario, Frame MainFrame, Home home)
        {
            InitializeComponent();
            this.codComunidade = codComunidade;
            this.comunidadeManager = comunidadeManager;
            this.codUsuario = codUsuario;
            this.userManager = userManager;
            this.frameComunidade = frameComunidade;
            this.MainFrame = MainFrame;
            this.Home = home;

            CarregarDadosComunidade();
        }

        private void CarregarDadosComunidade()
        {
            var comunidade = comunidadeManager.ObterComunidadePorCodigo(codComunidade);

            if (comunidade != null)
            {
                txtNomeComunidade.Text = comunidade.Nome;
                txtDescricao.Text = comunidade.Descricao;

                if (!string.IsNullOrEmpty(comunidade.Foto))
                {
                    imgComunidade.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(comunidade.Foto)),
                        Stretch = Stretch.UniformToFill,
                    };
                }
                else
                {
                    imgComunidade.Fill = Brushes.Gray;
                }
            }
            else
            {
                MessageBox.Show("Erro ao carregar dados da comunidade.");
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var nomeAtualizado = txtNomeComunidade.Text;
            var descricaoAtualizada = txtDescricao.Text;

            var sucesso = comunidadeManager.AtualizarComunidade(codComunidade, nomeAtualizado, descricaoAtualizada);

            if (sucesso)
            {
                if (!string.IsNullOrEmpty(novaFotoTemp))
                {
                    bool sucessoFoto = comunidadeManager.AtualizarImagemComunidade(codComunidade, novaFotoTemp);
                    if (!sucessoFoto)
                    {
                        MessageBox.Show("Erro ao atualizar a imagem da comunidade.");
                    }
                }

                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Não há página anterior para voltar.");
                }
            }
            else
            {
                MessageBox.Show("Erro ao atualizar a comunidade.");
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("Tem certeza de que deseja excluir esta comunidade?", "Excluir Comunidade", MessageBoxButton.YesNo);

            if (resultado == MessageBoxResult.Yes)
            {
                var comunidade = comunidadeManager.ObterComunidadePorCodigo(codComunidade);

                if (comunidade != null)
                {
                    foreach (var membro in comunidade.Membros.ToList())
                    {
                        comunidadeManager.RemoverUsuarioDaComunidade(membro, codComunidade);
                    }

                    bool sucesso = comunidadeManager.ExcluirComunidade(codComunidade);

                    if (sucesso)
                    {
                        MessageBox.Show("Comunidade excluída com sucesso.");
                        //Voltar para a página Comunidades
                        MainFrame.Navigate(new PageTesteComunidade(comunidadeManager, userManager, codUsuario, MainFrame, Home));


                    }
                    else
                    {
                        MessageBox.Show("Erro ao excluir a comunidade.");
                    }
                }
                else
                {
                    MessageBox.Show("Comunidade não encontrada.");
                }
            }
        }

        private void AlterarImagem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Imagens (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                Title = "Selecionar Nova Imagem da Comunidade"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                novaFotoTemp = openFileDialog.FileName;

                imgComunidade.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(novaFotoTemp)),
                    Stretch = Stretch.UniformToFill,
                };
            }

        }

        private void botaoVoltar_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Não há página anterior para voltar.");
            }
        }
    }
}
