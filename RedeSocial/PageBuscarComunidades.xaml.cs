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
using RedeSocial.CoisasComunidades;
using RedeSocial.Models;

namespace RedeSocial
{
    /// <summary>
    /// Interação lógica para PageBuscarComunidades.xam
    /// </summary>
    public partial class PageBuscarComunidades : Page
    {
        string pesquisa;
        private ComunidadeManager comunidadeManager;
        private UserManager userManager;
        private int codUsuario;
        private Frame frameComunidade;
        private Frame mainFrame;
        

        public ObservableCollection<ComunidadeViewModel> Comunidades { get; set; } = [];
        private int codComunidade;

        public PageBuscarComunidades(string _pesquisa, ComunidadeManager _comunidadeManager, UserManager _userManager, int _codComunidade, int _codUser, Frame _frameComunidade, Frame _mainFrame)
        {
            InitializeComponent();
            pesquisa = _pesquisa;
            comunidadeManager = _comunidadeManager;
            userManager = _userManager; 
            codComunidade = _codComunidade;
            codUsuario = _codUser;
            frameComunidade = _frameComunidade;
            mainFrame = _mainFrame;
            repetirLista(pesquisa);
        }

        private void criarCartaoComunidade(int codComunidade) 
        { 
            Frame frame = new Frame 
            { 
                Height = 230, 
                Width = 200 
            }; 
            PageCartaoComunidade pageCartaoComunidade = new PageCartaoComunidade(codComunidade, comunidadeManager, userManager, codUsuario, mainFrame); 
            frame.Navigate(pageCartaoComunidade); 
            gridComunidades.Children.Add(frame); 
        }
        public void repetirLista(string pesquisa)
        {
            var comunidades = comunidadeManager.ObterTodasAsComunidades();
            foreach (var comunidade in comunidades)
            {
                string nomeComunidade = comunidadeManager.BuscarNomeComunidade(comunidade.Codigo).ToLower();
                if (nomeComunidade.Contains(pesquisa.ToLower()))
                {
                    criarCartaoComunidade(comunidade.Codigo);
                }
            }
        }

    }
}
