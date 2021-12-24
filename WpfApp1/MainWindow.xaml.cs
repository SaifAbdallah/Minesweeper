using BIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Level level=Level.Beginner;
        Game myGame;
        SoundPlayer player;
        SoundPlayer player2;
        DispatcherTimer time;
        bool state;
        int flag = 0;
        int nb;
        int s;
        int min;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
          
            myGame = new Game(level);
            MyCanvas.Children.Clear();
            Flag.Visibility = Visibility.Visible;
            text1.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Visible;
            image.Visibility = Visibility.Visible;
            //MyCanvas.Children.Add(rectangle);
            //MyCanvas.Children.Add(text);
            //MyCanvas.Children.Add(timer);
            if (level == Level.Beginner)
            {
                flag = 10;
            }
            else if (level == Level.Intermediate)
            {
                flag = 20;
            }
            else if (level == Level.Advanced)
            {
                flag = 40;
            }
            state = true;
            if (time != null)
            {
                time.Stop();
            }
            min = 0;
            s = 0;
           
            timer.Content = "00:00";
            time = new DispatcherTimer();
            time.Interval = System.TimeSpan.FromSeconds(1);
            time.Tick += Timer_Progress;

            int h = 0;
            nb = 0;
            for (int i = 0; i < myGame.Grid.GetLength(0); i++)
            {
                int l = 0;
                for (int j = 0; j < myGame.Grid.GetLength(1); j++)
                {
                    Button bt = new Button();
                    bt.Click += Bt_Click;
                    bt.MouseRightButtonDown += Bt_MouseRightButtonDown;
                    bt.Height = 50;
                    bt.Width = 50;
                    Canvas.SetTop(bt, h);
                    Canvas.SetLeft(bt , l);
                    MyCanvas.Children.Add(bt);
                    bt.Tag = myGame.Grid[i, j];
                    l = l + 50;
                    if (myGame.Grid[i, j] == 1)
                    {
                        bt.Foreground = Brushes.Blue;
                        bt.FontSize = 20;
                        bt.FontWeight = FontWeights.Bold;
                    }
                    else if (myGame.Grid[i, j] == 2)
                    {
                        bt.Foreground = Brushes.Green;
                        bt.FontSize = 20;
                        bt.FontWeight = FontWeights.Bold;
                    }
                    else if (myGame.Grid[i, j] == 0)
                    {
                        bt.Foreground = Brushes.White;
                    }
                    else if (myGame.Grid[i, j] >= 3 || myGame.Grid[i, j] <= 7)
                    {
                        bt.Foreground = Brushes.Red;
                        bt.FontSize = 20;
                        bt.FontWeight = FontWeights.Bold;
                    }
                    if (myGame.Grid[i, j] == 9)
                    {
                        bt.Background = Brushes.Red;
                    }

                }
                h = h + 50;
            }

        }
        private void Timer_Progress(object sender, System.EventArgs e)
        {
            s += 1;
            if (s == 60)
            {
                s = 0;
                min++;
            }
            timer.Content = min.ToString("00") + ":" + s.ToString("00");
            if (level == Level.Beginner && min == 1)
            {
                Bomb_Checked();
            }
            else if (level == Level.Intermediate && min == 3)
            {
                Bomb_Checked();
            }
            else if (level == Level.Advanced && min == 5)
            {
                Bomb_Checked();
            }
        }
        private void zeros_reveal(Button bt)
        {
            var i = Canvas.GetTop(bt) / bt.Height;
            var j = Canvas.GetLeft(bt) / bt.Width;
            for (int x = (int)i - 1; x <= i + 1; x++)
            {
                for (int y = (int)j - 1; y <= j + 1; y++)
                {
                    foreach (Button item in MyCanvas.Children)
                    {
                        bt.Background = Brushes.White;
                        if (Canvas.GetTop(item) / item.Height == x && Canvas.GetLeft(item) / item.Width == y && item.Content == null)
                        {
                            
                            item.Content = item.Tag;
                            nb++;
                            if (myGame.Grid[x, y] == 0)
                            {
                                zeros_reveal(item);
                            }
                        }
                    }

                }
            }
        }
        private void Bomb_Checked()
        {
            state = false;
            player2 = new SoundPlayer(Properties.Resources.Funny_Laugh_SOUND_EFFECT);
            player2.Play();
            time.Stop();
            MessageBox.Show("Time over - you lost");
            foreach (Button item in MyCanvas.Children)
            {
                if (item.Tag.ToString() == "9")
                {
                    StackPanel stk = new StackPanel();
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("bomb.png", UriKind.Relative));
                    stk.Orientation = Orientation.Horizontal;
                    stk.Children.Add(img);
                    item.Content = stk;
                    item.Click -= Bt_Click;
                    item.Background = Brushes.Red;
                }
                else
                {
                    item.Content = item.Tag.ToString();
                    item.Click -= Bt_Click;
                }
                if (item.Tag.ToString() == "0")
                {
                    item.Foreground = Brushes.White;
                    item.Background = Brushes.White;
                }
            }
        }
        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            if (state == true)
            {
            time.Start();
            Button b = sender as Button;
            int c = (int)b.Tag;
            if (c == 9)
            {
                    Bomb_Checked();
            }
            else if (c == 0)
            {
                zeros_reveal(b);
            }
            else
            {
                nb++;
                b.Content = b.Tag.ToString();
            }
            win_Checked(nb);

                //if (b == MouseRightButtonDown)
                //{
                //Button b = sender as Button;
                //StackPanel stk = new StackPanel();
                //Image img = new Image();
                //img.Source = new BitmapImage(new Uri("viper.jpg", UriKind.Relative));
                //stk.Orientation = Orientation.Horizontal;
                //stk.Children.Add(img);
                //b.Content = stk;
                //b.Click -= Bt2_Click;
                //}
            }
        }
        private void Bt_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (state == true)
            {
                Button b = (Button)sender;
                Image img = new Image();
                StackPanel stk = new StackPanel();
                img.Source = new BitmapImage(new Uri("viper.jpg", UriKind.Relative));
                stk.Orientation = Orientation.Horizontal;
                stk.Children.Add(img);
                if (level == Level.Beginner)
                {
                    if (flag > 0)
                    {
                        if (b.Content == null && b.Content != stk)
                        {
                            b.Content = stk;
                            flag -= 1;
                        }
                        else if (b.Background != Brushes.White && flag < 10)
                        {
                            b.Content = null;
                            flag += 1;
                        }
                    }

                }
                else if (level == Level.Intermediate)
                {
                    if (flag > 0)
                    {
                        if (b.Content == null && b.Content != stk && b.Content != b.Tag)
                        {
                            b.Content = stk;
                            flag -= 1;
                        }
                        else if (b.Background != Brushes.White && flag < 10)
                        {
                            b.Content = null;
                            flag += 1;
                        }
                    }
                }
                else if (level == Level.Advanced)
                {
                    if (flag > 0)
                    {
                        if (b.Content == null && b.Content != stk && b.Content != b.Tag)
                        {
                            b.Content = stk;
                            flag -= 1;
                        }
                        else if (b.Background != Brushes.White && flag < 10)
                        {
                            b.Content = null;
                            flag += 1;
                        }
                    }
                }
                Flag.Content = flag;
            }
        }
        private void win_Checked(int nb)
        {
            int x = myGame.Grid.GetLength(0) * myGame.Grid.GetLength(1);
            int y;
            if (level== Level.Beginner)
            {
                y = x - 10;
            }
            else if (level == Level.Intermediate)
            {
                y = x - 20;
            }
            else
            {
                y = x - 40;
            }
            if (nb == y)
            {
                time.Stop();
                state = false;
                player2 = new SoundPlayer(Properties.Resources.Queen___We_Are_The_Champions);
                player2.Play();
                MessageBox.Show("Victory - You took " + timer.Content);
            }
        }
        private void Beginner_Checked(object sender, RoutedEventArgs e)
        {
            level = Level.Beginner;
        }

        private void Intermediate_Checked(object sender, RoutedEventArgs e)
        {
            level = Level.Intermediate;
        }

        private void Advanced_Checked(object sender, RoutedEventArgs e)
        {
            level = Level.Advanced;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void stop_music(object sender, RoutedEventArgs e)
        {
            player = new SoundPlayer(Properties.Resources.J__Cole___White_Tiger__Mix_);
            player.Stop();
        }

        private void play_music(object sender, RoutedEventArgs e)
        {
            player = new SoundPlayer(Properties.Resources.J__Cole___White_Tiger__Mix_);
            player.Play();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("-In beginner level you have 1 minute to colpmete the game\n-In intermediate level you have 3 minute to colpmete the game\n-In advanced level you have 5 minute to colpmete the game\n\nwhen you click at any button and a number appears , that means that there is a bomb in the counter of it and if it doesn't that means that the button you clicked is either a zero(0) or a bomb.\n-If the button is 0, it will help you to reveal more buttons\n-If it's a bomb than you're gonna loose\n\nGood luck ^^");
        }
    }
}
