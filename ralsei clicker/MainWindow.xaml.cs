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

namespace ralsei_clicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void UpdateText() // function to update all text and to check if ralsei is pet enough
        {
            Amount.Content = x;
            Level.Content = "LVL: " + lvl;
            secretcount.Content = "pet: " + pet;
            ShopItem1.Content = "+1 per click for $" + ShopItem1Price;
            PerClick.Content = SI2Bought == 1 ? "Per Click: " + y + " (x2)" : "Per Click: " + y;
            Progress.Value = x;
            Progress.Maximum = requirement;
            if (x < Progress.Minimum)
            {
                Progress.Minimum = x;
            }
            if (pet >= 100) { ralsei.Source = new BitmapImage(new Uri(@"/oh.png", UriKind.Relative)); }
            if (pet >= 200) 
            { 
                ralsei.Source = new BitmapImage(new Uri(@"/ralsei.png", UriKind.Relative));
                pet = 0;
            }
        }
        public double x; //  money variable
        public int pet; // shh secret
        public int y = 1; // every click add this to x
        public double requirement = 50; // requirement to reach next level
        public int lvl; // level
        public int SI2Bought = 0; // stands for Store Item 2 Bought (which is the x2 upgrade)
        Point position = Mouse.GetPosition(displayArea);
        private void RalseiClicked(object sender, MouseButtonEventArgs e)
        {
            Point position = Mouse.GetPosition(displayArea);
            if (position.Y < 175)
            {
                // on his head (cute)
                if (x >= requirement - 1)
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"/lvlup.wav");
                    player.Play();
                    ralsei.Height *= 1.02;
                    x += y;
                    pet++; // add pet
                    requirement *= 2;
                    if (SI2Bought == 1) { lvl += 2; }
                    else { lvl += 1; }
                    Progress.Minimum = x;
                    UpdateText();
                }
                else
                {
                    ralsei.Height *= 1.02;
                    if (SI2Bought == 1)
                    {
                        x += y * 2;
                        pet++; // add pet (not affected by x2 upgrade because its a secret value)
                    }
                    else
                    {
                        x += y;
                        pet++; // add pet
                    }
                    UpdateText();
                }
                if (lvl >= 1)
                {
                    ShopItem1.Opacity = 100;
                }
                if (lvl >= 8)
                {
                    ShopItem2.Opacity = 100;
                }
            }
            else
            {
                // not on his head (what are you doing)
                if (x >= requirement - 1)
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"/lvlup.wav");
                    player.Play();
                    ralsei.Height *= 1.02;
                    x++;
                    requirement *= 2;
                    if (SI2Bought == 1) { lvl += 2; }
                    else { lvl += 1; }
                    Progress.Minimum = x;
                    UpdateText();
                }
                else
                {
                    ralsei.Height *= 1.02;
                    if (SI2Bought == 1)
                    {
                        x += y * 2;
                    }
                    else
                    {
                        x += y;
                    }
                    UpdateText();
                }
                if (lvl >= 1)
                {
                    ShopItem1.Opacity = 100;
                }
                if (lvl >= 8)
                {
                    ShopItem2.Opacity = 100;
                }
            }
        }

        private void EduardoClick(object sender, RoutedEventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"/eduardo.wav");
            player.Play();
            _ = MessageBox.Show("well well well");
            player.Stop();
        }

        private void RalseiDown(object sender, MouseButtonEventArgs e)
        {
            ralsei.Height /= 1.02;
        }

        public double ShopItem1Price = 25;
        private void ShopItem1Click(object sender, RoutedEventArgs e)
        {
            if (x >= ShopItem1Price && SI2Bought == 0)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"locker.wav");
                player.Play();
                x -= ShopItem1Price;
                ShopItem1Price *= 1.3;
                ShopItem1Price = Math.Round(ShopItem1Price);
                y++;
                UpdateText();
            }
            else if (x >= ShopItem1Price && SI2Bought == 1)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"locker.wav");
                player.Play();
                x -= ShopItem1Price;
                ShopItem1Price *= 1.3;
                ShopItem1Price = Math.Round(ShopItem1Price);
                y += 2;
                UpdateText();
            }
        }

        public int ShopItem2Price = 2000;
        private static IInputElement displayArea;

        private void ShopItem2Click(object sender, RoutedEventArgs e)
        {
            if (x >= ShopItem2Price && SI2Bought == 0)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"/locker.wav");
                player.Play();
                x -= ShopItem2Price;
                SI2Bought = 1;
                ShopItem2.Content = "BOUGHT";
                UpdateText();
            }
        }
    }
}

