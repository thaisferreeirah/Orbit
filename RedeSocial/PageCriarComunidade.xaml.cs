// PageCriarComunidade.xaml.cs
using RedeSocial.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RedeSocial
{
    public partial class PageCriarComunidade : Page
    {
        private UserManager userManager;
        private ComunidadeManager comunidadeManager;
        private string caminhoFoto;
        private int codUsuario;
        private Frame frameComunidade;
        private Frame MainFrame;
        private int novoCodigo;

        public PageCriarComunidade(ComunidadeManager comunidadeManager ,UserManager userManager, int codUsuario, Frame frameComunidade, Frame MainFrame, int novoCodigo)
        {
            InitializeComponent();
            this.userManager = userManager;
            this.codUsuario = codUsuario;
            this.comunidadeManager = comunidadeManager;
            this.frameComunidade = frameComunidade;
            this.MainFrame = MainFrame;
            this.novoCodigo = novoCodigo;

        }

        private void AdicionarImagem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Imagens (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                Title = "Selecionar Imagem da Comunidade"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                caminhoFoto = openFileDialog.FileName;
                //ImageBrush imageBrush = new ImageBrush(new BitmapImage(new Uri(caminhoFoto)));
                imgComunidade.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(caminhoFoto)),
                    Stretch = Stretch.UniformToFill,
                };
            }
        }

        private void CriarComunidade_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtNomeComunidade.Text;
            string descricao = txtDescricao.Text;

            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(descricao) && !string.IsNullOrEmpty(caminhoFoto))
            {
                //int novoCodigo = comunidadeManager.CodigoUnico();
                comunidadeManager.AdicionarComunidade(novoCodigo, nome, caminhoFoto, descricao);

                comunidadeManager.AssociarUsuarioAComunidade(codUsuario, novoCodigo);

                MainFrame.Navigate(new PageGrupo(comunidadeManager, userManager, novoCodigo, codUsuario, MainFrame));

                txtNomeComunidade.Clear();
                txtDescricao.Clear();
                imgComunidade.Fill = Brushes.Transparent;
            }
            else
            {
                MessageBox.Show("Por favor, preencha todos os campos e adicione uma imagem.");
            }
        }

        private void txtNomeComunidade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LabelVoltar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            frameComunidade.Navigate(new PageTodasComunidades(comunidadeManager, userManager, novoCodigo, codUsuario, frameComunidade, MainFrame));
        }
    }
}
