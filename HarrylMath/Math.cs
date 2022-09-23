﻿using System.Numerics;

namespace HarrylMath
{
    public class Math
    {   
        public static double Abs(double x)
        {
            if (x < 0.0) return x * -1.0;
            return x;
        }

        public static double Sqrt(double x, int steps = 20)
        {
            double x1 = x / 2.0;
            for (int i = 0; i < steps; i++)
            {
                x1 = ((x / x1) + x1) / 2.0;
            }

            return x1;
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
            return Math.Pow(x, n);
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

        public static double Lerp(double x1, double x2, double a)
        {
            return x1 * (1.0 - a) + x2 * a;
        }

        public static double Integrate(System.Func<double, double> function, double a, double b, int steps = 1000)
        {
            double x1 = 0.0;
            for (int i = 0; i < steps; i++)
            {
                x1 += function(Lerp(a, b, (double)i / steps)) / steps;
            }

            return x1;
        }

        public static double PI = 3.1415926535897931;

        public static double DegreesToRadians(double radians)
        {
            return (radians / 360.0) * (2 * PI);
        }
        public static double RadiansToDegrees(double radians)
        {
            return (radians / (2 * PI)) * 360.0;
        }

        public static double Sin(double radians, int steps = 10)
        {
            return Math.Sin(radians);
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
            return Math.Asin(d);
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
            return Math.Cos(radians);
            /*double x1 = 0.0;

            for (int i = 0; i < steps; i++)
            {
                x1 += Pow(radians, i * 2) / Factorial(i * 2) * (i % 2 == 1 ? -1 : 1);
            }

            return x1;*/
        }

        public static double Acos(double d, int steps = 10)
        {
            return Math.Acos(d);
            /*if (d < -1 || d > 1) return 0.0;
            return PI / 2.0 - Asin(d, steps);*/
        }

        public static double Log(double x, double y)
        {
            return 1.0;
        }
    }
}