using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCooking.Parsers
{ 
    public struct Fraction
    {
        public int Number { get; private set; }
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }

        public Fraction(int number, int numerator, int denominator)
        {
            this.Number = number;
            this.Numerator  = numerator;
            this.Denominator = denominator;
        }

        public override string ToString()
        {
            if (this.Number == 0)
                return String.Format("{0}/{1}", this.Numerator, this.Denominator);
            else
                return String.Format("{0} {1}/{2}", this.Number, this.Numerator, this.Denominator);
        }
    }

    public class FractionParser
    {
        public static Fraction RealToFraction(decimal value)
        {
            return RealToFraction(value, 0.1M);
        }

        private static Fraction RealToFraction(decimal value, decimal accuracy)
        {
            int wholeNumberPart = 0;

            if (accuracy <= 0.0M || accuracy >= 1.0M)
            {
                throw new ArgumentOutOfRangeException("accuracy", "Must be > 0 and < 1.");
            }

            if (value > 1)
            {
                wholeNumberPart = (int)Math.Truncate(value);
                value = value - wholeNumberPart;
            }

            int sign = Math.Sign(value);

            if (sign == -1)
            {
                value = Math.Abs(value);
            }

            // Accuracy is the maximum relative error; convert to absolute maxError
            decimal maxError = sign == 0 ? accuracy : value * accuracy;

            int n = (int)Math.Floor(value);
            value -= n;

            if (value < maxError)
            {
                return new Fraction(wholeNumberPart, sign * n, 1);
            }

            if (1 - maxError < value)
            {
                return new Fraction(wholeNumberPart, sign * (n + 1), 1);
            }

            // The lower fraction is 0/1
            int lower_n = 0;
            int lower_d = 1;

            // The upper fraction is 1/1
            int upper_n = 1;
            int upper_d = 1;

            while (true)
            {
                // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
                int middle_n = lower_n + upper_n;
                int middle_d = lower_d + upper_d;

                if (middle_d * (value + maxError) < middle_n)
                {
                    // real + error < middle : middle is our new upper
                    upper_n = middle_n;
                    upper_d = middle_d;
                }
                else if (middle_n < (value - maxError) * middle_d)
                {
                    // middle < real - error : middle is our new lower
                    lower_n = middle_n;
                    lower_d = middle_d;
                }
                else
                {
                    // Middle is our best fraction
                    return new Fraction(wholeNumberPart, (n * middle_d + middle_n) * sign, middle_d);
                }
            }
        }
    }
}
