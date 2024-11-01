using RedeSocial.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace RedeSocial
{

    public class User
    {
        public required string Email { get; set; }
        public required string ID { get; set; }
        public required string PasswordHash { get; set; }
        public required string FullName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Foto { get; set; }
        public string Capa { get; set; }
        public ArrayList Amigos { get; set; } = new ArrayList();
        public ArrayList Solicitacoes { get; set; } = new ArrayList();
        public List<Notificacao> Notificacoes { get; set; } = new List<Notificacao>();
    }

    public class UserManager
    {   
        private const int AgeLimit = 16; // Limite de idade mínima

        // Dicionário para armazenar usuários e suas informações, indexados pelo Email e ID
        private static Dictionary<string, User> usersByEmail = new Dictionary<string, User>();
        private static Dictionary<string, User> usersByID = new Dictionary<string, User>();
        
        // Método para criar o hash da senha
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Verifica se a pessoa tem pelo menos 16 anos
        private bool IsValidAge(DateOnly birthDate)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age)) age--; // Ajusta a idade caso o aniversário não tenha ocorrido ainda este ano

            return age >= AgeLimit;
        }

        // Valida o formato do email
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Valida o formato da senha
        private bool IsValidPassword(string password)
        {
            return password.Length >= 6;
        }

        // Registrar um novo usuário
        public string Registrar(string email, string id, string password, string confirmPassword, string fullName, DateOnly birthDate)
        {
            // Valida o Email
            if (string.IsNullOrWhiteSpace(email))
            {
                return "O email não pode ser vazio.";
            }

            if (!IsValidEmail(email))
            {
                return "O email não é válido.";
            }

            if (usersByEmail.ContainsKey(email))
            {
                return "Email já cadastrado. Tente novamente.";
            }

            // Valida o ID
            if (string.IsNullOrWhiteSpace(id))
            {
                return "O ID não pode ser vazio.";
            }

            if (usersByID.ContainsKey(id))
            {
                return "ID já está em uso. Escolha outro.";
            }

            // Valida a Senha
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                return "Não é permitido espaço em branco na senha.";
            }

            if (!IsValidPassword(password))
            {
                return "A senha deve ter pelo menos 6 caracteres.";
            }

            if (password != confirmPassword)
            {
                return "As senhas não coincidem. Tente novamente.";
            }

            // Valida o Nome Completo
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return "O nome completo não pode ser vazio.";
            }

            // Valida a Idade
            if (!IsValidAge(birthDate))
            {
                return $"Você deve ter pelo menos {AgeLimit} anos para se registrar.";
            }

            // Cria o hash da senha
            string passwordHash = HashPassword(password);

            // Criar novo usuário
            User newUser = new User
            {
                Email = email,
                ID = id,
                PasswordHash = passwordHash,
                FullName = fullName,
                BirthDate = birthDate
            };

            // Armazenar o usuário tanto por Email quanto por ID
            usersByEmail.Add(email, newUser);
            usersByID.Add(id, newUser);

            return "Usuário registrado com sucesso!";
        }

        // Realizar login usando o Email ou o ID
        public string Logar(string login, string password)
        {
            User? user = null;

            // Verificações de entrada
            if (string.IsNullOrWhiteSpace(login))
            {
                return "Por favor, insira o email ou ID.";
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return "Por favor, insira a senha.";
            }

            // Verifica se o login é um email ou um ID
            if (usersByEmail.ContainsKey(login))
            {
                user = usersByEmail[login];
            }
            else if (usersByID.ContainsKey(login))
            {
                user = usersByID[login];
            }

            if (user != null)
            {
                string passwordHash = HashPassword(password);
                if (user.PasswordHash == passwordHash)
                {
                    return "Logado com sucesso!";
                }
                else
                {
                    return "Senha incorreta!";
                }
            }

            return "Usuário não encontrado!";
        }

        // Obter usuário pelo Email ou ID
        public User? ObterUsuario(string login)
        {
            if (usersByEmail.ContainsKey(login))
            {
                return usersByEmail[login];
            }
            else if (usersByID.ContainsKey(login))
            {
                return usersByID[login];
            }
            return null;
        }

        public void AdicionarFoto(int codUsuario, string foto)
        {
            usersByEmail.ElementAt(codUsuario).Value.Foto = foto;
        }

        //Adicionar capa
        public void AdicionarCapa(int codusuario, string capa)
        {
            usersByEmail.ElementAt(codusuario).Value.Capa = capa;
        }

        //Busca o código do usuário pelo id
        public int BuscarCodigoUsuario(string id)
        {
            return usersByID.Keys.ToList().IndexOf(id);
        }

        //Busca o nome de um usuário pelo código
        public string BuscarNome(int codUsuario)
        {
            return usersByEmail.ElementAt(codUsuario).Value.FullName;
        }
        //busca a quantidade de usuarios
        public int BuscarQuantidade()
        {
            return usersByEmail.Count;
        }

        //Busca a foto de um usuário pelo código
        public string BuscarFoto(int codUsuario)
        {
            return usersByEmail.ElementAt(codUsuario).Value.Foto;
        }

        //Buscar a capa de um usuário pelo código
        public string BuscarCapa(int codUsuario)
        {
            return usersByEmail.ElementAt(codUsuario).Value.Capa;
        }

        public void AdicionarAmigo(int codUsuario, int codAmigo)
        {
            usersByEmail.ElementAt(codUsuario).Value.Amigos.Add(codAmigo);
        }

        public int BuscarCodAmigo(int codUsuario, int i)
        {
            return (int)usersByEmail.ElementAt(codUsuario).Value.Amigos[i];
        }

        public int BuscarQuantidadeAmigos(int codUsuario)
        {
            return usersByEmail.ElementAt(codUsuario).Value.Amigos.Count;
        }

        public ArrayList BuscarListaAmigos(int codUsuario)
        {
            return usersByEmail.ElementAt(codUsuario).Value.Amigos;
        }
     

        public bool VerificarCodAmigo(int codUsuario, int codAmigo)
        {
            return usersByEmail.ElementAt(codUsuario).Value.Amigos.Contains(codAmigo);
        }

        public void AdicionarUsuario(string email, string id, string passwordHash, string fullNome, DateOnly birthDate, string foto, string capa)
        {
            User novoUsuario = new User()
            {
                Email = email,
                ID = id,
                PasswordHash = HashPassword(passwordHash),
                FullName = fullNome,
                Foto = foto,
                BirthDate = birthDate,
                Capa = capa,
            };

            usersByEmail.Add(email, novoUsuario);
            usersByID.Add(id, novoUsuario);
        }
        public void AdicionarSolicitacao(int remetente, int destinatario)
        {
            usersByEmail.ElementAt(destinatario).Value.Solicitacoes.Add(remetente);
            usersByEmail.ElementAt(destinatario).Value.Notificacoes.Add(new Notificacao(remetente, "solicitacao", BuscarCodSolicitacao(remetente, destinatario)));

        }
        public void AceitarSolicitacao(int remetente, int destinatario)
        {
            usersByEmail.ElementAt(remetente).Value.Solicitacoes.Remove(destinatario);
            AdicionarAmigo(remetente, destinatario);
            AdicionarAmigo(destinatario, remetente);
        }

        public void RecusarSolicitacao(int remetente, int destinatario)
        {
            usersByEmail.ElementAt(remetente).Value.Solicitacoes.Remove(destinatario);
        }

        public void CancelarSolicitacao(int remetente, int destinatario)
        {
            usersByEmail.ElementAt(remetente).Value.Solicitacoes.Remove(destinatario);
            //usersByEmail.ElementAt(destinatario).Value.Notificacoes.RemoveAt(BuscarCodigoNotificacao(destinatario, remetente));
        }
        public bool VerificarSolicitacao(int remetente, int destinatario)
        {
           return usersByEmail.ElementAt(destinatario).Value.Solicitacoes.Contains(remetente);
        }
        public int BuscarCodSolicitacao(int remetente, int destinatario)
        {
            return usersByEmail.ElementAt(destinatario).Value.Solicitacoes.IndexOf(remetente);
        }
        public void DesfazerAmizade(int codUser, int codAmigo)
        {
            usersByEmail.ElementAt(codUser).Value.Amigos.Remove(codAmigo);
            usersByEmail.ElementAt(codAmigo).Value.Amigos.Remove(codUser);
        }
        public void AlterarSenha(int codUser, string passwordHash)
        {
            usersByEmail.ElementAt(codUser).Value.PasswordHash = HashPassword(passwordHash);
        }

        public int BuscarCodigoUsuarioPorEmail(string email)
        {
            return usersByEmail.Keys.ToList().IndexOf(email);
        }

        #region NOTIFICAÇÂO
        public int BuscarQuantidadeNotificacao(int codUsuario)
        {
            return usersByEmail.ElementAt(codUsuario).Value.Notificacoes.Count;
        }

        public bool VerificarNotificacaoVerificacao(int codUsuario, int i)
        {
            return usersByEmail.ElementAt(codUsuario).Value.Notificacoes.ElementAt(i).Verificado;
        }

        public void AlterarNotificacaoVerificado(int codUsuario, int i)
        {
            usersByEmail.ElementAt(codUsuario).Value.Notificacoes.ElementAt(i).Verificado = true;
        }

        public int BuscarRemetenteNotificacao(int codUsuario, int i)
        {
            return usersByEmail.ElementAt(codUsuario).Value.Notificacoes.ElementAt(i).Remetente;
        }

        public int BuscarCodigoNotificacao(int codUsuario, int remetente)
        {
            return usersByEmail.ElementAt(codUsuario).Value.Notificacoes.IndexOf(new Notificacao(remetente, "amizade", BuscarCodSolicitacao(remetente, codUsuario)));
        }
        #endregion
    }
}
