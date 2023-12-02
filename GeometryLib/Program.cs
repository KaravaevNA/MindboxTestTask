

namespace GeometryLib
{
    class Program
    {
        static void Main()
        {
            //Пример работы с библиотекой

            List<Shape> shapes = new List<Shape>
            {
                new Circle(4.5f),
                new Triangle(1.11f, 2.22f, 3.32f),
                new Triangle(3.0f, 5.0f, 4.0f)
            };
            
            shapes.ForEach(x => Console.WriteLine($"Area: {x.GetArea(),-10} | Is right triangle: {x is Triangle triangle && triangle.IsRightTriangle()}"));
        }
    }
}