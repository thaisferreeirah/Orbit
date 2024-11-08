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
    /// Interação lógica para PageAmigos.xam
    /// </summary>
    public partial class PageAmigos : Page
    {
        UserManager userManager = new UserManager();
        Frame mainFrame;
        Home mainWindow;
        public PageAmigos(int codUser, Frame _mainFrame, Home _mainWindow)
        {
            InitializeComponent();

            mainFrame = _mainFrame;
            mainWindow = _mainWindow;

            repetirListaRecebidas(codUser);
            repetirListaEnviadas(codUser);
            repetirListaAmigos(codUser);
        }
        public void listarUsuarioRecebidas(int codUser, int codPerfil)
        {
            PageCartaoSolicitacao pageCartaoSolicitacao = new PageCartaoSolicitacao(codUser, codPerfil, mainFrame);

            Frame frame = new Frame()
            {
                Height = 345,
                Width = 230
            };
            frame.Navigate(pageCartaoSolicitacao);

            gridSolicitacaoRecebida.Children.Add(frame);
        }

        public void repetirListaRecebidas(int codUser)
        {
            for (int i = 0; i < userManager.BuscarQuantidade(); i++)
            {
                if (i != codUser)
                {
                    if (userManager.VerificarSolicitacao(i, codUser))
                    {
                        listarUsuarioRecebidas(codUser, i);
                        gridSolicAmizade.Visibility = Visibility.Visible;
                    }
                }
            }
        }
        //atualizar a pagina  com os cartoes depois de ja ter adicionado
        public void listarUsuarioEnviadas(int codUser, int codPerfil)
        {
            PageCartaoSolicitacaoEnviada pageCartaoSolicitacaoEnviada = new PageCartaoSolicitacaoEnviada(codUser, codPerfil, mainFrame, mainWindow);

            Frame frame = new Frame()
            {
                Height = 300,
                Width = 230
            };
            frame.Navigate(pageCartaoSolicitacaoEnviada);

            gridSolicitacaoEnviada.Children.Add(frame);
        }

        public void repetirListaEnviadas(int codUser)
        {
            for (int i = 0; i < userManager.BuscarQuantidade(); i++)
            {
                if (i != codUser)
                {
                    if (userManager.VerificarSolicitacao(codUser, i))
                    {
                        listarUsuarioEnviadas(codUser, i);
                        gridSolicEnviada.Visibility = Visibility.Visible;
                    }

                }
            }
        }

        public void listarUsuarioAmigos(int codUser, int codPerfil)
        {
            PageCartaoAmigo pageCartaoAmigo = new PageCartaoAmigo(codUser, codPerfil, mainFrame, mainWindow);

            Frame frame = new Frame()
            {
                Height = 310,
                Width = 230
            };
            frame.Navigate(pageCartaoAmigo);

            gridAmigos.Children.Add(frame);
        }

        public void repetirListaAmigos(int codUser)
        {
            for (int i = 0; i < userManager.BuscarQuantidade(); i++)
            {
                if (i != codUser)
                {
                    if (userManager.VerificarCodAmigo(i, codUser))
                    {
                        listarUsuarioAmigos(codUser, i);
                        LabelNenhumAmigo.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
    }
}
