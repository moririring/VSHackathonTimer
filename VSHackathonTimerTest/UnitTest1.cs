using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using VSHackathonTimer;

namespace VSHackathonTimerTest
{
    [TestClass]
    public class UnitTest1
    {
        VSCountDown cd = new VSCountDown();

        [TestMethod]
        public void カウントダウンタイマーから時間取得()
        {
            Assert.IsTrue(cd.IntTime == 0);
        }
        [TestMethod]
        public void カウントダウンタイマーの３桁目を減少()
        {
            cd.SetTimer(VSCountDown.PlusMinus.Minus, VSCountDown.Digit.Digit_m01);
            Assert.IsTrue(cd.IntTime == 900);
        }
        [TestMethod]
        public void カウントダウンタイマーの3桁目2回減少()
        {
            cd.SetTimer(VSCountDown.PlusMinus.Minus, VSCountDown.Digit.Digit_m01);
            cd.SetTimer(VSCountDown.PlusMinus.Minus, VSCountDown.Digit.Digit_m01);
            Assert.IsTrue(cd.IntTime == 800);
        }
        [TestMethod]
        public void カウントダウンタイマーの2桁目を増加()
        {
            cd.SetTimer(VSCountDown.PlusMinus.Plus, VSCountDown.Digit.Digit_s10);
            Assert.IsTrue(cd.IntTime == 10);
        }
        [TestMethod]
        public void カウントダウンタイマーの2桁目を増加した時間()
        {
            cd.SetTimer(VSCountDown.PlusMinus.Plus, VSCountDown.Digit.Digit_s10);
            Assert.IsTrue(cd.DateTimeTime.ToString("HH:mm:ss") == "00:00:10");
        }
        [TestMethod]
        public void カウントダウンタイマーの4と5桁目を増加()
        {
            cd.SetTimer(VSCountDown.PlusMinus.Plus, VSCountDown.Digit.Digit_h01);
            cd.SetTimer(VSCountDown.PlusMinus.Plus, VSCountDown.Digit.Digit_h10);
            Assert.IsTrue(cd.IntTime == 110000);
        }
        [TestMethod]
        public void カウントダウンタイマーの4と5桁目を増加した時間()
        {
            cd.SetTimer(VSCountDown.PlusMinus.Plus, VSCountDown.Digit.Digit_h01);
            cd.SetTimer(VSCountDown.PlusMinus.Plus, VSCountDown.Digit.Digit_h10);
            Assert.IsTrue(cd.DateTimeTime.ToString("HH:mm:ss") == "11:00:00");
        }
        [TestMethod]
        public void カウントダウンタイマーの99分をチェック()
        {
            cd.SetTimer(VSCountDown.PlusMinus.Minus, VSCountDown.Digit.Digit_m01);
            cd.SetTimer(VSCountDown.PlusMinus.Minus, VSCountDown.Digit.Digit_m10);
            Assert.IsTrue(cd.StringTime == "00:99:00");
        }
        [TestMethod]
        public void カウントダウンタイマーをクリア()
        {
            cd.SetTimer(VSCountDown.PlusMinus.Minus, VSCountDown.Digit.Digit_m01);
            cd.SetTimer(VSCountDown.PlusMinus.Minus, VSCountDown.Digit.Digit_m10);
            cd.Clear();
            Assert.IsTrue(cd.StringTime == "00:00:00");
        }
        [TestMethod]
        public void カウントダウンタイマーをカウントアップ()
        {
            cd.SetTimer(VSCountDown.PlusMinus.Plus, VSCountDown.Digit.Digit_s10);
            cd.Counter();
            Assert.IsTrue(cd.StringTime == "00:00:11");
        }
        [TestMethod]
        public void カウントダウンタイマーをカウントダウン()
        {
            cd.UpDownTime = VSCountDown.UpDown.Down;
            cd.Counter();
            Assert.IsTrue(cd.StringTime == "-00:00:01");
        }
    }
}
