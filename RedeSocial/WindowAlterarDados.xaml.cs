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
using System.Windows.Shapes;

namespace RedeSocial
{
    /// <summary>
    /// Lógica interna para WindowAlterarDados.xaml
    /// </summary>
    public partial class WindowAlterarDados : Window
    {
        UserManager userManager = new UserManager();

        int codUsuario;
        public WindowAlterarDados(int codUsuario)
        {
            InitializeComponent();

            this.codUsuario = codUsuario;

            CarregarDados();
        }

        private void BotaoAlterarDados_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CampoNome.Text) || string.IsNullOrEmpty(CampoEmail.Text))
            {
                MessageBox.Show("Preencha todos os campos");
            }
            else
            {
                MessageBox.Show(userManager.AtualizarDados(codUsuario, CampoNome.Text, CampoEmail.Text));
                this.Close();
            }
        }

        private void BotaoAlterarSenha_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(userManager.AtualizarSenha(codUsuario, CampoSenhaAtual.Password, CampoSenhaNova.Password, CampoRepetirSenhaNova.Password));
            this.Close();
        }

        private void CampoSenhaAtual_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CampoSenhaAtual.Password))
            {
                LabelSenhaAtual.Visibility = Visibility.Visible;
            }
            else
            {
                LabelSenhaAtual.Visibility = Visibility.Hidden;
            }
        }

        private void CampoSenhaNova_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CampoSenhaNova.Password))
            {
                LabelSenhaNova.Visibility = Visibility.Visible;
            }
            else
            {
                LabelSenhaNova.Visibility = Visibility.Hidden;
            }
        }

        private void CampoRepetirSenhaNova_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CampoRepetirSenhaNova.Password))
            {
                LabelRepetirSenhaNova.Visibility = Visibility.Visible;
            }
            else
            {
                LabelRepetirSenhaNova.Visibility = Visibility.Hidden;
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CarregarDados()
        {
            CampoNome.Text = userManager.BuscarNome(codUsuario);
            CampoEmail.Text = userManager.BuscarEmail(codUsuario);
        }
    }
}
