using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
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
    public sealed partial class CountDownSetPage : VSHackathonTimer.Common.LayoutAwarePage
    {
        VSCountDown gVSCountDown = new VSCountDown(); 
        public CountDownSetPage()
        {
            this.InitializeComponent();
        }

        /*
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
            if (pageState != null && pageState.ContainsKey("Timer"))
            {
                //TitleTextBox.Text = pageState["Title"].ToString();
                
                DateTime dt = (DateTime)pageState["Timer"];
                gVSCountDown.SetTimer(dt);
                TimerTextBox.Text = gVSCountDown.StringTime;

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
            pageState["Timer"] = gVSCountDown.DateTimeTime;

            //TimerTextBox.Text = gVSCountDown.StringTime;

        }
        */
        private void TimerButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            VSCountDown.PlusMinus pm = VSCountDown.PlusMinus.Plus;
            VSCountDown.Digit digit = VSCountDown.Digit.Digit_s01;

            if (btn.Name == "S01PButton") pm = VSCountDown.PlusMinus.Plus;
            if (btn.Name == "S10PButton") pm = VSCountDown.PlusMinus.Plus;
            if (btn.Name == "M01PButton") pm = VSCountDown.PlusMinus.Plus;
            if (btn.Name == "M10PButton") pm = VSCountDown.PlusMinus.Plus;
            if (btn.Name == "H01PButton") pm = VSCountDown.PlusMinus.Plus;
            if (btn.Name == "H10PButton") pm = VSCountDown.PlusMinus.Plus;

            if (btn.Name == "S01MButton") pm = VSCountDown.PlusMinus.Minus;
            if (btn.Name == "S10MButton") pm = VSCountDown.PlusMinus.Minus;
            if (btn.Name == "M01MButton") pm = VSCountDown.PlusMinus.Minus;
            if (btn.Name == "M10MButton") pm = VSCountDown.PlusMinus.Minus;
            if (btn.Name == "H01MButton") pm = VSCountDown.PlusMinus.Minus;
            if (btn.Name == "H10MButton") pm = VSCountDown.PlusMinus.Minus;

            if (btn.Name == "S01PButton") digit = VSCountDown.Digit.Digit_s01;
            if (btn.Name == "S10PButton") digit = VSCountDown.Digit.Digit_s10;
            if (btn.Name == "M01PButton") digit = VSCountDown.Digit.Digit_m01;
            if (btn.Name == "M10PButton") digit = VSCountDown.Digit.Digit_m10;
            if (btn.Name == "H01PButton") digit = VSCountDown.Digit.Digit_h01;
            if (btn.Name == "H10PButton") digit = VSCountDown.Digit.Digit_h10;

            if (btn.Name == "S01MButton") digit = VSCountDown.Digit.Digit_s01;
            if (btn.Name == "S10MButton") digit = VSCountDown.Digit.Digit_s10;
            if (btn.Name == "M01MButton") digit = VSCountDown.Digit.Digit_m01;
            if (btn.Name == "M10MButton") digit = VSCountDown.Digit.Digit_m10;
            if (btn.Name == "H01MButton") digit = VSCountDown.Digit.Digit_h01;
            if (btn.Name == "H10MButton") digit = VSCountDown.Digit.Digit_h10;

            gVSCountDown.SetTimer(pm, digit);
            TimerTextBox.Text = gVSCountDown.StringTime;
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            gVSCountDown.Clear();
            TimerTextBox.Text = gVSCountDown.StringTime;
        }
        private void M5Button_Click(object sender, RoutedEventArgs e)
        {
            gVSCountDown.SetTimer(new DateTime(0).AddMinutes(5));
            TimerTextBox.Text = gVSCountDown.StringTime;
        }

        private void M50Button_Click(object sender, RoutedEventArgs e)
        {
            gVSCountDown.SetTimer(new DateTime(0).AddMinutes(50));
            TimerTextBox.Text = gVSCountDown.StringTime;
        }

        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn.Content.ToString() == "CountDown")
            {
                gVSCountDown.UpDownTime = VSCountDown.UpDown.Down;
            }
            else if (btn.Content.ToString() == "CountUp")
            {
                gVSCountDown.UpDownTime = VSCountDown.UpDown.Up;
            }
            gVSCountDown.Title = TitleTextBox.Text;

            //
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["Title"] = TitleTextBox.Text;

            this.Frame.Navigate(typeof(CountDownPage), gVSCountDown);
        }




    }
}
