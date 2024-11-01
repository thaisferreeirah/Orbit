using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using RedeSocial.CoisasChat;
using RedeSocial.Models;

namespace RedeSocial
{
    /// <summary>
    /// Lógica interna para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private UserManager usuarioManager = new UserManager();
        ChatList chatList;

        PagePost pagePost;
        WindowNotificacao windowNotificacao;

        int codUsuario;
        string pesquisa;
        bool novaNotificacao;
        public Home(int _codUsuario, ChatList _chatList)
        {
            InitializeComponent();

            codUsuario = _codUsuario;
            novaNotificacao = false;
            chatList = _chatList;
            pagePost = new PagePost(codUsuario);

            AtualizarFotoPerfil();
            ChecarNotificacao();

            MainFrame.Navigate(pagePost);
        }

        private void BotaoBuscar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pesquisa = CaixaPesquisa.Text.ToString();
            MainFrame.Navigate(new PageBuscar(codUsuario, MainFrame, this, pesquisa));
        }

        private void BotaoPerfil_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PagePerfil(codUsuario, this, MainFrame));
        }

        private void BotaoInicio_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PagePost(codUsuario));
        }

        private void BotaoAmigos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PageAmigos(codUsuario, MainFrame, this));
        }

        private void BotaoConversas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PageChat(codUsuario, chatList));
        }

        private void BotaoDepoimentos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PageDepoimentos(codUsuario));
        }

        private void BotaoComunidades_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PageComunidades());
        }

        private void BotaoJogos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new PageJogos(codUsuario, this));
        }

        private void BotaoSair_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow1 = new MainWindow(chatList);
            mainWindow1.Show();
            this.Close();
        }

        private void BotaoSair_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new(chatList);
            mainWindow.Show();
            this.Close();
        }

        private void BotaoMenu_Checked(object sender, RoutedEventArgs e)
        {
            //MainFrame.Opacity = 0.3;
        }

        private void BotaoMenu_Unchecked(object sender, RoutedEventArgs e)
        {
            // MainFrame.Opacity = 1.0;
        }

        private void AtualizarFotoPerfil()
        {
            EllipseFotoUser.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(usuarioManager.BuscarFoto(codUsuario)))
            };
        }

        private void CaixaPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                pesquisa = CaixaPesquisa.Text.ToString();
                PageBuscar pageBuscar = new PageBuscar(codUsuario, MainFrame, this, pesquisa);
                MainFrame.Navigate(pageBuscar);
            }
        }

        private void BotaoNotificacao_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BotaoNotificacao.Source = new BitmapImage(new Uri("pack://application:,,,/RedeSocial;component/Icones/Notificacao.png"));

            Point buttonPosition = BotaoNotificacao.TransformToAncestor(this).Transform(new Point(0, 0));

            Point mainWindowPosition = new Point(this.Left, this.Top);

            Point modalPosition = new Point(mainWindowPosition.X + buttonPosition.X + BotaoNotificacao.ActualWidth,
                                            mainWindowPosition.Y + buttonPosition.Y);

            windowNotificacao = new WindowNotificacao(codUsuario)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = modalPosition.X - 280,
                Top = modalPosition.Y + 80
            };

            windowNotificacao.Show();
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (windowNotificacao != null && !windowNotificacao.IsMouseOver)
            {
                windowNotificacao.Close();
                windowNotificacao = null;
            }
        }

        private void ChecarNotificacao()
        {
            for (int i = 0; i < usuarioManager.BuscarQuantidadeNotificacao(codUsuario); i++)
            {
                if (!usuarioManager.VerificarNotificacaoVerificacao(codUsuario, i))
                {
                    novaNotificacao = true;
                    BotaoNotificacao.Source = new BitmapImage(new Uri("pack://application:,,,/RedeSocial;component/Icones/NotificacaoNova.png"));
                    break;
                }
            }
        }
    }
}
