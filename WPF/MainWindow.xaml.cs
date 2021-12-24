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
using System.Windows.Navigation;
using System.Windows.Shapes;
using bib;

namespace WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Level level = Level.Beginner;
        Game myGame;
        public MainWindow()
        {
            InitializeComponent();
        }



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            myGame = new Game(level);
            int x = 0;
            myCanvas.Children.Clear();

            for (int i = 0; i < myGame.Grid.GetLength(0); i++)
            {
                int y = 0;
               
                for (int j = y; j < myGame.Grid.GetLength(1); j++)
                {
                    Button bt = new Button();
                    bt.Click += Bt_Click;
                    bt.Height = 495 / myGame.Grid.GetLength(0) ;
                    bt.Width = 600/ myGame.Grid.GetLength(1);

                    Canvas.SetTop(bt, x);
                    Canvas.SetLeft(bt, y);
                    bt.Tag = myGame.Grid[i, j];
                    myCanvas.Children.Add(bt);
                    y = y + 600 / myGame.Grid.GetLength(1);
                }
                x = x + 495 / myGame.Grid.GetLength(0);
            }
            //myCanvas.Children.Add(bt);

        }

        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int c = (int)b.Tag;
            if (c == 9) 
            {
                foreach(Button item in myCanvas.Children)
                {
                    item.Content = item.Tag.ToString();
                }
                MessageBox.Show("Game Over !");
            }
        }

        private void beginner_Checked(object sender, RoutedEventArgs e)
        {
            level = Level.Beginner;
        }

        private void intermediare_Checked(object sender, RoutedEventArgs e)
        {
            level = Level.Intermediate;
        }

        private void advanceds_Checked(object sender, RoutedEventArgs e)
        {
            level = Level.Advanced;
        }
    }
}
