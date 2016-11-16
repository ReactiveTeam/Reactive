using System;

namespace Reactive.Utils.Extensions
{
    /// <summary>
    /// Extensions for <see cref="int"/> class.
    /// </summary>
    public static class IntExtensions
    {
        private const string BaseFormat = "########0";

        public static string ToString(this int i, bool signed = false)
        {
            string text = "########0";
            if (signed)
            {
                text = string.Concat(new string[]
                {
                    "+",
                    text,
                    ";-",
                    text,
                    ";+0"
                });
            }
            return i.ToString(text);
        }
    }
}
