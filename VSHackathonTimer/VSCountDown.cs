using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSHackathonTimer
{
    public class VSCountDown
    {
        public enum UpDown
        {
            Up,
            Down,
        };
        public enum PlusMinus
        {
            Plus,
            Minus,
        };
        public enum Digit
        {
            Digit_s01,
            Digit_s10,
            Digit_m01,
            Digit_m10,
            Digit_h01,
            Digit_h10,
            Digit_Max,
        };
        public int[] timer = new int[] { 0, 0, 0, 0, 0, 0 };
        public int[] Timer
        {
            get { return timer; }
        }
        public bool Minus { set; get; }
        public int IntTime { private set; get; }
        public string StringTime { private set; get; }
        public DateTime DateTimeTime { set; get; }
        public UpDown UpDownTime { set; get; }
        public string Title { set; get; }

        public VSCountDown()
        {
            CalcTime();
        }
        private void CalcIntTime()
        {
            IntTime = 0;
            int dig = 1;
            foreach (var t in timer)
            {
                IntTime += t * dig;
                dig *= 10;
            }
        }
        private void CalcStringTime()
        {
            var st = new StringBuilder();
            if (Minus == true)
            {
                st.Append("-");
            }
            foreach (var t in timer.Reverse().Select((v, i) => new { v, i }))
            {
                //最初以外2回毎に:をつける
                if (t.i % 2 == 0 && t.i != 0)
                {
                    st.Append(":");
                }
                st.Append(t.v);
            }
            StringTime = st.ToString();
        }

        private void CalcTime()
        {
            CalcIntTime();
            CalcStringTime();
            //DateTime
            DateTimeTime = new DateTime(0);
            DateTimeTime = DateTimeTime.AddSeconds(timer[(int)Digit.Digit_s10] * 10 + timer[(int)Digit.Digit_s01] * 1);
            DateTimeTime = DateTimeTime.AddMinutes(timer[(int)Digit.Digit_m10] * 10 + timer[(int)Digit.Digit_m01] * 1);
            DateTimeTime = DateTimeTime.AddHours(timer[(int)Digit.Digit_h10] * 10 + timer[(int)Digit.Digit_h01] * 1);

        }
        private void CalcTimerFromDateTime()
        {
            timer[(int)Digit.Digit_s01] = DateTimeTime.Second % 10;
            timer[(int)Digit.Digit_s10] = DateTimeTime.Second / 10;
            timer[(int)Digit.Digit_m01] = DateTimeTime.Minute % 10;
            timer[(int)Digit.Digit_m10] = DateTimeTime.Minute / 10;
            timer[(int)Digit.Digit_h01] = DateTimeTime.Hour % 10;
            timer[(int)Digit.Digit_h10] = DateTimeTime.Hour / 10;
            CalcIntTime();
            CalcStringTime();
        }
        public void SetTimer(PlusMinus ud, Digit p)
        {
            if (ud == PlusMinus.Plus)
            {
                timer[(int)p] = (timer[(int)p] + 1) % 10;
            }
            else
            {
                timer[(int)p] = (timer[(int)p] + (10 - 1)) % 10;
            }
            CalcTime();
        }
        public void SetTimer(DateTime gStartDateTime)
        {
            Minus = false;
            DateTimeTime = gStartDateTime;
            CalcTimerFromDateTime();
        }
        public void Clear()
        {
            for (int i = 0; i < timer.Length; i++)
            {
                timer[i] = 0;
            }
            CalcTime();
        }
        public void CountDown()
        {
            if (UpDownTime == UpDown.Up)
            {
                DateTimeTime = DateTimeTime.AddSeconds(1);
            }
            else
            {
                if (DateTimeTime.ToString("HH:mm:ss") == "00:00:00")
                {
                    Minus = true;
                }
                var pm = (Minus) ? 1 : -1;
                DateTimeTime = DateTimeTime.AddSeconds(pm);
            }
            CalcTimerFromDateTime();
        }


    }
}
