using System;
using System.Linq;
using System.Text;

namespace VSHackathonTimer
{
    public class VsCountDown
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
            DigitS01,
            DigitS10,
            DigitM01,
            DigitM10,
            DigitH01,
            DigitH10,
            DigitMax,
        };

        private int[] IntTimes { get; set; }
        private int IntTime {  set; get; }
        public string StringTime { private set; get; }
        public DateTime DateTimeTime { set; get; }

        public bool Minus { set; get; }
        public DateTime Start { set; get; }
        public DateTime End { set; get; }
        public UpDown UpDownTime { set; get; }
        public string Title { set; get; }

        public VsCountDown()
        {
            IntTimes = new[] { 0, 0, 0, 0, 0, 0 };
            CalcTime();
        }

        private void CalcIntTime()
        {
            IntTime = 0;
            var dig = 1;
            foreach (var t in IntTimes)
            {
                IntTime += t * dig;
                dig *= 10;
            }
        }
        private void CalcStringTime()
        {
            var st = new StringBuilder();
            if (UpDownTime == UpDown.Down && Minus)
            {
                st.Append("-");
            }
            foreach (var t in IntTimes.Reverse().Select((v, i) => new { v, i }))
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
            DateTimeTime = DateTimeTime.AddSeconds(IntTimes[(int)Digit.DigitS10] * 10 + IntTimes[(int)Digit.DigitS01] * 1);
            DateTimeTime = DateTimeTime.AddMinutes(IntTimes[(int)Digit.DigitM10] * 10 + IntTimes[(int)Digit.DigitM01] * 1);
            DateTimeTime = DateTimeTime.AddHours(IntTimes[(int)Digit.DigitH10] * 10 + IntTimes[(int)Digit.DigitH01] * 1);

        }


        public void SetTimer(PlusMinus ud, Digit p)
        {
            if (ud == PlusMinus.Plus)
            {
                IntTimes[(int)p] = (IntTimes[(int)p] + 1) % 10;
            }
            else
            {
                IntTimes[(int)p] = (IntTimes[(int)p] + (10 - 1)) % 10;
            }
            CalcTime();
        }
        public void SetTimer(DateTime time)
        {
            IntTimes[(int)Digit.DigitS01] = time.Second % 10;
            IntTimes[(int)Digit.DigitS10] = time.Second / 10;
            IntTimes[(int)Digit.DigitM01] = time.Minute % 10;
            IntTimes[(int)Digit.DigitM10] = time.Minute / 10;
            IntTimes[(int)Digit.DigitH01] = time.Hour % 10;
            IntTimes[(int)Digit.DigitH10] = time.Hour / 10;
            CalcTime();
        }
        public void Clear()
        {
            for (var i = 0; i < IntTimes.Length; i++)
            {
                IntTimes[i] = 0;
            }
            CalcTime();
        }
        public void SetStart()
        {
            Minus = false;
            if (UpDownTime == UpDown.Up)
            {
                Start = new DateTime(0);
                End = DateTimeTime;
            }
            else
            {
                Start = DateTimeTime;
                End = new DateTime(0);
            }
            SetTimer(Start);
        }
        public void Counter()
        {
            if (DateTimeTime.ToString("HH:mm:ss") == End.ToString("HH:mm:ss"))
            {
                Minus = true;
            }
            var pm = (UpDownTime == UpDown.Up || Minus) ? 1 : -1;
            SetTimer(DateTimeTime.AddSeconds(pm));
        }


    }
}
