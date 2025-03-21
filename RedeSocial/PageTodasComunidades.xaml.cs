﻿using RedeSocial.CoisasComunidades;
using RedeSocial.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interação lógica para PageTodasComunidades.xam
    /// </summary>
    public partial class PageTodasComunidades : Page
    {
        private ComunidadeManager comunidadeManager;
        private UserManager userManager;
        private int codUsuario;
        private Frame frameComunidade;
        private Frame MainFrame;

        public ObservableCollection<ComunidadeViewModel> Comunidades { get; set; } = [];
        private int codComunidade;


        public PageTodasComunidades(ComunidadeManager comunidadeManager, UserManager userManager, int codComunidade, int codUser, Frame frameComunidade, Frame MainFrame)
        {
            InitializeComponent();
            this.comunidadeManager = comunidadeManager ?? throw new ArgumentNullException(nameof(comunidadeManager));
            this.userManager = userManager;
            this.codComunidade = codComunidade;
            this.MainFrame = MainFrame;
            this.frameComunidade = frameComunidade;
            this.codUsuario = codUser;

            DataContext = this;
            CarregarTodasAsComunidades();
        }

        private void CarregarTodasAsComunidades()
        {
            var todasAsComunidades = comunidadeManager.ObterTodasAsComunidades();

            if (todasAsComunidades != null && todasAsComunidades.Count > 0)
            {
                //Cria um cartão de comunidade para cada comunidade existente
                foreach (var comunidade in todasAsComunidades)
                {
                    int codigo = comunidade.Codigo;

                    PageCartaoComunidade pageCartaoComunidade = new PageCartaoComunidade(codigo, comunidadeManager, userManager, codUsuario, MainFrame);

                    Frame frame = new Frame()
                    {
                        Height = 230,
                        Width = 200
                    };
                    frame.Navigate(pageCartaoComunidade);
                    gridComunidades.Children.Add(frame);


                }
            }

        }

        private void AbrirPaginaComunidade(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.Tag is int codComunidade)
            {
                MainFrame.Navigate(new PageGrupo(comunidadeManager, userManager, codComunidade, codUsuario, frameComunidade, MainFrame, null));
            }
        }

    }
    public class ComunidadeViewModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
    }
}
