﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 基本ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234237 を参照してください

namespace VSHackathonTimer
{
    /// <summary>
    /// 多くのアプリケーションに共通の特性を指定する基本ページ。
    /// </summary>
    public sealed partial class CountDownPage : VSHackathonTimer.Common.LayoutAwarePage
    {
        DateTime gDateTime;
        DateTime gStartDateTime;
        DispatcherTimer gTimer = null;

        public CountDownPage()
        {
            this.InitializeComponent();
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
            gStartDateTime = gDateTime = (DateTime)navigationParameter;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gTimer = new DispatcherTimer();
            gTimer.Interval = TimeSpan.FromSeconds(1);
            gTimer.Tick += timer_Tick;
            gTimer.Start();
        }
        private void timer_Tick(object sender, object e)
        {
            if (gDateTime.ToString("HH:mm:ss") != "00:00:00")
            {
                gDateTime = gDateTime.AddSeconds(-1);
                TimerTextBox.Text = gDateTime.ToString("HH:mm:ss");
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (PauseButton.Content.ToString() == "Pause")
            {
                PauseButton.Content = "Start";
                gTimer.Stop();
            }
            else if (PauseButton.Content.ToString() == "Start")
            {
                PauseButton.Content = "Pause";
                gTimer.Start();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            gDateTime = gStartDateTime;
            TimerTextBox.Text = gDateTime.ToString("HH:mm:ss");
        }
    }
}
