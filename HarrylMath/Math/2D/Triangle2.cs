﻿namespace NerdFramework
{
    public class Triangle2
    {
        public Vector2 a;
        public Vector2 b;
        public Vector2 c;

        public Triangle2(Vector2 a, Vector2 b, Vector2 c)
        {
            /* Triangle:
             * a: point1
             * b: point2
             * c: point3
             */

            this.a = a;
            this.b = b;
            this.c = c;
        }

        public Vector2 Parameterization(Vector2 point)
        {
            // Repackages the Meets(Point) method to spit out t and s
            // Useful for any operations that involve finding a vector
            // in terms of two non-parallel vector components

            /* A, B, and C are COPLANAR, and vertices of the triangle
             * P is any point that lies within the triangle
             * 
             * Parameterization of triangle:
             * A triangle is created by two vectors AB and AC
             * A solution of the triangle consists of initial point A with added components AB*t and AC*s
             * In other words, picture a triangle as a defined coordinate plane of two (non?)orthogonal axes, AB and AC
             * 
             * OP = OA + (OB-OA)t + (OC-OA)s
             * (OP-OA) = (OB-OA)t + (OC-OA)s <=> AP = ABt + ACs
             * The point is within the triangle if t and s fall within defined boundaries
             * 
             * Solving for t and s:
             * P = (OP-OA)
             * B = (OB-OA)
             * C = (OC-OA)
             * 
             * P = Bt + Cs
             * Thus, we can derive t and s from a system of two equations:
             * P.x = B.x*t + C.x*s
             * P.y = B.y*t + C.y*s
             * 
             * s = (P.y - B.y*t)/C.y
             * t = (P.y - C.y*s)/B.y
             * 
             * P.x = B.x*t + C.x*[(P.y - B.y*t)/C.y]
             * B.x*t - B.y*t*(C.x/C.y) = P.x - P.y*(C.x/C.y)
             * t = [P.x - P.y*(C.x/C.y)] / [B.x - B.y*(C.x/C.y)]
             * 
             * P.x = B.x*(P.y - C.y*s)/B.y + C.x*s
             * C.x*s - C.y*s*(B.x/B.y) = P.x - P.y*(B.x/B.y)
             * s = [P.x - P.y*(B.x/B.y)] / [C.x - C.y*(B.x/B.y)]
             * 
             * t and s are interpolant values between 0 and 1 that describe P length relative to axis lengths
             * IF t defines how far the point is along AB and s the same for AC:
             * The hypotenuse BC can be seen as a linear inverse relationship between s and t
             * 
             * THUS the additional condition applies:
             * t <= 1 - s
             * t + s <= 1
             * 
             * [Conclusion]
             * After calculating t and s using the following formulas:
             * t = [P.x - P.y*(C.x/C.y)] / [B.x - B.y*(C.x/C.y)]
             * s = [P.x - P.y*(B.x/B.y)] / [C.x - C.y*(B.x/B.y)]
             */

            Vector2 AB = b - a;
            Vector2 AC = c - a;
            Vector2 AP = point - a;
            double ABdiff = AB.x / AB.y;
            double ACdiff = AC.x / AC.y;
            double t = (AP.x - AP.y * ACdiff) / (AB.x - AB.y * ACdiff);
            double s = (AP.x - AP.y * ABdiff) / (AC.x - AC.y * ABdiff);

            return new Vector2(t, s);
        }

        public bool Meets(Vector2 point)
        {
            /* A, B, and C are COPLANAR, and vertices of the triangle
             * P is any point that lies within the triangle
             * 
             * Parameterization of triangle:
             * A triangle is created by two vectors AB and AC
             * A solution of the triangle consists of initial point A with added components AB*t and AC*s
             * In other words, picture a triangle as a defined coordinate plane of two (non?)orthogonal axes, AB and AC
             * 
             * OP = OA + (OB-OA)t + (OC-OA)s
             * (OP-OA) = (OB-OA)t + (OC-OA)s <=> AP = ABt + ACs
             * The point is within the triangle if t and s fall within defined boundaries
             * 
             * Solving for t and s:
             * P = (OP-OA)
             * B = (OB-OA)
             * C = (OC-OA)
             * 
             * P = Bt + Cs
             * Thus, we can derive t and s from a system of two equations:
             * P.x = B.x*t + C.x*s
             * P.y = B.y*t + C.y*s
             * 
             * s = (P.y - B.y*t)/C.y
             * t = (P.y - C.y*s)/B.y
             * 
             * P.x = B.x*t + C.x*[(P.y - B.y*t)/C.y]
             * B.x*t - B.y*t*(C.x/C.y) = P.x - P.y*(C.x/C.y)
             * t = [P.x - P.y*(C.x/C.y)] / [B.x - B.y*(C.x/C.y)]
             * 
             * P.x = B.x*(P.y - C.y*s)/B.y + C.x*s
             * C.x*s - C.y*s*(B.x/B.y) = P.x - P.y*(B.x/B.y)
             * s = [P.x - P.y*(B.x/B.y)] / [C.x - C.y*(B.x/B.y)]
             * 
             * t and s are interpolant values between 0 and 1 that describe P length relative to axis lengths
             * IF t defines how far the point is along AB and s the same for AC:
             * The hypotenuse BC can be seen as a linear inverse relationship between s and t
             * 
             * THUS the additional condition applies:
             * t <= 1 - s
             * t + s <= 1
             * 
             * [Conclusion]
             * After calculating t and s using the following formulas:
             * t = [P.x - P.y*(C.x/C.y)] / [B.x - B.y*(C.x/C.y)]
             * s = [P.x - P.y*(B.x/B.y)] / [C.x - C.y*(B.x/B.y)]
             * 
             * The point is a solution of the triangle if:
             * t >= 0
             * s >= 0
             * t + s <= 1
             */

            Vector2 AB = b - a;
            Vector2 AC = c - a;
            Vector2 AP = point - a;
            double ABdiff = AB.x / AB.y;
            double ACdiff = AC.x / AC.y;
            double t = (AP.x - AP.y * ACdiff) / (AB.x - AB.y * ACdiff);
            double s = (AP.x - AP.y * ABdiff) / (AC.x - AC.y * ABdiff);

            return t >= 0.0 && s >= 0.0 && t + s <= 1.0;
        }
    }
}