using System;
using System.Collections.Generic;
using VSHackathonTimer.Common;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// 基本ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234237 を参照してください

namespace VSHackathonTimer
{
    /// <summary>
    /// 多くのアプリケーションに共通の特性を指定する基本ページ。
    /// </summary>
    public sealed partial class CountDownPage
    {
        VsCountDown _gDateTime;
        DateTime _gStartDateTime;
        DispatcherTimer _gTimer;

        public CountDownPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// このページには、移動中に渡されるコンテンツを設定します。前のセッションからページを
        /// 再作成する場合は、保存状態も指定されます。
        /// </summary>
        /// <param name="navigationParameter">このページが最初に要求されたときに
        /// <see cref="Frame.Navigate(Type, Object)"/> に渡されたパラメーター値。
        /// </param>
        /// <param name="pageState">前のセッションでこのページによって保存された状態の
        /// ディクショナリ。ページに初めてアクセスするとき、状態は null になります。</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            _gDateTime = (VsCountDown)navigationParameter;

            pageTitle.Text = _gDateTime.Title;

            _gTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(1)};
            _gTimer.Tick += timer_Tick;
            _gTimer.Stop();

            _gStartDateTime = _gDateTime.DateTimeTime;
            TimerText.Text = _gDateTime.StringTime;
        }

        /// <summary>
        /// アプリケーションが中断される場合、またはページがナビゲーション キャッシュから破棄される場合、
        /// このページに関連付けられた状態を保存します。値は、
        /// <see cref="SuspensionManager.SessionState"/> のシリアル化の要件に準拠する必要があります。
        /// </summary>
        /// <param name="pageState">シリアル化可能な状態で作成される空のディクショナリ。</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void timer_Tick(object sender, object e)
        {
            _gDateTime.Counter();
            TimerText.Text = _gDateTime.StringTime;
            TimerText.Foreground = _gDateTime.Minus ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White);
            if (TimerText.Text == "00:00:00")
            {
                TimeOver.Play();
                StopButton.Visibility = Visibility.Visible;
            }
            if (TimerText.Text == DoraTextBox.Text)
            {
                Dora.Play();
            }
            
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (PauseButton.Content != null && PauseButton.Content.ToString() == "Pause")
            {
                PauseButton.Content = "Start";
                _gTimer.Stop();
                TimeOver.Stop();
            }
            else if (PauseButton.Content != null && PauseButton.Content.ToString() == "Start")
            {
                PauseButton.Content = "Pause";
                _gTimer.Start();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TimeOver.Stop();
            _gDateTime.SetTimer(_gStartDateTime);
            TimerText.Text = _gDateTime.StringTime;
            TimerText.Foreground = new SolidColorBrush(Colors.White);
            PauseButton.Content = "Pause";
            PauseButton_Click(sender, e);
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            TimeOver.Stop();
            StopButton.Visibility = Visibility.Collapsed;
        }
    }
}
