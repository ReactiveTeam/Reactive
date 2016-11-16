using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.Framework.Math
{
    public class MathHelpers
    {
        /// <summary>
        /// cleans non numerical values (NaN) from a float by
        /// retaining a previous property value. if 'prevValue' is
        /// omitted, the NaN will be replaced by '0.0f'.
        /// </summary>
        public static float NaNSafeFloat(float value, float prevValue = default(float))
        {
            value = double.IsNaN(value) ? prevValue : value;
            return value;
        }

        /// <summary>
        /// this can be used to snap individual super-small property
        /// values to zero, for avoiding some floating point issues.
        /// </summary>
        public static float SnapToZero(float value, float epsilon = 0.0001f)
        {
            value = (System.Math.Abs(value) < epsilon) ? 0.0f : value;
            return value;
        }

        /// <summary>
        /// reduces the number of decimals of a floating point number.
        /// this can be used to solve floating point imprecision cases.
        /// 'factor' determines the amount of decimals. Default is 1000
        /// which results in 3 decimals.
        /// </summary>
        public static float ReduceDecimals(float value, float factor = 1000)
        {
            return (float)System.Math.Round(value * factor) / factor;
        }

        /// <summary>
        /// returns true if the supplied integer is an odd value and false
        /// if it's an even value. this can be used to perform logic every
        /// other time something happens, or for every other iteration in
        /// a loop
        /// </summary>
        public static bool IsOdd(int val)
        {
            return (val % 2 != 0);
        }
    }
}
