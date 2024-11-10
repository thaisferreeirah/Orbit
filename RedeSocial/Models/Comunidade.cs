using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RedeSocial.Models
{
    public class Comunidade(int codigo, string nome, string foto, string descricao, int codUsuarioCriador)
    {
        public int Codigo { get; set; } = codigo;
        public int CodUsuarioCriador { get; set; } = codUsuarioCriador;
        public string Nome { get; set; } = nome;
        public string Foto { get; set; } = foto;
        public string Descricao { get; set; } = descricao;
        public List<int> Membros { get; private set; } = [];
        public int UserID;

        // Adiciona um usuário à comunidade usando o código do usuário (codUsuario)
        public void AdicionarMembro(int codUsuario)
        {
            if (!Membros.Contains(codUsuario))
            {
                Membros.Add(codUsuario);
            }
        }

        // Remove um usuário da comunidade
        public void RemoverMembro(int codUsuario)
        {
            Membros.Remove(codUsuario);
        }

        // Verifica se um usuário é membro da comunidade
        public bool VerificarMembro(int codUsuario)
        {
            return Membros.Contains(codUsuario);
        }
    }

    public class ComunidadeManager
    {
        private Dictionary<int, Comunidade> comunidades = new Dictionary<int, Comunidade>(); // Comunidades por código
        private Dictionary<int, List<int>> usuarioComunidades = new Dictionary<int, List<int>>(); // Associações de codUsuario às comunidades

        // Adiciona uma nova comunidade
        public void AdicionarComunidade(int codigo, string nome, string foto, string descricao, int codUsuarioCriador)
        {
            Comunidade novaComunidade = new Comunidade(codigo, nome, foto, descricao, codUsuarioCriador);
            //Comunidade.Add();
            comunidades[codigo] = novaComunidade;
        }

        // Associa um usuário a uma comunidade usando codUsuario
        public void AssociarUsuarioAComunidade(int codUsuario, int codComunidade)
        {
            if (comunidades.ContainsKey(codComunidade))
            {
                comunidades[codComunidade].AdicionarMembro(codUsuario);

                if (!usuarioComunidades.ContainsKey(codUsuario))
                {
                    usuarioComunidades[codUsuario] = new List<int>();
                }
                usuarioComunidades[codUsuario].Add(codComunidade);
            }
        }

        // Remove a associação de um usuário a uma comunidade
        public void RemoverUsuarioDaComunidade(int codUsuario, int codComunidade)
        {
            if (comunidades.ContainsKey(codComunidade))
            {
                comunidades[codComunidade].RemoverMembro(codUsuario);
                usuarioComunidades[codUsuario]?.Remove(codComunidade);
            }
        }

        // Busca o código das comunidades em que o usuário participa
        public List<int> BuscarComunidadesDoUsuario(int codUsuario)
        {
            if (usuarioComunidades.ContainsKey(codUsuario))
            {
                return usuarioComunidades[codUsuario];
            }
            return new List<int>();
        }

        // Obtém uma comunidade pelo código
        public Comunidade ObterComunidadePorCodigo(int codComunidade)
        {
            if (comunidades.TryGetValue(codComunidade, out var comunidade))
            {
                return comunidade;
            }
            //MessageBox.Show("Comunidade não encontrada. (Comunidade.cs)");
            return null; // Retorna null se a comunidade não for encontrada
        }

        public string BuscarNomeComunidade(int codComunidade)
        {
            if (comunidades.TryGetValue(codComunidade, out var comunidade))
            {
                return comunidade.Nome;
            }
            return "Comunidade não encontrada";
        }

        public string BuscarFotoComunidade(int codComunidade)
        {
            if (comunidades.TryGetValue(codComunidade, out var comunidade))
            {
                return comunidade.Foto;
            }
            return "Comunidade não encontrada";
        }

        public string BuscarDescricaoComunidade(int codComunidade)
        {
            if (comunidades.TryGetValue(codComunidade, out var comunidade))
            {
                return comunidade.Descricao;
            }
            return "Comunidade não encontrada";
        }

        public List<Comunidade> ObterTodasAsComunidades()
        {
            return comunidades.Values.ToList();
        }

        public List<ComunidadeViewModel> ObterTodasAsComunidadesParaExibicao()
        {
            return comunidades.Values.Select(comunidade => new ComunidadeViewModel
            {
                Nome = comunidade.Nome,
                Foto = comunidade.Foto
            }).ToList();
        }

        // Método para atualizar uma comunidade
        public bool AtualizarComunidade(int codigo, string novoNome, string novaDescricao)
        {
            if (comunidades.ContainsKey(codigo))
            {
                var comunidade = comunidades[codigo];
                comunidade.Nome = novoNome;
                comunidade.Descricao = novaDescricao;
                return true; // Sucesso na atualização
            }
            return false; // Comunidade não encontrada
        }

        // Método para excluir uma comunidade
        public bool ExcluirComunidade(int codigo)
        {
            if (comunidades.ContainsKey(codigo))
            {
                comunidades.Remove(codigo);
                return true; // Sucesso na exclusão
            }
            return false; // Comunidade não encontrada
        }

        public bool AtualizarImagemComunidade(int codComunidade, string novaImagem)
        {
            if (comunidades.ContainsKey(codComunidade))
            {
                comunidades[codComunidade].Foto = novaImagem;
                return true; // Imagem atualizada com sucesso
            }
            return false; // Comunidade não encontrada
        }



        public int CodigoUnico()
        {
            return new Random().Next(1000, 9999);
        }
    }

    public class ComunidadeViewModel
    {
        public string Nome { get; set; }
        public string Foto { get; set; }
    }
}