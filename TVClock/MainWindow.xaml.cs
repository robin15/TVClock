using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TransparentClockApp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer, timer2;
        private bool fOnWindowFirst = false;
        private DateTime time_prev;
        public MainWindow()
        {
            InitializeComponent();
            // タイマーを設定して1秒ごとに時刻を更新
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            this.MouseEnter += (sebder, e) => {

                if ((Keyboard.Modifiers & ModifierKeys.Alt) != ModifierKeys.Alt)
                {
                    time_prev = DateTime.Now;
                    if (!fOnWindowFirst)
                    {
                        fOnWindowFirst = true;
                        timer.Stop();
                        this.Hide();
                        timer2 = new DispatcherTimer();
                        timer2.Interval = TimeSpan.FromSeconds(0.2);
                        timer2.Tick += Timer_Tick2;
                        timer2.Start();
                    }
                }
            };
            this.MouseLeftButtonDown += (sender, e) => {
                TimeTextBlock.Foreground = new SolidColorBrush(Colors.Gray);
                this.DragMove();
            };
            
            this.MouseLeftButtonUp += (sender, e) => {
                TimeTextBlock.Foreground = new SolidColorBrush(Colors.White);
            };

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string currentTime = DateTime.Now.ToString("HH:mm");
            TimeTextBlock.Text = currentTime;
        }
        private void Timer_Tick2(object sender, EventArgs e)
        {
            TimeSpan difference = DateTime.Now - time_prev;
            if (difference.Seconds > 1.5)
            {
                fOnWindowFirst = false;
                timer.Start();
                timer2.Stop();
                this.Show();
            }
        }
    }
}