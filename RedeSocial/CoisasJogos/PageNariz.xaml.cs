using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RedeSocial.Models
{
    /// <summary>
    /// Interação lógica para PageNariz.xam
    /// </summary>
    public partial class PageNariz : Page
    {
        DispatcherTimer _timer;

        NarizManager narizManager = new NarizManager();

        int userCod;

        string projectPath;

        double upgrade1;
        double upgrade2;
        double upgrade3;
        double upgrade4;
        double upgrade5;
        double upgrade6;

        double upgradeAux;
        double upgradeAux2;
        int upgradeLvAux;

        int upgradeMultiplicador;

        Image acessorio;
        public PageNariz(int codUsuario)
        {
            InitializeComponent();

            //Configuração inicial
            userCod = codUsuario;

            projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName; //Pega o caminho do projeto
            projectPath = projectPath.Remove(projectPath.Length - 4);

            upgradeMultiplicador = 1;

            upgrade1 = 10 + narizManager.GetUpgrade1(userCod) * 10;
            upgrade2 = 20 + narizManager.GetUpgrade2(userCod) * 15;
            upgrade3 = 200 + narizManager.GetUpgrade3(userCod) * 20;
            upgrade4 = 500 + narizManager.GetUpgrade4(userCod) * 25;
            upgrade5 = 1000 + narizManager.GetUpgrade4(userCod) * 100;
            upgrade6 = 10000 + narizManager.GetUpgrade4(userCod) * 1000;

            labelNpC.Content = $"Nariz por Clique: {narizManager.GetNarizPorClique(userCod)}";
            labelNpS.Content = $"Nariz por Segundo: {narizManager.GetNarizPorSegundo(userCod)}";
            labelNariz.Content = narizManager.GetNariz(userCod).ToString("F0", CultureInfo.InvariantCulture);
            atualizarPrecoTodos();

            //Timer do Nariz por Segundo
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(0.1);
            _timer.Start();
            _timer.Tick += Timer_Tick;

            if (narizManager.GetAcessorio(userCod) == "moustache")
            {
                adicionarMustache();
            }
        }

        //Somar nariz por segundo
        private void Timer_Tick(object sender, EventArgs e)
        {
            narizManager.SetNariz(userCod, narizManager.GetNariz(userCod) + narizManager.GetNarizPorSegundo(userCod) / 10);
            labelNariz.Content = narizManager.GetNariz(userCod).ToString("F0", CultureInfo.InvariantCulture);

            atualizarPrecoTodos();
        }

        //Clique no nariz
        private void botaoNariz_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            narizManager.SetNariz(userCod, narizManager.GetNariz(userCod) + narizManager.GetNarizPorClique(userCod));
            labelNariz.Content = narizManager.GetNariz(userCod).ToString("F0", CultureInfo.InvariantCulture);

            //Diminuir o tamanho do nariz quando clicar
            botaoNariz.Width = 130;
            botaoNariz.Height = 130;

            if (acessorio != null)
            {
                acessorio.Width = 250;
                acessorio.Height = 250;
            }

            //Chamando a função de mostrar +
            mostrarMais();
        }

        #region Upgrades
        //Upgrade 1 (+1 por clique)
        private void botaoUpgrade1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < upgradeMultiplicador; i++)
            {
                if (narizManager.GetNariz(userCod) >= upgrade1)
                {
                    narizManager.SetNariz(userCod, narizManager.GetNariz(userCod) - upgrade1);
                    upgrade1 += 10;
                    narizManager.Upgrade1LvUp(userCod);
                    narizManager.SetNarizPorClique(userCod, narizManager.GetNarizPorClique(userCod) + 1);
                    labelUpgrade1Lv.Text = $"{narizManager.GetUpgrade1(userCod)}";
                    labelNariz.Content = narizManager.GetNariz(userCod).ToString("F0", CultureInfo.InvariantCulture);
                    labelNpC.Content = $"Nariz por Clique: {narizManager.GetNarizPorClique(userCod)}";
                }
                else
                {
                    break;
                }
            }

            atualizarPrecoTodos();
        }

        //Upgrade 2 (+1 por segundo)
        private void botaoUpgrade2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < upgradeMultiplicador; i++)
            {
                if (narizManager.GetNariz(userCod) >= upgrade2)
                {
                    narizManager.SetNariz(userCod, narizManager.GetNariz(userCod) - upgrade2);
                    upgrade2 += 15;
                    narizManager.Upgrade2LvUp(userCod);
                    narizManager.SetNarizPorSegundo(userCod, narizManager.GetNarizPorSegundo(userCod) + 1);
                    labelUpgrade2Lv.Text = $"{narizManager.GetUpgrade2(userCod)}";
                    labelNariz.Content = narizManager.GetNariz(userCod).ToString("F0", CultureInfo.InvariantCulture);
                    labelNpS.Content = $"Nariz por Segundo: {narizManager.GetNarizPorSegundo(userCod)}";
                }
                else
                {
                    break;
                }
            }

            atualizarPrecoTodos();
        }

        //Upgrade 3 (+20 por clique)
        private void botaoUpgrade3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < upgradeMultiplicador; i++)
            {
                if (narizManager.GetNariz(userCod) >= upgrade3)
                {
                    narizManager.SetNariz(userCod, narizManager.GetNariz(userCod) - upgrade3);
                    upgrade3 += 20;
                    narizManager.Upgrade3LvUp(userCod);
                    narizManager.SetNarizPorClique(userCod, narizManager.GetNarizPorClique(userCod) + 20);
                    labelUpgrade3Lv.Text = $"{narizManager.GetUpgrade3(userCod)}";
                    labelNariz.Content = narizManager.GetNariz(userCod).ToString("F0", CultureInfo.InvariantCulture);
                    labelNpC.Content = $"Nariz por Clique: {narizManager.GetNarizPorClique(userCod)}";
                }
                else
                {
                    break;
                }
            }

            atualizarPrecoTodos();
        }

        //Upgrade 4 (+25 por segundo)
        private void botaoUpgrade4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < upgradeMultiplicador; i++)
            {
                if (narizManager.GetNariz(userCod) >= upgrade4)
                {
                    narizManager.SetNariz(userCod, narizManager.GetNariz(userCod) - upgrade4);
                    upgrade4 += 25;
                    narizManager.Upgrade4LvUp(userCod);
                    narizManager.SetNarizPorSegundo(userCod, narizManager.GetNarizPorSegundo(userCod) + 25);
                    labelUpgrade4Lv.Text = $"{narizManager.GetUpgrade4(userCod)}";
                    labelNariz.Content = narizManager.GetNariz(userCod).ToString("F0", CultureInfo.InvariantCulture);
                    labelNpS.Content = $"Nariz por Segundo: {narizManager.GetNarizPorSegundo(userCod)}";

                    if (narizManager.GetAcessorio(userCod) != "moustache")
                    {
                        adicionarMustache();
                    }
                }
                else
                {
                    break;
                }
            }

            atualizarPrecoTodos();
        }

        //Upgrade 5 (+50 por clique)
        private void botaoUpgrade5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < upgradeMultiplicador; i++)
            {
                if (narizManager.GetNariz(userCod) >= upgrade5)
                {
                    narizManager.SetNariz(userCod, narizManager.GetNariz(userCod) - upgrade5);
                    upgrade5 += 100;
                    narizManager.Upgrade5LvUp(userCod);
                    narizManager.SetNarizPorClique(userCod, narizManager.GetNarizPorClique(userCod) + 50);
                    labelUpgrade5Lv.Text = $"{narizManager.GetUpgrade5(userCod)}";
                    labelNariz.Content = narizManager.GetNariz(userCod).ToString("F0", CultureInfo.InvariantCulture);
                    labelNpC.Content = $"Nariz por Clique: {narizManager.GetNarizPorClique(userCod)}";
                }
                else
                {
                    break;
                }
            }

            atualizarPrecoTodos();
        }

        private void botaoUpgrade6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < upgradeMultiplicador; i++)
            {
                if (narizManager.GetNariz(userCod) >= upgrade6)
                {
                    narizManager.SetNariz(userCod, narizManager.GetNariz(userCod) - upgrade6);
                    upgrade6 += 1000;
                    narizManager.Upgrade6LvUp(userCod);
                    narizManager.SetNarizPorSegundo(userCod, narizManager.GetNarizPorSegundo(userCod) + 100);
                    labelUpgrade6Lv.Text = $"{narizManager.GetUpgrade6(userCod)}";
                    labelNariz.Content = narizManager.GetNariz(userCod).ToString("F0", CultureInfo.InvariantCulture);
                    labelNpS.Content = $"Nariz por Segundo: {narizManager.GetNarizPorSegundo(userCod)}";
                }
                else
                {
                    break;
                }
            }

            atualizarPrecoTodos();
        }
        #endregion

        //Mostra animação +N
        private void mostrarMais()
        {
            //Cria a label
            Label labelMais = new Label();
            labelMais.Content = $"+{narizManager.GetNarizPorClique(userCod)}";
            labelMais.FontSize = 18;
            labelMais.IsHitTestVisible = false; //Deixa a label não clicável, permitindo clicar no botão que está atrás
            labelMais.HorizontalAlignment = HorizontalAlignment.Center;
            labelMais.VerticalAlignment = VerticalAlignment.Center;
            grid1.Children.Add(labelMais);

            //Movimento
            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.From = 50;
            heightAnimation.To = 250;
            heightAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            labelMais.BeginAnimation(Label.HeightProperty, heightAnimation);

            //Opacidade
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.From = 1.0;
            opacityAnimation.To = 0;
            opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            labelMais.BeginAnimation(Label.OpacityProperty, opacityAnimation);
        }

        //Adiciona a imagem de pelos
        private void adicionarMustache()
        {
            acessorio = new Image()
            {
                Width = 250,
                Height = 250,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 120, 0, 0),
                IsHitTestVisible = false,
                Source = new BitmapImage(new Uri(projectPath + "\\Imagens\\Moustache.png"))
            };
            grid1.Children.Add(acessorio);

            narizManager.SetAcessorio(userCod, "moustache");
        }

        #region Multiplicador
        private void botaox1_Click(object sender, RoutedEventArgs e)
        {
            upgradeMultiplicador = 1;
            alterarCorBotoesMultiplicador();
            atualizarPrecoTodos();
        }

        private void botaox10_Click(object sender, RoutedEventArgs e)
        {
            upgradeMultiplicador = 10;
            alterarCorBotoesMultiplicador();
            atualizarPrecoTodos();
        }

        private void botaox100_Click(object sender, RoutedEventArgs e)
        {
            upgradeMultiplicador = 100;
            alterarCorBotoesMultiplicador();
            atualizarPrecoTodos();
        }

        private void atualizarPrecoTodos()
        {
            atualizarPrecoUpgrade1();
            atualizarPrecoUpgrade2();
            atualizarPrecoUpgrade3();
            atualizarPrecoUpgrade4();
            atualizarPrecoUpgrade5();
            atualizarPrecoUpgrade6();
        }

        private void atualizarPrecoUpgrade1()
        {
            upgradeAux = upgradeAux2 = upgrade1;
            upgradeLvAux = 1;

            for (int i = 1; i < upgradeMultiplicador; i++)
            {
                if (narizManager.GetNariz(userCod) >= upgradeAux + upgradeAux2 + 10)
                {
                    upgradeAux2 += 10;
                    upgradeAux += upgradeAux2;
                    upgradeLvAux++;
                }
            }
            labelUpgrade1Price.Text = $"x{upgradeLvAux} {upgradeAux}";
        }

        private void atualizarPrecoUpgrade2()
        {
            upgradeAux = upgradeAux2 = upgrade2;
            upgradeLvAux = 1;

            for (int i = 1; i < upgradeMultiplicador; i++)
            {
                if (narizManager.GetNariz(userCod) >= upgradeAux + upgradeAux2 + 15)
                {
                    upgradeAux2 += 15;
                    upgradeAux += upgradeAux2;
                    upgradeLvAux++;
                }
            }
            labelUpgrade2Price.Text = $"x{upgradeLvAux} {upgradeAux}";
        }

        private void atualizarPrecoUpgrade3()
        {
            upgradeAux = upgradeAux2 = upgrade3;
            upgradeLvAux = 1;

            for (int i = 1; i < upgradeMultiplicador; i++)
            {
                if (narizManager.GetNariz(userCod) >= upgradeAux + upgradeAux2 + 20)
                {
                    upgradeAux2 += 20;
                    upgradeAux += upgradeAux2;
                    upgradeLvAux++;
                }
            }
            labelUpgrade3Price.Text = $"x{upgradeLvAux} {upgradeAux}";
        }

        private void atualizarPrecoUpgrade4()
        {
            upgradeAux = upgradeAux2 = upgrade4;
            upgradeLvAux = 1;

            for (int i = 1; i < upgradeMultiplicador; i++)
            {
                if (narizManager.GetNariz(userCod) >= upgradeAux + upgradeAux2 + 25)
                {
                    upgradeAux2 += 25;
                    upgradeAux += upgradeAux2;
                    upgradeLvAux++;
                }
            }
            labelUpgrade4Price.Text = $"x{upgradeLvAux} {upgradeAux}";
        }

        private void atualizarPrecoUpgrade5()
        {
            upgradeAux = upgradeAux2 = upgrade5;
            upgradeLvAux = 1;

            for (int i = 1; i < upgradeMultiplicador; i++)
            {
                if (narizManager.GetNariz(userCod) >= upgradeAux + upgradeAux2 + 100)
                {
                    upgradeAux2 += 100;
                    upgradeAux += upgradeAux2;
                    upgradeLvAux++;
                }
            }
            labelUpgrade5Price.Text = $"x{upgradeLvAux} {upgradeAux}";
        }

        private void atualizarPrecoUpgrade6()
        {
            upgradeAux = upgradeAux2 = upgrade6;
            upgradeLvAux = 1;

            for (int i = 1; i < upgradeMultiplicador; i++)
            {
                if (narizManager.GetNariz(userCod) >= upgradeAux + upgradeAux2 + 1000)
                {
                    upgradeAux2 += 1000;
                    upgradeAux += upgradeAux2;
                    upgradeLvAux++;
                }
            }
            labelUpgrade6Price.Text = $"x{upgradeLvAux} {upgradeAux}";
        }

        private void alterarCorBotoesMultiplicador()
        {
            botaox1.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
            botaox10.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
            botaox100.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
            botaox1.Foreground = new SolidColorBrush(Color.FromRgb(237, 237, 237));
            botaox10.Foreground = new SolidColorBrush(Color.FromRgb(237, 237, 237));
            botaox100.Foreground = new SolidColorBrush(Color.FromRgb(237, 237, 237));

            if (upgradeMultiplicador == 1)
            {
                botaox1.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                botaox1.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            else if (upgradeMultiplicador == 10)
            {
                botaox10.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                botaox10.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            else if (upgradeMultiplicador == 100)
            {
                botaox100.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                botaox100.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }
        #endregion

        #region MouseEnterLeave
        private void botaoNariz_MouseEnter(object sender, MouseEventArgs e)
        {
            botaoNariz.Width = 135;
            botaoNariz.Height = 135;

            if (acessorio != null)
            {
                acessorio.Width = 255;
                acessorio.Height = 255;
            }
        }

        private void botaoNariz_MouseLeave(object sender, MouseEventArgs e)
        {
            botaoNariz.Width = 130;
            botaoNariz.Height = 130;

            if (acessorio != null)
            {
                acessorio.Width = 250;
                acessorio.Height = 250;
            }
        }

        private void botaoNariz_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            botaoNariz.Width = 135;
            botaoNariz.Height = 135;

            if (acessorio != null)
            {
                acessorio.Width = 255;
                acessorio.Height = 255;
            }
        }
        private void botaoUpgrade1_MouseEnter(object sender, MouseEventArgs e)
        {
            botaoUpgrade1.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void botaoUpgrade1_MouseLeave(object sender, MouseEventArgs e)
        {
            botaoUpgrade1.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        }

        private void botaoUpgrade2_MouseEnter(object sender, MouseEventArgs e)
        {
            botaoUpgrade2.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void botaoUpgrade2_MouseLeave(object sender, MouseEventArgs e)
        {
            botaoUpgrade2.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        }

        private void botaoUpgrade3_MouseEnter(object sender, MouseEventArgs e)
        {
            botaoUpgrade3.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void botaoUpgrade3_MouseLeave(object sender, MouseEventArgs e)
        {
            botaoUpgrade3.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        }

        private void botaoUpgrade4_MouseEnter(object sender, MouseEventArgs e)
        {
            botaoUpgrade4.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void botaoUpgrade4_MouseLeave(object sender, MouseEventArgs e)
        {
            botaoUpgrade4.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        }

        private void botaoUpgrade5_MouseEnter(object sender, MouseEventArgs e)
        {
            botaoUpgrade5.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void botaoUpgrade5_MouseLeave(object sender, MouseEventArgs e)
        {
            botaoUpgrade5.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        }

        private void botaoUpgrade6_MouseEnter(object sender, MouseEventArgs e)
        {
            botaoUpgrade6.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void botaoUpgrade6_MouseLeave(object sender, MouseEventArgs e)
        {
            botaoUpgrade6.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        }
        #endregion
    }
}
