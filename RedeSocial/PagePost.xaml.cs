using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using RedeSocial.CoisasInicio;
using RedeSocial.Models;

namespace RedeSocial
{
    /// <summary>
    /// Interação lógica para PagePost.xam
    /// </summary>

    //TODO: talvez trocar os botões por um combobox
    public partial class PagePost : Page
    {
        private PostManager postManager = new PostManager();
        private UserManager usuarioManager = new UserManager();

        private Home mainWindow;
        Frame mainFrame;

        int codUsuario;
        string enderecoMidia;
        string exibicaoPost;
        string projectPath; //Caminho até o projeto para poder adicionar fotos e ícones em outros computadores

        //Cores
        SolidColorBrush corFundo;
        SolidColorBrush corPrincipal;
        SolidColorBrush corSecundaria;
        SolidColorBrush corPlano;
        SolidColorBrush corLinha;
        private ComunidadeManager comunidadeManager;

        public PagePost(int codUser, Home _mainWin, Frame _mainFrame, ComunidadeManager comunidadeManager)
        {
            InitializeComponent();

            this.comunidadeManager = comunidadeManager;

            codUsuario = codUser;
            exibicaoPost = "proprio";
            enderecoMidia = "";

            mainWindow = _mainWin;
            mainFrame = _mainFrame;

            projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

            corFundo = new SolidColorBrush(Color.FromRgb(240, 240, 250));
            corPrincipal = new SolidColorBrush(Color.FromRgb(55, 55, 110));
            corSecundaria = new SolidColorBrush(Color.FromRgb(75, 75, 130));
            corPlano = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            corLinha = new SolidColorBrush(Color.FromRgb(200, 200, 200));

            frameAmigos.Navigate(new PageAmizadesInicio(codUsuario, mainFrame, mainWindow));
            frameComunidades.Navigate(new PageComunidadesInicio(codUsuario, mainFrame, mainWindow, comunidadeManager));

            atualizarPagina(exibicaoPost);
            exibirFotoPerfil();
        }

        //Define qual função de atualizarPaginaPost vai ser executado
        public void atualizarPagina(string exibicaoPost)
        {
            if (exibicaoPost == "proprio")
            {
                atualizarPaginaPostProprio();
                alterarCorBotaoPost(botaoPostProprio);
            }
            else if (exibicaoPost == "amigos")
            {
                atualizarPaginaPostAmigos();
                alterarCorBotaoPost(botaoPostAmigos);
            }
            else
            {
                atualizarPaginaPostGeral();
                alterarCorBotaoPost(botaoPostGeral);
            }
        }

        //Coloca todos os posts do próprio usuário na página
        public void atualizarPaginaPostProprio()
        {
            gridPosts.Children.Clear();

            for (int i = postManager.BuscarQuantidade() - 1; i >= 0; i--)
            {
                if (postManager.VerificarPostProprio(i, codUsuario))
                {
                    publicarPost(i);
                }
            }
        }

        //Coloca todos os posts de todos na página
        public void atualizarPaginaPostAmigos()
        {
            gridPosts.Children.Clear();

            for (int i = postManager.BuscarQuantidade() - 1; i >= 0; i--)
            {
                if (usuarioManager.VerificarCodAmigo(codUsuario, postManager.BuscarRemetente(i))
                    || postManager.BuscarListaRecomendador(i).Cast<object>().Any(item => usuarioManager.BuscarListaAmigos(codUsuario).Contains(item)))
                {
                    publicarPost(i);
                }
            }
        }

        //Coloca todos os posts de todos na página
        public void atualizarPaginaPostGeral()
        {
            gridPosts.Children.Clear();

            for (int i = postManager.BuscarQuantidade() - 1; i >= 0; i--)
            {
                publicarPost(i);
            }
        }

