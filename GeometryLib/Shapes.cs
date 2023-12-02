using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib
{
    public abstract class Shape
    {
        public abstract float GetArea();
    }

    public class Circle : Shape
    {
        public float Radius { get; }

        public Circle(float radius)
        {
            if (radius <= 0)
                throw new ArgumentException($"Invalid circle radius: {radius}");

            Radius = radius;
        }

        public override float GetArea()
        {
            return (float)(Math.PI * Radius * Radius);
        }
    }

    public class Triangle : Shape
    {
        public float A { get; }
        public float B { get; }
        public float C { get; }

        public Triangle(float a, float b, float c)
        {
            if (a <= 0 || b <= 0 || c <= 0 || a + b <= c || a + c <= b || b + c <= a)
                throw new ArgumentException($"Invalid triangle sides. A: {a}, B: {b}, C: {c}");

            (A, B, C) = (a, b, c);
        }

        public override float GetArea()
        {
            float p = (A + B + C) / 2;
            return (float)Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }

        public bool IsRightTriangle()
        {
            float[] sides = { A, B, C };
            Array.Sort(sides);
            return sides[0] * sides[0] + sides[1] * sides[1] == sides[2] * sides[2];
        }
    }
}
