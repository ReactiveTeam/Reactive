using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.Utils.Extensions
{
    /// <summary>
    /// Extensions for <see cref="float"/> class.
    /// </summary>
    public static class FloatExtensions
    {
        private const string BaseFormat = "########0";

        public static string ToString(this float f, int decimals = 0, bool percentage = false, bool signed = false)
        {
            string text = "########0";
            if (decimals > 0)
            {
                text = text + "." + new string('#', decimals);
            }
            if (percentage)
            {
                text += "%";
            }
            if (signed)
            {
                text = "+" + text + ";-" + text;
                text += ((!percentage) ? ";+0" : ";+0%");
            }
            return f.ToString(text);
        }

        public static int ToInt(this float f)
        {
            return (int)Math.Round(f);
        }

        public static float CeilAwayFromZero(this float f)
        {
            if (f > 0f)
            {
                return (float)Math.Ceiling(f);
            }
            return (float)Math.Floor(f);
        }

        public static float FloorTowardZero(this float f)
        {
            if (f > 0f)
            {
                return (float)Math.Floor(f);
            }
            return (float)Math.Ceiling(f);
        }

        public static float Round(this float f)
        {
            float num = Math.Abs(f);
            if (num - Math.Floor(num) == 0.5f)
            {
                return Math.Sign(f) * (float)Math.Ceiling(num);
            }
            return (float)Math.Round(f);
        }
    }
}
