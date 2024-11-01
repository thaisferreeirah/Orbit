using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Windows;

namespace RedeSocial
{
    /// <summary>
    /// Lógica interna para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        UserManager userManager = new UserManager();
        private string senhaRecebida;
        public Window1()
        {
            InitializeComponent();
        }
        public Window1(string senha)
       
        {
            InitializeComponent();
            senhaRecebida = senha; ; //armazena a senha recebida
        }

        private void EnviarEmail_Click(object sender, RoutedEventArgs e)
        {
            string emailUsuario = txtEmail.Text;
            if (string.IsNullOrEmpty(emailUsuario))
            {
                MessageBox.Show("Por favor, insira o e-mail cadastrado");
                return;
            }

            string novaSenha = GerarNovaSenha();
           
            try
            {
                EnviarEmailRecuperacao(emailUsuario, novaSenha);
                //AtualizarSenhaEmArquivo(emailUsuario, novaSenha);
                MessageBox.Show($"E-mail de recuperação enviado para {emailUsuario}");
                userManager.AlterarSenha(userManager.BuscarCodigoUsuarioPorEmail(emailUsuario), novaSenha);
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao enviar o e-mail: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GerarNovaSenha()
        {
            const string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            int tamanhoSenha = random.Next(8, 12); // Define o tamanho da senha
            return new string(Enumerable.Repeat(caracteres, tamanhoSenha)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void EnviarEmailRecuperacao(string emailUsuario, string novaSenha)
        {
            // Configurações do e-mail
            string remetente = "orbitrede@gmail.com";
            string senhaRemetente = "bwhf xkgy judj khrf";  // Senha do e-mail remetente

            // Configuração do servidor SMTP
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(remetente, senhaRemetente),
                EnableSsl = true,
                UseDefaultCredentials = false
            };

            try
            {
                // Construindo o e-mail
                string assunto = "Recuperação de Senha";
                string corpoEmail = $"Olá,\n\nSua nova senha é: {novaSenha}\n\n" +
                                    "Por favor, acesse o sistema com esta senha e altere-a na primeira oportunidade.\n\n" +
                                    "Se você não solicitou a redefinição de senha, ignore este e-mail.";

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(remetente),
                    Subject = assunto,
                    Body = corpoEmail,
                    IsBodyHtml = true,
                    SubjectEncoding = Encoding.UTF8,
                    BodyEncoding = Encoding.UTF8,
                };

                mailMessage.To.Add(emailUsuario);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar o e-mail: " + ex.Message);
            }
        }

        private void AtualizarSenhaEmArquivo(string emailUsuario, string novaSenha)
        {
            string filePath = "senhas.txt";

            // Lê todas as linhas do arquivo e as armazena em uma lista
            List<string> linhas = File.Exists(filePath) ? File.ReadAllLines(filePath).ToList() : new List<string>();

            // Atualiza ou adiciona a senha do usuário na lista
            bool usuarioExistente = false;
            for (int i = 0; i < linhas.Count; i++)
            {
                if (linhas[i].StartsWith(emailUsuario + ":"))
                {
                    linhas[i] = $"{emailUsuario}:{novaSenha}"; // Atualiza a senha
                    usuarioExistente = true;
                    break;
                }
            }

            if (!usuarioExistente)
            {
                linhas.Add($"{emailUsuario}:{novaSenha}"); // Adiciona um novo usuário
            }

            // Escreve a lista de volta no arquivo
            File.WriteAllLines(filePath, linhas);
        }
        private bool ValidarCredenciais(string emailUsuario, string senha)
        {
            string filePath = "senhas.txt";

            // Verifica se o arquivo existe
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Arquivo de senhas não encontrado.");
                return false;
            }

            // Lê todas as linhas e verifica se alguma delas corresponde ao email e senha
            var linhas = File.ReadAllLines(filePath);
            foreach (var linha in linhas)
            {
                var partes = linha.Split(':');
                if (partes.Length == 2 && partes[0] == emailUsuario && partes[1] == senha)
                {
                    return true; // Credenciais corretas
                }
            }

            return false; // Credenciais incorretas
        }
       



    }
}

