using RedeSocial.CoisasComunidades;
using RedeSocial.Models;
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
    /// Interação lógica para PageTesteComunidade.xam
    /// </summary>
    public partial class PageTesteComunidade : Page
    {
        private ComunidadeManager comunidadeManager;
        private UserManager userManager;
        private int codUsuario;
        private Frame MainFrame;
        private int novoCodigo;


        public PageTesteComunidade(ComunidadeManager comunidadeManager, UserManager userManager, int codUsuario, Frame MainFrame)
        {
            InitializeComponent();
            this.userManager = userManager;
            this.comunidadeManager = comunidadeManager;
            this.codUsuario = codUsuario;
            this.MainFrame = MainFrame;

            ExibirMinhasComunidades(codUsuario);
            TodasComunidades();
        }

        private void AbrirCriarComunidade()
        {
            novoCodigo = comunidadeManager.CodigoUnico();
            frameComunidade.Navigate(new PageCriarComunidade(comunidadeManager, userManager, codUsuario, frameComunidade, MainFrame, novoCodigo));
        }

        public void AbrirPageGrupo()
        {

        }

        private void ExibirMinhasComunidades(int codUsuario)
        {
            gridMinhasComunidades.Children.Clear();
            gridMinhasComunidades.RowDefinitions.Clear();

            // Iterar por todas as comunidades
            foreach (var comunidade in comunidadeManager.ObterTodasAsComunidades())
            {
                // Verifica se o usuário está na comunidade
                if (comunidade.VerificarMembro(codUsuario))
                {
                    int codComunidade = comunidade.Codigo;

                    Frame frame = new Frame
                    {
                        Height = 60,
                        Width = 330
                    };
                    //PageMinhaComunidade cartaoComunidade = new PageMinhaComunidade(codComunidade);
                    //frame.Navigate(cartaoComunidade);

                    frame.Navigate(new PageMinhaComunidade(comunidadeManager, codComunidade));
                    gridMinhasComunidades.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(frame, gridMinhasComunidades.RowDefinitions.Count - 1);
                    gridMinhasComunidades.Children.Add(frame);
                }
            }
        }

        private void AbrirCriarComunidade_Click(object sender, RoutedEventArgs e)
        {
            AbrirCriarComunidade();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AbrirTodasAsComunidades_Click(object sender, RoutedEventArgs e)
        {
            frameComunidade.Navigate(new PageTodasComunidades(comunidadeManager, userManager, novoCodigo, codUsuario, frameComunidade, MainFrame));
        }
        private void TodasComunidades() {
            frameComunidade.Navigate(new PageTodasComunidades(comunidadeManager, userManager, novoCodigo, codUsuario, frameComunidade, MainFrame));
        }
    }
}
