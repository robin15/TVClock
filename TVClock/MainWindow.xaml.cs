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
            //this.MouseEnter += (sebder, e) => { timer.Tick += null; this.Hide(); };
            //this.MouseLeave += (sebder, e) => { this.Show(); timer.Tick += Timer_Tick; };
            this.MouseLeftButtonDown += (sender, e) => { this.DragMove(); };
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 現在の時刻を取得してテキストに反映
            string currentTime = DateTime.Now.ToString("HH:mm");
            TimeTextBlock.Text = currentTime;
        }
    }
}