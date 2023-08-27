using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace TransparentClockApp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            // タイマーを設定して1秒ごとに時刻を更新
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            this.MouseEnter += (sebder, e) => {
                TimeTextBlock.Text = "";
                timer.Stop();
            };
            this.MouseLeave += (sebder, e) => {
                TimeTextBlock.Foreground = new SolidColorBrush(Colors.White);
                TimeTextBlock.Text = DateTime.Now.ToString("HH:mm");
                timer.Start();
            };
            this.MouseLeftButtonDown += (sender, e) => {
                TimeTextBlock.Foreground = new SolidColorBrush(Colors.DimGray);
                TimeTextBlock.Text = DateTime.Now.ToString("HH:mm");
                this.DragMove();
            };
            this.MouseLeftButtonUp += (sender, e) => {
                TimeTextBlock.Foreground = new SolidColorBrush(Colors.White);
            };

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 現在の時刻を取得してテキストに反映
            string currentTime = DateTime.Now.ToString("HH:mm");
            TimeTextBlock.Text = currentTime;
        }
    }
}