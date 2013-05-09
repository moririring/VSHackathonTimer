
using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using VSHackathonTimer;

namespace VSHackathonTimerTest
{
    [TestClass]
    public class UnitTest1
    {
        readonly VsCountDown _cd = new VsCountDown();

        [TestMethod]
        public void カウントダウンタイマーの３桁目を減少()
        {
            _cd.SetTimer(VsCountDown.PlusMinus.Minus, VsCountDown.Digit.DigitM01);
            Assert.IsTrue(_cd.StringTime == "00:09:00");
        }
        [TestMethod]
        public void カウントダウンタイマーの3桁目2回減少()
        {
            _cd.SetTimer(VsCountDown.PlusMinus.Minus, VsCountDown.Digit.DigitM01);
            _cd.SetTimer(VsCountDown.PlusMinus.Minus, VsCountDown.Digit.DigitM01);
            Assert.IsTrue(_cd.StringTime == "00:08:00");
        }
        [TestMethod]
        public void カウントダウンタイマーの2桁目を増加()
        {
            _cd.SetTimer(VsCountDown.PlusMinus.Plus, VsCountDown.Digit.DigitS10);
            Assert.IsTrue(_cd.StringTime == "00:00:10");
        }
        [TestMethod]
        public void カウントダウンタイマーの5と6桁目を増加()
        {
            _cd.SetTimer(VsCountDown.PlusMinus.Plus, VsCountDown.Digit.DigitH01);
            _cd.SetTimer(VsCountDown.PlusMinus.Plus, VsCountDown.Digit.DigitH10);
            Assert.IsTrue(_cd.StringTime == "11:00:00");
        }
        [TestMethod]
        public void カウントダウンタイマーの99分をチェック()
        {
            _cd.SetTimer(VsCountDown.PlusMinus.Minus, VsCountDown.Digit.DigitM01);
            _cd.SetTimer(VsCountDown.PlusMinus.Minus, VsCountDown.Digit.DigitM10);
            Assert.IsTrue(_cd.StringTime == "00:99:00");
        }
        [TestMethod]
        public void カウントダウンタイマーをクリア()
        {
            _cd.SetTimer(VsCountDown.PlusMinus.Minus, VsCountDown.Digit.DigitM01);
            _cd.SetTimer(VsCountDown.PlusMinus.Minus, VsCountDown.Digit.DigitM10);
            _cd.Clear();
            Assert.IsTrue(_cd.StringTime == "00:00:00");
        }
        [TestMethod]
        public void カウントダウンタイマーをカウントダウン()
        {
            _cd.SetTimer(VsCountDown.PlusMinus.Plus, VsCountDown.Digit.DigitM01);
            _cd.UpDownTime = VsCountDown.UpDown.Down;
            _cd.SetStart();

            _cd.Counter();
            Assert.IsTrue(_cd.StringTime == "00:00:59");
        }
        [TestMethod]
        public void カウントダウンタイマーをカウントアップ()
        {
            _cd.SetTimer(VsCountDown.PlusMinus.Plus, VsCountDown.Digit.DigitS01);
            _cd.UpDownTime = VsCountDown.UpDown.Up;
            _cd.SetStart();

            _cd.Counter();
            Assert.IsTrue(_cd.StringTime == "00:00:01");
        }
        [TestMethod]
        public void カウントダウンタイマーをカウントダウンしてマイナスチェック()
        {
            _cd.SetTimer(new DateTime(0));
            _cd.UpDownTime = VsCountDown.UpDown.Down;
            _cd.SetStart();

            _cd.Counter();
            Assert.IsTrue(_cd.StringTime == "-00:00:01");
        }
        [TestMethod]
        public void カウントダウンタイマーをカウントプラスしてマイナスチェック()
        {
            _cd.SetTimer(VsCountDown.PlusMinus.Plus, VsCountDown.Digit.DigitS01);
            _cd.SetTimer(VsCountDown.PlusMinus.Plus, VsCountDown.Digit.DigitS01);
            _cd.UpDownTime = VsCountDown.UpDown.Up;
            _cd.SetStart();

            _cd.Counter();
            _cd.Counter();
            _cd.Counter();
            Assert.IsTrue(_cd.Minus == true);
        }
    }
}
