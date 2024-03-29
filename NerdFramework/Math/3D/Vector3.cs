﻿namespace NerdFramework
{
    public struct Vector3
    {
        public readonly double x;
        public readonly double y;
        public readonly double z;

        public static Vector3 Zero = new Vector3();
        public static Vector3 One = new Vector3(1.0, 1.0, 1.0);
        public static Vector3 xAxis = new Vector3(1.0, 0.0, 0.0);
        public static Vector3 yAxis = new Vector3(0.0, 1.0, 0.0);
        public static Vector3 zAxis = new Vector3(0.0, 0.0, 1.0);

        public Vector3(double x = 0.0, double y = 0.0, double z = 0.0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(Vector3s spherical)
        {
            /* Spherical coordinates to Rectangular:
             * x = ρsin(ϕ)cos(θ)
             * y = ρsin(ϕ)sin(θ)
             * z = ρcos(ϕ)
             * 
             * HOWEVER: This engine uses x and z as horizontal plane
             * We will shift the values with cyclic permutation
             * 
             * z = ρsin(ϕ)cos(θ)
             * x = ρsin(ϕ)sin(θ)
             * y = ρcos(ϕ)
             */

            double s = Math.Sin(spherical.phi);
            this.z = spherical.rho * s * Math.Cos(spherical.theta);
            this.x = spherical.rho * s * Math.Sin(spherical.theta);
            this.y = spherical.rho * Math.Cos(spherical.phi);
        }

        public Vector3(Vector3 a, Vector3 b)
        {
            this.x = b.x - a.x;
            this.y = b.y - a.y;
            this.z = b.z - a.z;
        }

        public Vector3(double s)
        {
            this.x = s;
            this.y = s;
            this.z = s;
        }

        public Vector2 WithoutZ()
        {
            return new Vector2(x, y);
        }

        public double Magnitude()
        {
            /* a⋅b = |a||b|cos(theta)
             * a⋅a = |a||a|cos(0) = |a||a|
             * |a| = sqrt(a⋅a)
             */

            return Math.Sqrt(Dot(this, this));
        }

        public Vector3 Normalized()
        {
            return this / Magnitude();
        }

        public Vector3 NormalizedCubic()
        {
            double max = Math.Max(Math.Abs(this.x), Math.Abs(this.y), Math.Abs(this.z));
            return new Vector3(x / max, y / max, z / max);
        }

        public Vector3 RotateX(double radians)
        {
            /* x' = x
             * y' = ycos(theta) - zsin(theta)
             * z' = ysin(theta) + zcos(theta)
             */

            double s = Math.Sin(radians);
            double c = Math.Cos(radians);

            return new Vector3(
                this.x,
                this.y * c - this.z * s,
                this.y * s + this.z * c
            );
        }

        public Vector3 RotateY(double radians)
        {
            /* x' = xcos(theta) + zsin(theta)
             * y' = y
             * z' = -xsin(theta) + zcos(theta)
             */

            double s = Math.Sin(radians);
            double c = Math.Cos(radians);

            return new Vector3(
                this.x * c + this.z * s,
                this.y,
                -this.x * s + this.z * c
            );
        }

        public Vector3 RotateZ(double radians)
        {
            /* x' = xcos(theta) - ysin(theta)
             * y' = xsin(theta) + ycos(theta)
             * z' = z
             */

            double s = Math.Sin(radians);
            double c = Math.Cos(radians);

            return new Vector3(
                this.x * c - this.y * s,
                this.x * s + this.y * c,
                this.z
            );
        }

        public Vector3 Rotate(double r1, double r2, double r3)
        {
            Vector3 newVector = this;

            if (r1 != 0.0)
                newVector = newVector.RotateX(r1);
            if (r2 != 0.0)
                newVector = newVector.RotateY(r2);
            if (r3 != 0.0)
                newVector = newVector.RotateZ(r3);

            return newVector;
        }

        public Vector3 RotateAbout(Vector3 rotand, double radians)
        {
            double c = Math.Cos(radians);
            return this * c + rotand * Vector3.Dot(this, rotand) * (1 - c) + Vector3.Cross(rotand, this) * Math.Sin(radians);
        }

        public static Vector3 Angle3(Vector3 a, Vector3 b)
        {
            // Create projections of vectors a and b onto the x-axis
            Vector3 Ax = new Vector3(0.0, a.y, a.z);
            Vector3 Bx = new Vector3(0.0, b.y, b.z);

            // Calculate 2D x-angle between projections
            double xRadians = Angle(Ax, Bx);

            // Create angle-adjusted a vector
            Vector3 a2 = a.RotateX(xRadians);

            // Create projections of vectors a2 and b onto the y-axis
            Vector3 Ay = new Vector3(a2.x, 0.0, a2.z);
            Vector3 By = new Vector3(b.x, 0.0, b.z);

            // Calculate 2D y-angle between projections
            double yRadians = Angle(Ay, By);

            // Create angle-adjusted a vector
            Vector3 a3 = a2.RotateY(yRadians);

            // Create projections of vectors a3 and b onto the z-axis
            Vector3 Az = new Vector3(a3.x, a3.y, 0.0);
            Vector3 Bz = new Vector3(b.x, b.y, 0.0);

            // Calculate 2D z-angle between projections
            double zRadians = Angle(Az, Bz);

            return new Vector3(xRadians, yRadians, zRadians);
        }

        public static double Angle(Vector3 a, Vector3 b)
        {
            /* a⋅b = |a||b|cos(theta)
             * 
             * cos(theta) = (a⋅b)/(|a||b|)
             * theta = acos((a⋅b)/(|a||b|))
             */

            if (a == b)
                return 0.0;
            return Math.Acos(Dot(a, b) / (a.Magnitude() * b.Magnitude()));
        }

        public static double Dot(Vector3 a, Vector3 b)
        {
            /* a⋅b = (a1*b1) + (a2*b2) + (a3*b3)
             * 
             * a⋅b = 0 <=> a ⊥ b
             * 
             * a⋅b = 1 <=> a'⋅b = 0 <=> a' ⊥ b
             * 
             * a⋅b = |a||b|cos(theta)
             * 
             * a⋅b = b⋅a
             */

            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
        }

        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            /*          | e1 e2 e3 |      | a2 a3 |        | a1 a3 |        | a1 a2 |
             * a×b = det| a1 a2 a3 | = det| b2 b3 |e1 - det| b1 b3 |e2 + det| b1 b2 |e3
             *          | b1 b2 b3 |
             * 
             *  = <(a2*b3) - (a3*b2), -[(a1*b3) - (a3*b1)], (a1*b2) - (a2*b1)>
             * 
             * c = a×b <=> c ⊥ a AND c ⊥ b
             * 
             * |a×b| = |A||B|sin(theta) = Area of Parallelogram
             * 
             * a×b = -b×a
             */

            return new Vector3((a.y * b.z) - (a.z * b.y), -(a.x * b.z) + (a.z * b.x), (a.x * b.y) - (a.y * b.x));
        }

        public static double Triple(Vector3 a, Vector3 b, Vector3 c)
        {
            /*              | a1 a2 a3 |      | b2 b3 |        | b1 b3 |        | b1 b2 |
             * a⋅(b×c) = det| b1 b2 b3 | = det| c2 c3 |a1 - det| c1 c3 |a2 + det| c1 c2 |a3
             *              | c1 c2 c3 |
             * 
             *  = [(b2*c3) - (b3*c2)]a1 -[(b1*c3) - (b3*c1)]a2 + [(b1*c2) - (b2*c1)]a3
             * 
             * |a⋅(b×c)| = Volume of Parallelepiped
             * 
             * a⋅(b×c) = c⋅(a×b) = b⋅(c×a)
             * a⋅(b×c) = -b⋅(a×c) = -c⋅(b×a)
             */

            return ((b.y * c.z) - (b.z * c.y)) * a.x - ((b.x * c.z) - (b.z * c.x)) * a.y + ((b.x * c.y) - (b.y * c.x)) * a.z;
        }

        public static bool Parallel(Vector3 a, Vector3 b)
        {
            /* IF a = b * s:
             * a ∥ b
             * 
             * CANNOT check if a/s = b,
             * as a component of s COULD be zero
             */

            Vector3 v1 = a.Normalized();
            Vector3 v2 = b.Normalized();
            return (v1 == v2 || v1 == -v2);
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, double alpha)
        {
            return a * (1 - alpha) + b * alpha;
        }

        public static Vector3 FromParameterization3(double t, double s, Vector3 a, Vector3 b, Vector3 c)
        {
            double u = 1.0 - t - s;
            return new Vector3(a.x*u + b.x*t + c.x*s, a.y*u + b.y*t + c.y*s, a.z*u + b.z*t + c.z*s);
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ")";
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3 vector &&
                   x == vector.x &&
                   y == vector.y &&
                   z == vector.z;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(x, y, z);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 operator +(Vector3 a, double b)
        {
            return new Vector3(a.x + b, a.y + b, a.z + b);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(b, a);
        }

        public static Vector3 operator -(Vector3 a, double b)
        {
            return new Vector3(a.x - b, a.y - b, a.z - b);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.x, -a.y, -a.z);
        }

        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3 operator *(Vector3 a, double b)
        {
            return new Vector3(a.x * b, a.y * b, a.z * b);
        }

        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static Vector3 operator /(Vector3 a, double b)
        {
            return new Vector3(a.x / b, a.y / b, a.z / b);
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return (a - b).Magnitude() <= 0.00001;
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return (a - b).Magnitude() >= 0.00001;
        }
    }
}
