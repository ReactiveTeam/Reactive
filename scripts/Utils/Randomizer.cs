using System;

namespace Reactive.Utils
{
    /// <summary>
    ///   <para>Class for generating random data.</para>
    /// </summary>
    public class Randomizer
    {
        public int Seed;

        protected double AM;

        protected readonly int IA = 16807;

        protected readonly int IM = 2147483647;

        protected readonly int IQ = 127773;

        protected readonly int IR = 2836;

        protected readonly int MASK = 123459876;

        public Randomizer(int _seed)
        {
            this.AM = 1.0 / (double)this.IM;
            this.Seed = _seed;
        }

        public double NextDouble()
        {
            this.Seed ^= this.MASK;
            int num = this.Seed / this.IQ;
            this.Seed = this.IA * (this.Seed - num * this.IQ) - this.IR * num;
            if (this.Seed < 0)
            {
                this.Seed += this.IM;
            }
            double result = this.AM * (double)this.Seed;
            this.Seed ^= this.MASK;
            return result;
        }

        public int Next(int min, int maxNotReached)
        {
            return min + (int)(this.NextDouble() * (double)(maxNotReached - min));
        }

        public int Next(int maxNotReached)
        {
            return (int)(this.NextDouble() * (double)maxNotReached);
        }

        public float Next()
        {
            return (float)this.NextDouble();
        }

        /// <summary>
        ///   <para>Returns a random float number between and min [inclusive] and max [inclusive] (Read Only).</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public float Range(float min, float max)
        {
            return this.Next() * (max - min) + min;
        }

        /// <summary>
        ///   <para>Returns a random integer within a dice (eg: sides = 8 would return a number within 1-8).</para>
        /// </summary>
        /// <param name="sides"></param>
        public int Dice(int sides)
        {
            return (int)(this.NextDouble() * (double)sides + 1.0);
        }
    }
}
