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
using System.Windows.Shapes;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for IntroGeneriek.xaml
    /// </summary>
    public partial class IntroGeneriek : Window
    {
        public IntroGeneriek()
        {
            InitializeComponent();

        }

        public Player ingelogdePlayerLoginscreen { get; set; }
      

        private void Intro_MediaEnded(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.ingelogdePlayerLoginscreen = ingelogdePlayerLoginscreen;
            this.Close();
            mainwindow.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Intro.Source = new Uri("../../Assets/IntroGeneriek Blokken.mp4", UriKind.Relative);
            Intro.Volume = 0.4;
            Intro.Play();
            Intro.MediaEnded += Intro_MediaEnded;
        }
    }
}
