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
using Xceed.Wpf.Toolkit;

namespace WPFTimer
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

        private void ButStartStop_Click(object sender, RoutedEventArgs e)
        {
            MasCurTime.IsEnabled = false;

            ButStartStop.Content = "Pause";

            // Replace underscores with 0
            MasCurTime.Text = MasCurTime.Text.Replace("_", "0");

            // Split MasCurTime into hours, minuites, and seconds 
            string[] hms = MasCurTime.Text.Split(':');

            // Create a timespan using hms[] for hours, minutes, and seconds
            TimeSpan timeRemaining = new TimeSpan(Int32.Parse(hms[0]), Int32.Parse(hms[1]), Int32.Parse(hms[2]));

            // Make a timer that runs every second
            // This is not super accurate and there is a pause but it'll do until I can find something better
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            void timer_Tick(object se, EventArgs c)
            {
                {
                    if (timeRemaining >= TimeSpan.Zero)
                    {
                        MasCurTime.Text = timeRemaining.ToString();
                        timeRemaining = timeRemaining.Subtract(TimeSpan.FromSeconds(1));
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("timer complete!");
                        timer.Stop();
                        MasCurTime.IsEnabled = true;
                        ButStartStop.Content = "Start";
                    }
                }
            }
        }
    }
}
