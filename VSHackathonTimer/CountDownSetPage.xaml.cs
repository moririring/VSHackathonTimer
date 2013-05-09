using System;
using System.Collections.Generic;
using VSHackathonTimer.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 基本ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234237 を参照してください

namespace VSHackathonTimer
{
    /// <summary>
    /// 多くのアプリケーションに共通の特性を指定する基本ページ。
    /// </summary>
    public sealed partial class CountDownSetPage
    {
        readonly VsCountDown _gVsCountDown = new VsCountDown(); 
        public CountDownSetPage()
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
            if (pageState != null && pageState.ContainsKey("IntTimes"))
            {
                //TitleTextBox.Text = pageState["Title"].ToString();
                
                var dt = (DateTime)pageState["IntTimes"];
                _gVsCountDown.SetTimer(dt);
                TimerTextBox.Text = _gVsCountDown.StringTime;

            }
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (roamingSettings.Values.ContainsKey("Title"))
            {
                //TitleTextBox.Text = pageState["Title"].ToString();

                TitleTextBox.Text = roamingSettings.Values["Title"].ToString();

            }
        }

        /// <summary>
        /// アプリケーションが中断される場合、またはページがナビゲーション キャッシュから破棄される場合、
        /// このページに関連付けられた状態を保存します。値は、
        /// <see cref="SuspensionManager.SessionState"/> のシリアル化の要件に準拠する必要があります。
        /// </summary>
        /// <param name="pageState">シリアル化可能な状態で作成される空のディクショナリ。</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            //pageState["Title"] = TitleTextBox.Text;
            pageState["IntTimes"] = _gVsCountDown.DateTimeTime;

            //TimerTextBox.Text = gVSCountDown.StringTime;

        }

        private void TimerButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            var pm = VsCountDown.PlusMinus.Plus;
            var digit = VsCountDown.Digit.DigitS01;

            if (btn == null) return;
            if (btn.Name == "S01PButton") pm = VsCountDown.PlusMinus.Plus;
            if (btn.Name == "S10PButton") pm = VsCountDown.PlusMinus.Plus;
            if (btn.Name == "M01PButton") pm = VsCountDown.PlusMinus.Plus;
            if (btn.Name == "M10PButton") pm = VsCountDown.PlusMinus.Plus;
            if (btn.Name == "H01PButton") pm = VsCountDown.PlusMinus.Plus;
            if (btn.Name == "H10PButton") pm = VsCountDown.PlusMinus.Plus;

            if (btn.Name == "S01MButton") pm = VsCountDown.PlusMinus.Minus;
            if (btn.Name == "S10MButton") pm = VsCountDown.PlusMinus.Minus;
            if (btn.Name == "M01MButton") pm = VsCountDown.PlusMinus.Minus;
            if (btn.Name == "M10MButton") pm = VsCountDown.PlusMinus.Minus;
            if (btn.Name == "H01MButton") pm = VsCountDown.PlusMinus.Minus;
            if (btn.Name == "H10MButton") pm = VsCountDown.PlusMinus.Minus;

            if (btn.Name == "S01PButton") digit = VsCountDown.Digit.DigitS01;
            if (btn.Name == "S10PButton") digit = VsCountDown.Digit.DigitS10;
            if (btn.Name == "M01PButton") digit = VsCountDown.Digit.DigitM01;
            if (btn.Name == "M10PButton") digit = VsCountDown.Digit.DigitM10;
            if (btn.Name == "H01PButton") digit = VsCountDown.Digit.DigitH01;
            if (btn.Name == "H10PButton") digit = VsCountDown.Digit.DigitH10;

            if (btn.Name == "S01MButton") digit = VsCountDown.Digit.DigitS01;
            if (btn.Name == "S10MButton") digit = VsCountDown.Digit.DigitS10;
            if (btn.Name == "M01MButton") digit = VsCountDown.Digit.DigitM01;
            if (btn.Name == "M10MButton") digit = VsCountDown.Digit.DigitM10;
            if (btn.Name == "H01MButton") digit = VsCountDown.Digit.DigitH01;
            if (btn.Name == "H10MButton") digit = VsCountDown.Digit.DigitH10;

            _gVsCountDown.SetTimer(pm, digit);
            TimerTextBox.Text = _gVsCountDown.StringTime;
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _gVsCountDown.Clear();
            TimerTextBox.Text = _gVsCountDown.StringTime;
        }
        private void M5Button_Click(object sender, RoutedEventArgs e)
        {
            _gVsCountDown.SetTimer(new DateTime(0).AddMinutes(5));
            TimerTextBox.Text = _gVsCountDown.StringTime;
        }

        private void M50Button_Click(object sender, RoutedEventArgs e)
        {
            _gVsCountDown.SetTimer(new DateTime(0).AddMinutes(50));
            TimerTextBox.Text = _gVsCountDown.StringTime;
        }

        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null && (btn.Content != null && btn.Content.ToString() == "CountDown"))
            {
                _gVsCountDown.UpDownTime = VsCountDown.UpDown.Down;
            }
            else if (btn != null && (btn.Content != null && (btn.Content.ToString() == "CountUp")))
            {
                _gVsCountDown.UpDownTime = VsCountDown.UpDown.Up;
            }
            if (TitleTextBox != null) _gVsCountDown.Title = TitleTextBox.Text;

            _gVsCountDown.SetStart();

            //
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (TitleTextBox != null) roamingSettings.Values["Title"] = TitleTextBox.Text;

            Frame.Navigate(typeof(CountDownPage), _gVsCountDown);
        }




    }
}