        //Insere o post na página
        public void publicarPost(int i)
        {
            //Cria o grid do corpo do post
            Grid gridPostCorpo = new Grid();
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());//Autor do post
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());//Título
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());//Texto
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());//Mídia
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());//Quantidade de Likes, comentários e recomendações
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());//Botão Curtir e Recomendar

            //Cria uma nova row no gridMensagens
            gridPosts.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            //Cria a grid do autor
            Grid gridAutor = new Grid();
            gridAutor.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            gridAutor.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            gridAutor.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            gridAutor.ColumnDefinitions.Add(new ColumnDefinition());
            gridAutor.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            //Grid para os botões Curtir, Comentar e Recomendar
            Grid gridBotoes = new Grid();
            gridBotoes.ColumnDefinitions.Add(new ColumnDefinition());//Botão curtir
            gridBotoes.ColumnDefinitions.Add(new ColumnDefinition());//Botão comentar
            gridBotoes.ColumnDefinitions.Add(new ColumnDefinition());//Botão recomendar
            //Borda do gridBotoes
            Border borderBotoes = new Border()
            {
                BorderBrush = corLinha,
                BorderThickness = new Thickness(0, 1, 0, 0),
                Margin = new Thickness(10, 0, 10, 0)
            };
            borderBotoes.Child = gridBotoes;

            //Botão curtir 
            Grid gridCurtir = new Grid();
            gridCurtir.ColumnDefinitions.Add(new ColumnDefinition());//Ícone curtir
            gridCurtir.ColumnDefinitions.Add(new ColumnDefinition());//Quantidade de curtidas
            //Borda do botão curtir
            Border borderCurtir = new Border()
            {
                Background = corPlano,
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(5)
            };
            borderCurtir.Child = gridCurtir;
            //Ícone curtir
            Image newIconeCurtir = new Image()
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 20,
                Width = 20,
                Margin = new Thickness(5),
                Source = new BitmapImage(new Uri(projectPath + "\\Icones\\Like.png", UriKind.RelativeOrAbsolute))
            };
            //Quantidade de curtidas
            TextBlock newQuantidadeCurtida = new TextBlock()
            {
                Text = postManager.BuscarQuantidadeLike(i).ToString(),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14,
                Margin = new Thickness(10, 5, 5, 5),
                FontWeight = FontWeights.Bold
            };
            borderCurtir.MouseLeftButtonUp += (sender, e) => gridCurtir_Click(sender, e, i, borderCurtir, newIconeCurtir, newQuantidadeCurtida);
            borderCurtir.MouseEnter += (sender, e) => gridCurtir_MouseEnter(sender, e, i, borderCurtir);
            borderCurtir.MouseLeave += (sender, e) => gridCurtir_MouseLeave(sender, e, i, borderCurtir);
            Grid.SetColumn(newIconeCurtir, 0);
            Grid.SetColumn(newQuantidadeCurtida, 1);
            gridCurtir.Children.Add(newIconeCurtir);
            gridCurtir.Children.Add(newQuantidadeCurtida);

            //Botão comentar
            Grid gridComentar = new Grid();
            gridComentar.ColumnDefinitions.Add(new ColumnDefinition());//Ícone comentar
            gridComentar.ColumnDefinitions.Add(new ColumnDefinition());//Quantidade de comentário
            //Borda do botão comentar
            Border borderComentar = new Border()
            {
                Background = corPlano,
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(5)
            };
            borderComentar.Child = gridComentar;
            //Ícone comentar
            Image newIconeComentar = new Image()
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 20,
                Width = 20,
                Margin = new Thickness(5),
                Source = new BitmapImage(new Uri(projectPath + "\\Icones\\Comentario.png", UriKind.RelativeOrAbsolute))
            };
            //Quantidade de comentário
            TextBlock newQuantidadeComentario = new TextBlock()
            {
                Text = postManager.BuscarQuantidadeComentario(i).ToString(),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14,
                Margin = new Thickness(10, 5, 5, 5),
                FontWeight = FontWeights.Bold
            };

            borderComentar.MouseLeftButtonUp += (sender, e) => borderComentar_Click(sender, e, i, gridPostCorpo, borderBotoes, borderCurtir, newQuantidadeComentario);
            borderComentar.MouseEnter += (sender, e) => borderComentar_MouseEnter(sender, e, i, borderComentar);
            borderComentar.MouseLeave += (sender, e) => borderComentar_MouseLeave(sender, e, i, borderComentar);

            Grid.SetColumn(newIconeComentar, 0);
            Grid.SetColumn(newQuantidadeComentario, 1);
            gridComentar.Children.Add(newIconeComentar);
            gridComentar.Children.Add(newQuantidadeComentario);

            //Botão recomendar
            Grid gridRecomendar = new Grid();
            //Borda do botão recomendar
            Border borderRecomendar = new Border()
            {
                Background = corPlano,
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(5)
            };
            borderRecomendar.Child = gridRecomendar;
            //Texto recomendar
            TextBlock newRecomendar = new TextBlock()
            {
                Text = "Recomendar",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 14,
            };

            borderRecomendar.MouseLeftButtonUp += (sender, e) => borderRecomendar_Click(sender, e, i, newRecomendar);
            borderRecomendar.MouseEnter += (sender, e) => borderRecomendar_MouseEnter(sender, e, i, borderRecomendar);
            borderRecomendar.MouseLeave += (sender, e) => borderRecomendar_MouseLeave(sender, e, i, borderRecomendar);

            gridRecomendar.Children.Add(newRecomendar);

            //Cria a foto do autor
            Ellipse newAutorFoto = new Ellipse()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 45,
                Width = 45,
                Stroke = corPrincipal,
                Margin = new Thickness(10),
                Fill = new ImageBrush(new BitmapImage(new Uri(usuarioManager.BuscarFoto(postManager.BuscarRemetente(i)), UriKind.Relative)))
            };

            //Cria o nome do autor
            TextBlock newAutorNome = new TextBlock()
            {
                Text = usuarioManager.BuscarNome(postManager.BuscarRemetente(i)),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                FontSize = 14,
                FontFamily = (FontFamily)Application.Current.Resources["ArimoFont"],
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(5, 13, 0, 0)
            };

            //Cria a data e horário
            TextBlock newDataHora = new TextBlock()
            {
                Text = postManager.BuscarData(i),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(5, -5, 0, 0)
            };

            //Cria a lixeira
            Image newLixeira = new Image()
            {
                Height = 16,
                Width = 16,
                Margin = new Thickness(0, 0, 10, 0),
                Source = new BitmapImage(new Uri(projectPath + "\\Icones\\Lixeira.png", UriKind.RelativeOrAbsolute))
            };

            //Funções da lixeira
            newLixeira.MouseLeftButtonUp += (sender, e) => newLixeira_Click(sender, e, i);
            newLixeira.MouseEnter += (sender, e) => newLixeira_MouseEnter(sender, e, i, newLixeira);
            newLixeira.MouseLeave += (sender, e) => newLixeira_MouseLeave(sender, e, i, newLixeira);

            //Cria o titulo
            TextBlock newTitulo = new TextBlock()
            {
                Text = postManager.BuscarTitulo(i),
                TextWrapping = TextWrapping.Wrap,
                FontSize = 16,
                FontFamily = (FontFamily)Application.Current.Resources["ArimoFont"],
                Foreground = corPrincipal,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(20, 5, 20, 5)
            };

            //Cria o texto
            RichTextBox newTexto = new RichTextBox() { Style = (Style)FindResource("RichTextBoxConteudo") };
            newTexto.Document.Blocks.Clear();
            //Carrega o texto formatado do Xaml armazenado
            string textoFormatado = postManager.BuscarTexto(i);
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(textoFormatado)))
            {
                try
                {
                    TextRange range = new TextRange(newTexto.Document.ContentStart, newTexto.Document.ContentEnd);
                    range.Load(stream, DataFormats.Xaml);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar o texto formatado: " + ex.Message);
                }
            }

            //Cria a foto do post
            Image newMidia = new Image()
            {
                Source = new BitmapImage(new Uri(postManager.BuscarMidia(i), UriKind.RelativeOrAbsolute)),
                MaxHeight = 150,
                MaxWidth = 150,
                Margin = new Thickness(0, 0, 0, 10)
            };

            //Cria a sombra
            DropShadowEffect dropShadowEffect = new DropShadowEffect()
            {
                BlurRadius = 3,
                ShadowDepth = 0
            };
            //Cria a borda arredondada
            Border border = new Border()
            {
                Effect = dropShadowEffect,
                Background = corPlano,
                Margin = new Thickness(0, 10, 0, 0),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Child = gridPostCorpo
            };

            //Adiciona a borda no gridMensagens
            gridPosts.Children.Add(border);

            //Verifica se o post é do usuário logado para mostrar ou não o botão de excluir post
            if (!postManager.VerificarPostProprio(i, codUsuario))
            {
                newLixeira.Visibility = Visibility.Hidden;
            }

            //Adiciona a foto e nome na gridAutor
            Grid.SetRow(newAutorFoto, 0);
            Grid.SetRowSpan(newAutorFoto, 2);
            Grid.SetColumn(newAutorFoto, 0);
            Grid.SetRow(newAutorNome, 0);
            Grid.SetColumn(newAutorNome, 1);
            Grid.SetRow(newDataHora, 1);
            Grid.SetColumn(newDataHora, 1);
            Grid.SetRow(newLixeira, 0);
            Grid.SetColumn(newLixeira, 2);
            gridAutor.Children.Add(newAutorFoto);
            gridAutor.Children.Add(newAutorNome);
            gridAutor.Children.Add(newDataHora);
            gridAutor.Children.Add(newLixeira);

            //Adiciona os botões na gridBotoes
            Grid.SetColumn(borderCurtir, 0);
            Grid.SetColumn(borderComentar, 1);
            Grid.SetColumn(borderRecomendar, 2);
            gridBotoes.Children.Add(borderCurtir);
            gridBotoes.Children.Add(borderComentar);
            gridBotoes.Children.Add(borderRecomendar);

            //Verifica se tem conteúdo, se não tiver deixa o margin 0 para não criar espaço em branco
            if (String.IsNullOrEmpty(newTitulo.Text))
            {
                newTitulo.Visibility = Visibility.Collapsed;
            }
            if (String.IsNullOrEmpty(new TextRange(newTexto.Document.ContentStart, newTexto.Document.ContentEnd).Text.Trim()))
            {
                newTexto.Visibility = Visibility.Collapsed;
            }
            if (String.IsNullOrEmpty(postManager.BuscarMidia(i)))
            {
                newMidia.Margin = new Thickness(0);
            }

            //Adiciona tudo no gridPostCorpo
            Grid.SetRow(gridAutor, 0);
            Grid.SetRow(newTitulo, 1);
            Grid.SetRow(newTexto, 2);
            Grid.SetRow(newMidia, 3);
            Grid.SetRow(borderBotoes, 4);
            gridPostCorpo.Children.Add(gridAutor);
            gridPostCorpo.Children.Add(newTitulo);
            gridPostCorpo.Children.Add(newTexto);
            gridPostCorpo.Children.Add(newMidia);
            gridPostCorpo.Children.Add(borderBotoes);

            //Adiciona o post na tela
            Grid.SetRow(border, gridPosts.RowDefinitions.Count - 1);
            Grid.SetColumn(border, 0);

            //Altera a cor do botão do like se já deu like
            alterarCorBotaoLike(i, newIconeCurtir);

            //Altera texto do botão recomendar para Recomendado caso já tenha sido recomendado
            alterarTextoBotaoRecomendar(i, newRecomendar);

            //Adiciona nome do recomendador caso tenha sido recomendado por um amigo lindo
            adicionarRecomendadorNoPost(i, newAutorNome);
        }

        //
        private void adicionarRecomendadorNoPost(int i, TextBlock newAutorNome)
        {
            if (postManager.BuscarListaRecomendador(i).Cast<object>().Any(item => usuarioManager.BuscarListaAmigos(codUsuario).Contains(item)))
            {
                newAutorNome.Text += $" (recomendado por {usuarioManager.BuscarNome(postManager.BuscarRecomendador(i, 0))})";
            }
        }

        //Função do botão Curtir
        private void gridCurtir_Click(object sender, EventArgs e, int i, Border border, Image iconeCurtir, TextBlock quantidadeCurtida)
        {
            if (!postManager.VerificarUsuarioLike(i, codUsuario))
            {
                postManager.AdicionarLike(i, codUsuario);
                iconeCurtir.Source = new BitmapImage(new Uri(projectPath + "\\Icones\\LikePreenchido.png", UriKind.RelativeOrAbsolute));
                quantidadeCurtida.Text = postManager.BuscarQuantidadeLike(i).ToString();
            }
            else
            {
                postManager.RemoverLike(i, codUsuario);
                iconeCurtir.Source = new BitmapImage(new Uri(projectPath + "\\Icones\\Like.png", UriKind.RelativeOrAbsolute));
                quantidadeCurtida.Text = postManager.BuscarQuantidadeLike(i).ToString();
            }
        }

        //Altera a cor quando clica no like
        private void alterarCorBotaoLike(int i, Image iconeCurtir)
        {
            if (postManager.VerificarUsuarioLike(i, codUsuario))
            {
                iconeCurtir.Source = new BitmapImage(new Uri(projectPath + "\\Icones\\LikePreenchido.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                iconeCurtir.Source = new BitmapImage(new Uri(projectPath + "\\Icones\\Like.png", UriKind.RelativeOrAbsolute));
            }
        }

        //Altera texto do botão recomendar para Recomendado caso já tenha sido recomendado
        private void alterarTextoBotaoRecomendar(int i, TextBlock newRecomendar)
        {
            if (postManager.VerificarRecomendador(i, codUsuario))
            {
                newRecomendar.FontWeight = FontWeights.Bold;
                newRecomendar.Text = "Recomendado!";
            }
            else
            {
                newRecomendar.FontWeight = FontWeights.Normal;
                newRecomendar.Text = "Recomendar";
            }
        }

        //Altera cor quando passa o mouse no like
        private void gridCurtir_MouseEnter(object sender, EventArgs e, int i, Border border)
        {
            border.Background = corFundo;
        }

        //Altera a cor quando tira o mouse do like
        private void gridCurtir_MouseLeave(object sender, EventArgs e, int i, Border border)
        {
            border.Background = corPlano;
        }

        //Função do botão comentário. Abre o campo para comentar e mostra outros comentários.
        private void borderComentar_Click(Object sender, EventArgs e, int i, Grid gridPostCorpo, Border borderBotoes, Border borderCurtir, TextBlock newQuantidadeComentario)
        {
            if (gridPostCorpo.Children.Count < 6)
            {
                gridPostCorpo.RowDefinitions.Add(new RowDefinition());

                Grid gridFormComentario = new Grid();
                gridFormComentario.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                gridFormComentario.ColumnDefinitions.Add(new ColumnDefinition());
                gridFormComentario.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

                Ellipse newAutorFoto = new Ellipse()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 40,
                    Width = 40,
                    Stroke = Brushes.DarkGray,
                    Margin = new Thickness(15, 10, 0, 10),
                    Fill = new ImageBrush(new BitmapImage(new Uri(usuarioManager.BuscarFoto(codUsuario))))
                };

                Label newlabelComentario = new Label()
                {
                    Content = "Comentário . . .",
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 14,
                    Opacity = 0.4,
                    Margin = new Thickness(20, 0, 0, 0),
                    IsHitTestVisible = false
                };

                TextBox newCampoComentario = new TextBox()
                {
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(10),
                    Padding = new Thickness(10, 10, 30, 10),
                    Style = (Style)Application.Current.Resources["TextBoxPost"]
                };

                //Adicionar função click para enviar com enter
                newCampoComentario.TextChanged += (senderComentario, eComentario) => newCampoComentario_TextChanged(senderComentario, eComentario, i, newCampoComentario, newlabelComentario);

                Image newBotaoEnviarComentario = new Image()
                {
                    Source = new BitmapImage(new Uri(projectPath + "\\Icones\\Enviar.png", UriKind.RelativeOrAbsolute)),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Width = 25,
                    Height = 25,
                    Margin = new Thickness(0, 0, 15, 0)
                };

                newBotaoEnviarComentario.MouseLeftButtonUp += (senderComentario, eComentario) => newBotaoEnviarComentario_Click(senderComentario, eComentario, i, codUsuario, newCampoComentario, gridPostCorpo, newQuantidadeComentario);
                newBotaoEnviarComentario.MouseEnter += (senderComentario, eComentario) => newBotaoEnviarComentario_MouseEnter(senderComentario, eComentario, newBotaoEnviarComentario);
                newBotaoEnviarComentario.MouseLeave += (senderComentario, eComentario) => newBotaoEnviarComentario_MouseLeave(senderComentario, eComentario, newBotaoEnviarComentario);

                Grid.SetColumn(newAutorFoto, 0);
                Grid.SetColumn(newCampoComentario, 1);
                Grid.SetColumn(newlabelComentario, 1);
                Grid.SetColumn(newBotaoEnviarComentario, 1);
                gridFormComentario.Children.Add(newAutorFoto);
                gridFormComentario.Children.Add(newCampoComentario);
                gridFormComentario.Children.Add(newlabelComentario);
                gridFormComentario.Children.Add(newBotaoEnviarComentario);

                Grid.SetRow(gridFormComentario, 5);
                gridPostCorpo.Children.Add(gridFormComentario);

                newCampoComentario.Focus();

                //Alterações
                borderBotoes.BorderThickness = new Thickness(0, 1, 0, 1);

                int posicao = 6;
                for (int j = postManager.BuscarQuantidadeComentario(i) - 1; j >= 0; j--)
                {
                    criarComentario(i, gridPostCorpo, j, posicao);
                    posicao++;
                }
            }
            else
            {
                for (int j = gridPostCorpo.Children.Count; j > 5; j--)
                {
                    gridPostCorpo.Children.RemoveAt(gridPostCorpo.Children.Count - 1);
                }
                //Desalterações
                borderBotoes.BorderThickness = new Thickness(0, 1, 0, 0);
            }
        }

        public void criarComentario(int i, Grid gridPostCorpo, int j, int posicao)
        {
            gridPostCorpo.RowDefinitions.Add(new RowDefinition());

            Grid gridComentario = new Grid();
            gridComentario.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            gridComentario.ColumnDefinitions.Add(new ColumnDefinition());

            Grid gridConteudoComentario = new Grid();
            gridConteudoComentario.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            gridConteudoComentario.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            Ellipse newAutorFoto = new Ellipse()
            {
                Height = 40,
                Width = 40,
                Stroke = Brushes.DarkGray,
                Margin = new Thickness(15, 10, 5, 20),
                Fill = new ImageBrush(new BitmapImage(new Uri(usuarioManager.BuscarFoto(postManager.BuscarUsuarioComentario(i, j)))))
            };

            Border newBorderComentario = new Border()
            {
                Background = corFundo,
                CornerRadius = new CornerRadius(10),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Margin = new Thickness(5, 0, 10, 10),
                Child = gridConteudoComentario
            };

            TextBlock newAutorNome = new TextBlock()
            {
                Text = usuarioManager.BuscarNome(postManager.BuscarUsuarioComentario(i, j)),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(15, 10, 15, 0)
            };

            TextBlock newComentario = new TextBlock()
            {
                Text = postManager.BuscarComentario(i, j),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(15, 5, 15, 10)
            };

            Grid.SetRow(newAutorNome, 0);
            Grid.SetRow(newComentario, 1);
            gridConteudoComentario.Children.Add(newAutorNome);
            gridConteudoComentario.Children.Add(newComentario);

            Grid.SetColumn(newAutorFoto, 0);
            Grid.SetColumn(newBorderComentario, 1);
            gridComentario.Children.Add(newAutorFoto);
            gridComentario.Children.Add(newBorderComentario);

            Grid.SetRow(gridComentario, posicao);
            gridPostCorpo.Children.Add(gridComentario);

        }

        //Altera cor quando passa o mouse no Comentar
        private void borderComentar_MouseEnter(object sender, MouseEventArgs e, int i, Border borderComentar)
        {
            borderComentar.Background = corFundo;
        }

        //Altera cor quando tira o mouse do Comentar
        private void borderComentar_MouseLeave(object sender, MouseEventArgs e, int i, Border borderComentar)
        {
            borderComentar.Background = corPlano;
        }

        //Função do botão recomendar
        private void borderRecomendar_Click(object sender, EventArgs e, int i, TextBlock newRecomendar)
        {
            if (!postManager.VerificarRecomendador(i, codUsuario))
            {
                postManager.AdicionarRecomendador(i, codUsuario);
                newRecomendar.FontWeight = FontWeights.Bold;
                newRecomendar.Text = "Recomendado!";
            }
            else
            {
                postManager.RemoverRecomendador(i, codUsuario);
                newRecomendar.FontWeight = FontWeights.Normal;
                newRecomendar.Text = "Recomendar";
            }
        }

        //Altera cor quando passa o mouse no Recomendar
        private void borderRecomendar_MouseEnter(object sender, MouseEventArgs e, int i, Border borderRecomendar)
        {
            borderRecomendar.Background = corFundo;
        }

        //Altera cor quando tira o mouse do Recomendar
        private void borderRecomendar_MouseLeave(object sender, MouseEventArgs e, int i, Border borderRecomendar)
        {
            borderRecomendar.Background = corPlano;
        }

        //Apagar a label "Texto" quando digitar
        private void campoTitulo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(campoTitulo.Text))
            {
                labelTitulo.Visibility = Visibility.Visible;
            }
            else
            {
                labelTitulo.Visibility = Visibility.Hidden;
            }
        }

        //Apagar a label "Texto" quando digitar
        private void campoTexto_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextRange conteudo = new TextRange(campoTexto.Document.ContentStart, campoTexto.Document.ContentEnd); // Captura o texto no RichTextBox
            string texto = conteudo.Text.Trim();
            if (String.IsNullOrEmpty(texto))
            {
                labelTexto.Visibility = Visibility.Visible;
            }
            else
            {
                labelTexto.Visibility = Visibility.Hidden;
            }
        }

        //Excluir post
        private void newLixeira_Click(object sender, EventArgs e, int i)
        {
            MessageBoxResult result = MessageBox.Show("Deseja excluir o post?", "Excluir post", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                postManager.ExcluirPost(i);
                atualizarPagina(exibicaoPost);
            }
        }

        //Altera cor quando passa o mouse na Lixeira
        private void newLixeira_MouseEnter(object sender, MouseEventArgs e, int i, Image newLixeira)
        {
            newLixeira.Width = 18;
            newLixeira.Height = 18;
        }

        private void newLixeira_MouseLeave(object sender, MouseEventArgs e, int i, Image newLixeira)
        {
            newLixeira.Width = 16;
            newLixeira.Height = 16;
        }

        //Apaga ou mostra a label do campo comentário
        private void newCampoComentario_TextChanged(object sender, TextChangedEventArgs e, int i, TextBox newCampoComentario, Label labelComentario)
        {
            if (String.IsNullOrEmpty(newCampoComentario.Text))
            {
                labelComentario.Visibility = Visibility.Visible;
            }
            else
            {
                labelComentario.Visibility = Visibility.Hidden;
            }
        }

        private void newBotaoEnviarComentario_Click(object sender, EventArgs e, int i, int codUsuario, TextBox newCampoComentario, Grid gridPostCorpo, TextBlock newQuantidadeComentario)
        {
            if (!String.IsNullOrEmpty(newCampoComentario.Text))
            {
                postManager.AdicionarComentario(i, codUsuario, newCampoComentario.Text);
                newCampoComentario.Clear();

                for (int j = gridPostCorpo.Children.Count; j > 6; j--)
                {
                    gridPostCorpo.Children.RemoveAt(gridPostCorpo.Children.Count - 1);
                }

                int posicao = 6;
                for (int j = postManager.BuscarQuantidadeComentario(i) - 1; j >= 0; j--)
                {
                    criarComentario(i, gridPostCorpo, j, posicao);
                    posicao++;
                    newQuantidadeComentario.Text = postManager.BuscarQuantidadeComentario(i).ToString();
                }
            }
            else
            {
                MessageBox.Show("Escreva algum comentário.");
            }
        }

        private void newBotaoEnviarComentario_MouseEnter(object sender, MouseEventArgs e, Image newBotaoEnviarComentario)
        {
            newBotaoEnviarComentario.Width = 27;
            newBotaoEnviarComentario.Height = 27;
        }

        private void newBotaoEnviarComentario_MouseLeave(object sender, MouseEventArgs e, Image newBotaoEnviarComentario)
        {
            newBotaoEnviarComentario.Width = 25;
            newBotaoEnviarComentario.Height = 25;
        }

        //Botão para postar o post
        private void botaoPostar_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(campoTexto.Document.ContentStart, campoTexto.Document.ContentEnd);
            string textoFormatado;
            using (MemoryStream stream = new MemoryStream())
            {
                textRange.Save(stream, DataFormats.Xaml);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    textoFormatado = reader.ReadToEnd();
                }
            }
            bool isRichTextBoxEmpty = string.IsNullOrWhiteSpace(textRange.Text.Trim()) || textoFormatado.Trim().Equals("<FlowDocument xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" />");

            if (isRichTextBoxEmpty && string.IsNullOrEmpty(enderecoMidia))
            {
                MessageBox.Show("Escreva algum texto ou selecione uma imagem.");
            }
            else
            {
                postManager.ArmazenarPost(codUsuario, campoTitulo.Text, textoFormatado, enderecoMidia, DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

                campoTitulo.Clear();
                campoTexto.Document.Blocks.Clear();
                enderecoMidia = "";
                if (exibicaoPost == "amigos") exibicaoPost = "proprio";
                removerPrevia(); //Remove a prévia da foto após postar
                atualizarPagina(exibicaoPost);
            }
        }

        //Permite selecionar uma foto para a postagem
        private void botaoAdicionarFoto_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                removerPrevia();
                enderecoMidia = openFileDialog.FileName;
                previaFoto();
            }
        }

        //Mostra uma prévia da foto selecionada
        private void previaFoto()
        {
            Image newMidia = new Image()
            {
                Source = new BitmapImage(new Uri(enderecoMidia, UriKind.RelativeOrAbsolute)),
                MaxHeight = 150,
                MaxWidth = 150,
                Margin = new Thickness(0, 10, 0, 10)
            };

            Grid.SetRow(newMidia, 2);
            Grid.SetColumn(newMidia, 0);
            Grid.SetColumnSpan(newMidia, 4);
            gridFormPost.Children.Add(newMidia);

            adicionarBotaoExcluirFoto(newMidia);
        }

        //Remove a prévia da foto
        private void removerPrevia()
        {
            var elementsInRow = gridFormPost.Children.Cast<UIElement>().Where(n => Grid.GetRow(n) == 2).ToList();
            foreach (var element in elementsInRow)
            {
                gridFormPost.Children.Remove(element);
            }
        }

        private void adicionarBotaoExcluirFoto(Image newMidia)
        {
            Image iconeExcluirFoto = new Image()
            {
                Source = new BitmapImage(new Uri(projectPath + "\\Icones\\Fechar.png", UriKind.RelativeOrAbsolute)),
                MaxHeight = 30,
                MaxWidth = 30,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 5, 10, 0)
            };

            iconeExcluirFoto.MouseLeftButtonUp += (sender, e) => iconeExcluirFoto_Click(sender, e, newMidia);

            Grid.SetRow(iconeExcluirFoto, 2);
            Grid.SetColumn(iconeExcluirFoto, 3);
            gridFormPost.Children.Add(iconeExcluirFoto);
        }

        private void iconeExcluirFoto_Click(object sender, EventArgs e, Image newMidia)
        {
            enderecoMidia = "";
            removerPrevia();
        }

        //Mostrar postagens próprias
        private void botaoPostProprio_Click(object sender, RoutedEventArgs e)
        {
            exibicaoPost = "proprio";
            atualizarPagina(exibicaoPost);
        }

        //Mostrar postagens dos amigos
        private void botaoPostAmigos_Click(object sender, RoutedEventArgs e)
        {
            exibicaoPost = "amigos";
            atualizarPagina(exibicaoPost);
        }

        //Mostrar postagens de todos
        private void botaoPostGeral_Click(object sender, RoutedEventArgs e)
        {
            exibicaoPost = "geral";
            atualizarPagina(exibicaoPost);
        }

        //Altera a cor dos botões Seus, Amigos, Todos
        private void alterarCorBotaoPost(Button botaoPost)
        {
            botaoPostProprio.Background = corPrincipal;
            botaoPostAmigos.Background = corPrincipal;
            botaoPostGeral.Background = corPrincipal;
            botaoPostProprio.Foreground = corFundo;
            botaoPostAmigos.Foreground = corFundo;
            botaoPostGeral.Foreground = corFundo;

            botaoPost.Background = corPlano;
            botaoPost.Foreground = corPrincipal;
        }

        //Exibir a foto de perfil no formulário de post
        private void exibirFotoPerfil()
        {
            postFormFoto.Fill = new ImageBrush(new BitmapImage(new Uri(usuarioManager.BuscarFoto(codUsuario))));
        }

        private void botaoPostProprio_MouseEnter(object sender, MouseEventArgs e)
        {
            if (exibicaoPost != "proprio")
            {
                botaoPostProprio.Background = corSecundaria;
            }
        }

        private void botaoPostProprio_MouseLeave(object sender, MouseEventArgs e)
        {
            if (exibicaoPost != "proprio")
            {
                botaoPostProprio.Background = corPrincipal;
            }
        }

        private void botaoPostAmigos_MouseEnter(object sender, MouseEventArgs e)
        {
            if (exibicaoPost != "amigos")
            {
                botaoPostAmigos.Background = corSecundaria;
            }
        }

        private void botaoPostAmigos_MouseLeave(object sender, MouseEventArgs e)
        {
            if (exibicaoPost != "amigos")
            {
                botaoPostAmigos.Background = corPrincipal;
            }
        }

        private void botaoPostGeral_MouseEnter(object sender, MouseEventArgs e)
        {
            if (exibicaoPost != "geral")
            {
                botaoPostGeral.Background = corSecundaria;
            }
        }

        private void botaoPostGeral_MouseLeave(object sender, MouseEventArgs e)
        {
            if (exibicaoPost != "geral")
            {
                botaoPostGeral.Background = corPrincipal;
            }
        }

        private void botaoNegrito_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = campoTexto.Selection;
            if (!selectedText.IsEmpty)
            {
                if (selectedText.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold))
                {
                    selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                }
                else
                {
                    selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                }
            }
            campoTexto.Focus();
        }

        private void botaoItalico_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = campoTexto.Selection;
            if (!selectedText.IsEmpty)
            {
                if (selectedText.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic))
                {
                    selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                }
                else
                {
                    selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                }
            }
            campoTexto.Focus();
        }

        private void botaoSublinhado_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selectedText = campoTexto.Selection;
            if (!selectedText.IsEmpty)
            {
                TextDecorationCollection textDecorations = (TextDecorationCollection)selectedText.GetPropertyValue(Inline.TextDecorationsProperty);
                if (textDecorations == TextDecorations.Underline)
                {
                    selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }
                else
                {
                    selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                }
            }
            campoTexto.Focus();
        }
    }
}
