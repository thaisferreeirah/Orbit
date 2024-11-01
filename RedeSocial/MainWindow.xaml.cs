using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RedeSocial.Models;

namespace RedeSocial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserManager userManager = new UserManager();
        private NarizManager narizManager = new NarizManager();
        private Teste teste = new Teste();
        private ChatList chatList;
       

        public MainWindow(ChatList _chatList = null)
        {
            InitializeComponent();

            chatList = _chatList;

            InstanciarChatList();
        }

        private void BotaoEntrar_Click(object sender, RoutedEventArgs e)
        {
            string email = CampoUsuario.Text;
            string password = CampoSenha.Password;
            string resultado = userManager.Logar(email, password);

            if (resultado != "Logado com sucesso!")
                MessageBox.Show(resultado);

            if (resultado == "Logado com sucesso!")
            {
                Home homeWindow = new Home(userManager.BuscarCodigoUsuario(email), chatList);
                homeWindow.Show();
                this.Close();
            }
        }

        private void BotaoCadastrar_Click(object sender, RoutedEventArgs e)
        {
            string email = CampoEmailCadastro.Text;
            string id = CampoIDCadastro.Text;
            string password = CampoSenhaCadastro.Password;
            string confirmPassword = CampoRepetirSenha.Password;
            string fullName = CampoNomeCadastro.Text;

            if (!CampoDataNascimentoCadastro.SelectedDate.HasValue)
            {
                MessageBox.Show("Por favor, selecione uma data de nascimento.");
                return;
            }
            DateOnly birthDate = DateOnly.FromDateTime(CampoDataNascimentoCadastro.SelectedDate.Value);

            string resultado = userManager.Registrar(email, id, password, confirmPassword, fullName, birthDate);
            MessageBox.Show(resultado);

            if (resultado == "Usuário registrado com sucesso!")
            {
                string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                userManager.AdicionarFoto(userManager.BuscarCodigoUsuario(CampoIDCadastro.Text), projectPath + "\\Fotos\\SemFoto.jpeg");
                userManager.AdicionarCapa(userManager.BuscarCodigoUsuario(CampoIDCadastro.Text), projectPath + "\\Fotos\\SemFoto.jpeg");
                
                CampoEmailCadastro.Clear();
                CampoIDCadastro.Clear();
                CampoSenhaCadastro.Clear();
                CampoRepetirSenha.Clear();
                CampoNomeCadastro.Clear();
                CampoDataNascimentoCadastro.SelectedDate = null;
                tabMenu.SelectedIndex = 0;

                narizManager.AdicionarUsuario();
            }
        }

        private void BotaoTeste_Click(object sender, RoutedEventArgs e)
        {
            teste.AdicionarTeste(chatList);

            BotaoTeste.IsEnabled = false;
        }

        private void InstanciarChatList()
        {
            if (chatList == null)
            {
                chatList = new ChatList();
            }
        }

        #region SelectionChanged, KeyDown e PasswordChanged
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabMenu.SelectedIndex == 1)
            {
                tabMenu.Height = 533;
            }
            else
            {
                tabMenu.Height = 358;
            }
        }

        private void CampoUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BotaoEntrar_Click(BotaoEntrar, e);
            }
        }

        private void CampoSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BotaoEntrar_Click(BotaoEntrar, e);
            }
        }

        private void CampoSenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CampoSenha.Password))
            {
                labelSenha.Visibility = Visibility.Visible;
            }
            else
            {
                labelSenha.Visibility = Visibility.Hidden;
            }
        }

        private void CampoSenhaCadastro_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CampoSenhaCadastro.Password))
            {
                LabelSenhaCadastro.Visibility = Visibility.Visible;
            }
            else
            {
                LabelSenhaCadastro.Visibility = Visibility.Hidden;
            }
        }

        private void CampoRepetirSenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CampoRepetirSenha.Password))
            {
                LabelRepetirSenha.Visibility = Visibility.Visible;
            }
            else
            {
                LabelRepetirSenha.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        private void BotaoRecuperarSenha_Click(object sender, RoutedEventArgs e)
        {
            // Apenas abre a janela de recuperação de senha
            Window1 windowRecuperacaoSenha = new Window1();
            windowRecuperacaoSenha.ShowDialog();
        }
    }
}