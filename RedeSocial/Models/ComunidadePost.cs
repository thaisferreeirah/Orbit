using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Models
{
    public class ComunidadePost
    {
        public int Comunidade {  get; set; }
        public int Remetente { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Midia { get; set; }
        public string Data { get; set; }
        public ArrayList Like { get; set; } = new ArrayList();
        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }

    public class ComunidadePostManager
    {
        private static List<ComunidadePost> comunidadePosts = new List<ComunidadePost>();

        public void ArmazenarPost(int comunidade, int remetente, string titulo, string texto, string midia, string data)
        {
            ComunidadePost novoComunidadePost = new ComunidadePost()
            {
                Comunidade = comunidade,
                Remetente = remetente,
                Titulo = titulo,
                Texto = texto,
                Midia = midia,
                Data = data
            };

            comunidadePosts.Add(novoComunidadePost);
        }

        public Boolean VerificarComunidade(int i, int comunidade)
        {
            if (comunidadePosts[i].Comunidade == comunidade)
            {
                return true;
            }
            return false;
        }

        public int BuscarRemetente(int i)
        {
            return comunidadePosts[i].Remetente;
        }

        public string BuscarTitulo(int i)
        {
            return comunidadePosts[i].Titulo;
        }

        public string BuscarTexto(int i)
        {
            return comunidadePosts[i].Texto;
        }

        public string BuscarMidia(int i)
        {
            return comunidadePosts[i].Midia;
        }

        public string BuscarData(int i)
        {
            return comunidadePosts[i].Data;
        }

        public int BuscarQuantidade()
        {
            return comunidadePosts.Count;
        }

        public void AdicionarLike(int i, int codUsuario)
        {
            comunidadePosts[i].Like.Add(codUsuario);
        }

        public void RemoverLike(int i, int codUsuario)
        {
            comunidadePosts[i].Like.Remove(codUsuario);
        }

        public int BuscarQuantidadeLike(int i)
        {
            return comunidadePosts[i].Like.Count;
        }

        public Boolean VerificarUsuarioLike(int i, int codUsuario) //Verifica se o usuário logado já deu like ou não
        {
            if (comunidadePosts[i].Like.Contains(codUsuario))
            {
                return true;
            }
            return false;
        }

        public void ExcluirPost(int i)
        {
            comunidadePosts.RemoveAt(i);
        }

        public void AdicionarComentario(int i, int codUsuario, string comentario)
        {
            comunidadePosts[i].Comentarios.Add(new Comentario(codUsuario, comentario));
        }

        public int BuscarQuantidadeComentario(int i)
        {
            return comunidadePosts[i].Comentarios.Count;
        }

        public string BuscarComentario(int i, int codComentario)
        {
            return comunidadePosts[i].Comentarios.ElementAt(codComentario).Conteudo;
        }

        public int BuscarUsuarioComentario(int i, int codComentario)
        {
            return comunidadePosts[i].Comentarios.ElementAt(codComentario).Remetente;
        }
    }
}
