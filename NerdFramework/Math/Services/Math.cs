﻿namespace NerdFramework
{
    public static class Math
    {
        public static double FromParameterization3(double t, double s, double a, double b, double c)
        {
            return a * (1.0 - t - s) + b * t + c * s;
        }

        public static double Average(params double[] values)
        {
            double total = 0.0;
            for (int i = 0; i < values.Length; i++)
            {
                total += values[i];
            }
            return total / values.Length;
        }

        public static double Min(double a, double b)
        {
            if (a <= b) return a;
            return b;
        }

        public static double Min(params double[] values)
        {
            double min = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] < min)
                    min = values[i];
            }
            return min;
        }

        public static double Max(double a, double b)
        {
            if (a >= b) return a;
            return b;
        }

        public static double Max(params double[] values)
        {
            double max = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > max)
                    max = values[i];
            }
            return max;
        }

        public static double[] Bounds(params double[] values)
        {
            double min = values[0];
            double max = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > max)
                    max = values[i];
                else if (values[i] < min)
                    min = values[i];
            }
            return new double[2] { min, max };
        }

        public static int[] BoundsIndex(params double[] values)
        {
            int min = 0;
            int max = 0;
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > values[max])
                    max = i;
                else if (values[i] < values[min])
                    min = i;
            }
            return new int[2] { min, max };
        }

        public static int MinIndex(params double[] values)
        {
            double min = values[0];
            int index = 0;
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] < min)
                {
                    min = values[i];
                    index = i;
                }
            }
            return index;
        }

        public static int MaxIndex(params double[] values)
        {
            double min = values[0];
            int index = 0;
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > min)
                {
                    min = values[i];
                    index = i;
                }
            }
            return index;
        }

        public static double Constrain(double x, double min, double max)
        {
            if (x > max)
                return max;
            if (x < min)
                return min;
            return x;
        }

        public static double Abs(double x)
        {
            return x < 0.0 ? -x : x;
        }

        public static int Floor(double x)
        {
            return (int)System.Math.Floor(x);
        }

        public static int Ceil(double x)
        {
            return (int)System.Math.Ceiling(x);
        }

        public static double Sqrt(double x, int steps = 20)
        {
            return System.Math.Sqrt(x);
            /*double x1 = x / 2.0;
            for (int i = 0; i < steps; i++)
            {
                x1 = ((x / x1) + x1) / 2.0;
            }

            return x1;*/
        }

        public static double Pow(double x, int n)
        {
            double x1 = 1.0;
            if (n == 0) return x1;

            for (int i = 0; i < n; i++)
            {
                x1 *= x;
            }

            if (n < 0) return 1 / x1;
            return x1;
        }

        public static double Pow(double x, double n)
        {
            return System.Math.Pow(x, n);
            //return Pow(x, (int)(n*10)) / Pow(x, 10);
        }

        public static int Factorial(int x)
        {
            int x1 = 1;
            for (int i = 1; i <= x; i++)
            {
                x1 *= i;
            }

            return x1;
        }

        public static double Integrate(System.Func<double, double> function, double a, double b, int steps = 1000)
        {
            double x1 = 0.0;
            for (int i = 0; i < steps; i++)
            {
                x1 += function(Tween.Linear(a, b, (double)i / steps)) / steps;
            }

            return x1;
        }

        public static double PI = 3.1415926535897931;
        public static double HalfPI = PI / 2.0;
        public static double QuarterPI = PI / 4.0;
        public static double TwoPI = PI * 2.0;

        public static double DegreesToRadians(double degrees)
        {
            /* Radians => Complete Rotation: 2*PI
             * Degrees => Complete Rotation: 360
             * 
             * Conversion:
             * Degrees => Radians
             * (x / 360) * (2 * PI)
             * (x * PI) / 180
             */

            return (degrees * PI) / 180.0;
        }
        public static double RadiansToDegrees(double radians)
        {
            /* Radians => Complete Rotation: 2*PI
             * Degrees => Complete Rotation: 360
             * 
             * Conversion:
             * Degrees => Radians
             * (x / [2 * PI]) * 360.0
             * (x * 180) / PI
             */

            return (radians * 180.0) / PI;
        }

        public static double Sin(double radians, int steps = 10)
        {
            return System.Math.Sin(radians);

            /* Possible fast solution?
             * https://gamedev.stackexchange.com/a/4780
             */

            /*
            double x1 = 0.0;
            
            for (int i = 1; i <= steps; i++)
            {
                x1 += Pow(radians, i * 2) / Factorial(i * 2) * (i % 2 == 0 ? -1 : 1);
            }

            return x1;*/
        }

        public static double Asin(double d, int steps = 10)
        {
            return System.Math.Asin(d);
            /*if (d < -1 || d > 1) return 0.0;
            double x1 = 0.0;

            for (int i = 0; i < steps; i++)
            {
                x1 += (Factorial(i * 2) / (Pow(2, i * 2) * Pow(Factorial(i), 2))) * (Pow(d, (2*i) + 1)/((2*i) + 1));
            }

            return x1*/
        }

        public static double Cos(double radians, int steps = 10)
        {
            return System.Math.Cos(radians);
            /*double x1 = 0.0;

            for (int i = 0; i < steps; i++)
            {
                x1 += Pow(radians, i * 2) / Factorial(i * 2) * (i % 2 == 1 ? -1 : 1);
            }

            return x1;*/
        }

        public static double Acos(double d, int steps = 10)
        {
            return System.Math.Acos(d);
            /*if (d < -1 || d > 1) return 0.0;
            return PI / 2.0 - Asin(d, steps);*/
        }

        public static double Tan(double radians, int steps = 10)
        {
            return System.Math.Tan(radians);
        }

        public static double Atan(double d, int steps = 10)
        {
            return System.Math.Atan(d);
        }

        public static double Cot(double radians)
        {
            return 1.0 / Tan(radians);
        }

        public static double Acot(double d)
        {
            return Atan(1.0 / d);
        }

        public static double Log(double x, double y)
        {
            return 1.0;
        }
    }
}
