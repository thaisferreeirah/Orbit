using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedeSocial.Models;

namespace RedeSocial
{
    public class Post
    {
        public int Remetente { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Midia { get; set; }
        public string Data { get; set; }
        public ArrayList Like { get; set; } = new ArrayList();
        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public ArrayList Recomendador { get; set; } = new ArrayList();
    }

    public class PostManager
    {
        private static List<Post> posts = new List<Post>();

        public void ArmazenarPost(int remetente, string titulo, string texto, string midia, string data)
        {
            Post novoPost = new Post()
            {
                Remetente = remetente,
                Titulo = titulo,
                Texto = texto,
                Midia = midia,
                Data = data
            };

            posts.Add(novoPost);
        }

        public Boolean VerificarPostProprio(int i, int remetente)
        {
            if (posts[i].Remetente == remetente)
            {
                return true;
            }
            return false;
        }

        public int BuscarRemetente(int i)
        {
            return posts[i].Remetente;
        }

        public string BuscarTitulo(int i)
        {
            return posts[i].Titulo;
        }

        public string BuscarTexto(int i)
        {
            return posts[i].Texto;
        }

        public string BuscarMidia(int i)
        {
            return posts[i].Midia;
        }

        public string BuscarData(int i)
        {
            return posts[i].Data;
        }

        public int BuscarQuantidade()
        {
            return posts.Count;
        }

        public void AdicionarLike(int i, int codUsuario)
        {
            posts[i].Like.Add(codUsuario);
        }

        public void RemoverLike(int i, int codUsuario)
        {
            posts[i].Like.Remove(codUsuario);
        }

        public int BuscarQuantidadeLike(int i)
        {
            return posts[i].Like.Count;
        }

        public Boolean VerificarUsuarioLike(int i, int codUsuario) //Verifica se o usuário logado já deu like ou não
        {
            if (posts[i].Like.Contains(codUsuario))
            {
                return true;
            }
            return false;
        }

        public void ExcluirPost(int i)
        {
            posts.RemoveAt(i);
        }

        public void AdicionarComentario(int i, int codUsuario, string comentario)
        {
            posts[i].Comentarios.Add(new Comentario(codUsuario, comentario));
        }

        public int BuscarQuantidadeComentario(int i)
        {
            return posts[i].Comentarios.Count;
        }

        public string BuscarComentario(int i, int codComentario)
        {
            return posts[i].Comentarios.ElementAt(codComentario).Conteudo;
        }

        public int BuscarUsuarioComentario(int i, int codComentario)
        {
            return posts[i].Comentarios.ElementAt(codComentario).Remetente;
        }

        public void AdicionarRecomendador(int i, int codUsuario)
        {
            posts[i].Recomendador.Add(codUsuario);
        }

        public void RemoverRecomendador(int i, int codUsuario)
        {
            posts[i].Recomendador.Remove(codUsuario);
        }

        public int BuscarRecomendador(int i, int j)
        {
            return (int)posts[i].Recomendador[j];
        }

        public ArrayList BuscarListaRecomendador(int i)
        {
            return posts[i].Recomendador;
        }

        public bool VerificarRecomendador(int i, int codUsuario)
        {
            return posts[i].Recomendador.Contains(codUsuario);
        }
    }
}
