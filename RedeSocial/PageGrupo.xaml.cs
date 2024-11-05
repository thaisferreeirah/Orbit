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
using System.Collections.ObjectModel;
using System.Collections;
using RedeSocial.Models;

namespace RedeSocial
{
    /// <summary>
    /// Interação lógica para PageGrupo.xam
    /// </summary>
    public partial class PageGrupo : Page
    {
        public ObservableCollection<MembroViewModel> Membros { get; set; } = [];
        public string NomeComunidade { get; set; }
        public string DescricaoComunidade { get; set; }
        public string FotoComunidade { get; set; }

        private UserManager userManager;
        private ComunidadeManager comunidadeManager;
        private int codComunidade;
        private int codUsuario;
        private Frame frameComunidade;
        private Frame MainFrame;

        public bool ParticiparHabilitado { get; set; }


        public PageGrupo(ComunidadeManager comunidadeManager, UserManager userManager, int codComunidade, int codUsuario, Frame frameComunidade, Frame MainFrame)
        {
            InitializeComponent();
            this.userManager = userManager;
            this.comunidadeManager = comunidadeManager ?? throw new ArgumentNullException(nameof(comunidadeManager));
            this.codComunidade = codComunidade;
            this.frameComunidade = frameComunidade;
            this.MainFrame = MainFrame;
            this.codUsuario = codUsuario;
            DataContext = this;
            CarregarInformacoesDaComunidade();
            VerificarParticipacao();


        }

        private void CarregarInformacoesDaComunidade()
        {
            // Verifique se comunidadeManager não é nulo antes de utilizá-lo
            if (comunidadeManager != null)
            {
                
                var comunidade = comunidadeManager.ObterComunidadePorCodigo(codComunidade);

                if (comunidade != null)
                {
                    // Carregar informações na interface
                    string NomeComunidade =  comunidadeManager.BuscarNomeComunidade(codComunidade);
                    labelNomeComunidade.Content = NomeComunidade;

                    string DescricaoComunidade = comunidadeManager.BuscarDescricaoComunidade(codComunidade);
                    labelDescricaoComunidade.Content = DescricaoComunidade;

                    int QuantidadeMembros = comunidade.Membros.Count();
                    QTD_Membros.Content = (QuantidadeMembros + " Membros");

                    FotoComuni.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(comunidadeManager.BuscarFotoComunidade(codComunidade))),
                        Stretch = Stretch.UniformToFill
                    };

                            foreach (int codUsuario in comunidade.Membros)
                            {
                                //MessageBox.Show($"Processando usuário com código: {codUsuario}");

                                string nome = userManager.BuscarNome(codUsuario);
                                string foto = userManager.BuscarFoto(codUsuario);
                                
                                if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(foto))
                                {
                                    Membros.Add(new MembroViewModel { Nome = nome, Foto = foto });
                                }
                            }
                    
                        }
                    else
                    {
                        MessageBox.Show("Comunidade não encontrada.");
                    }
                }
                else
                {
                    MessageBox.Show("Erro: ComunidadeManager está nulo.");
                }
            }

        private void VoltarPag_Click(object sender, RoutedEventArgs e)
        {
            //MainFrame.Navigate(new PageTesteComunidade(comunidadeManager, userManager, codComunidade, Frame frameComunidade, Frame MainFrame));
        }

        private void VerificarParticipacao()
        {
            var comunidade = comunidadeManager.ObterComunidadePorCodigo(codComunidade);
            if (comunidade != null)
            {
                // Verifica diretamente se o usuário está na lista de membros da comunidade
                if (comunidade.Membros.Contains(codUsuario))
                {
                    ParticiparHabilitado = false;
                    btnParticipar.Content = "Participando da Comunidade";
                }
                else
                {
                    ParticiparHabilitado = true;
                }
                btnParticipar.IsEnabled = ParticiparHabilitado;
            }
            else
            {
                MessageBox.Show("Comunidade não encontrada.");
            }
        }
    }



    public class MembroViewModel
        {
            public string Nome { get; set; }
            public string Foto { get; set; }
        }
    }
