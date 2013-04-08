using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSHackathonTimer
{
    public class VSCountDown
    {
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

        public int IntTime { private set; get; }
        public string StringTime { private set; get; }
        public DateTime DateTimeTime { private set; get; }

        private void CalcTime()
        {
            IntTime = 0;
            int dig = 1;
            foreach (var t in timer)
            {
                IntTime += t * dig;
                dig *= 10;
            }
            int dotCount = 0;
            もう少しリファクタリング
            var st = new StringBuilder();
            foreach (var t in timer.Reverse())
            {
                if (dotCount % 2 == 0 && dotCount != 0)
                {
                    st.Append(":");
                }
                st.Append(t);
                dotCount++;
            }
            StringTime = st.ToString();
            DateTimeTime = new DateTime(0);
            DateTimeTime = DateTimeTime.AddSeconds(timer[(int)Digit.Digit_s10] * 10 + timer[(int)Digit.Digit_s01] * 1);
            DateTimeTime = DateTimeTime.AddMinutes(timer[(int)Digit.Digit_m10] * 10 + timer[(int)Digit.Digit_m01] * 1);
            DateTimeTime = DateTimeTime.AddHours(timer[(int)Digit.Digit_h10] * 10 + timer[(int)Digit.Digit_h01] * 1);
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

    }
}
